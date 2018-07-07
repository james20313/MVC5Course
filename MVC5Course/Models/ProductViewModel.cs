using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "產品名稱為必填選項")]
        [StringLength(10, ErrorMessage = "商品名稱字元不可大於10")]
        public string ProductName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:#}")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

    }
}