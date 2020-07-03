using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace MVCDemo.Models
{
    [Serializable]
    public class CartModel
    {
       
        public Product Product { set; get; }

        public int Quantity { set; get; }

    }
}