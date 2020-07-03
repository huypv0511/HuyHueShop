using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemo.Areas.Admin.Models
{
    public class ProductCategoryModel
    {
        [Required]
        public string Name { get; set; }

        public long ID { get; set; }
    }
}