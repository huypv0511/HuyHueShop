using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Model.EF;
using PagedList;
using PagedList.Mvc;

namespace MVCDemo.Models
{
    public class ModelMix
    {
        public IPagedList<Product> ProductObj { get; set; }
        public IEnumerable<ProductCategory> ProductCateObj { get; set; }
    }
}