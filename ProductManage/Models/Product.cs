﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProductManage.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Qnty { get; set; }
        public string Remarks { get; set; }
    }
}