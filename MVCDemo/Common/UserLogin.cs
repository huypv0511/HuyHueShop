using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace MVCDemo
{
    [Serializable]
    public class UserLogin
    {
  
        public int UserID { set; get; }
        public string UserName { set; get; }
    }
}