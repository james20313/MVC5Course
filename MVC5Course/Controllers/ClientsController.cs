using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [RoutePrefix("JamesHAHA")]
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ClientRepository repo;
        OccupationRepository occuRepo;

        public ClientsController()
        {
            repo = RepositoryHelper.GetClientRepository();
            occuRepo = RepositoryHelper.GetOccupationRepository(repo.UnitOfWork);

        }

        // GET: Clients
        [Route("Index")]
        public ActionResult Index()
        {
            var items = (from p in db.Client
                         select p.CreditRating).Distinct().OrderBy(p => p)
                         .Select(p => new SelectListItem()
                         {
                             Text = p.Value.ToString(),
                             Value = p.Value.ToString()
                         });
            ViewBag.CreditRating = new SelectList(items, "Value", "Text");

            var client = repo.All();
            return View(client.Take(10).ToList());
        }

        [HttpPost]
        [Route("BatchUpdate")]
        [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
        public ActionResult BatchUpdate(ClientBatchVM[] data)
        //public ActionResult BatchUpdate(Client[] data)
        {
            //data[0].ClientId

            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var client = db.Client.Find(item.ClientId);
                    client.FirstName = item.FirstName;
                    client.LastName = item.LastName;
                    client.MiddleName = item.MiddleName;

                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewData.Model = repo.All().Take(10);

            return View("Index");
        }

        public ActionResult Search(string keyword, String CreditRating)
        {
            var data = repo.搜尋名稱(keyword, CreditRating);

            var items = (from p in db.Client
                         select p.CreditRating).Distinct().OrderBy(p => p)
                        .Select(p => new SelectListItem()
                        {
                            Text = p.Value.ToString(),
                            Value = p.Value.ToString()
                        });
            ViewBag.CreditRating = new SelectList(items, "Value", "Text");

            return View("Index", data);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //[Route("{*name}")]
        public ActionResult Details2(string name)
        {
            string[] names = name.Split('/');
            string FirstName = names[0];
            string MiddleName = names[1];
            string LastName = names[2];

            Client client = repo.All().FirstOrDefault(p => p.FirstName == FirstName && p.MiddleName == MiddleName && p.LastName == LastName);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View("Details", client);
        }
        [Route("{id}/orders")]
        [ChildActionOnly]
        public ActionResult Details_OrderList(int id)
        {
            ViewData.Model = repo.Find(id).Order.ToList();
            return PartialView("OrderList");
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IdNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            //FormCollection form  <==不使用

            var client = repo.Find(id);


            if (TryUpdateModel(client, "", null, new string[] { "FirstName" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = repo.Find(id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
