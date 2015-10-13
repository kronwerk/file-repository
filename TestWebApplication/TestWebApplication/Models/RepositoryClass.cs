using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplication.Models
{
    public class RepositoryClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public int usersNum { get; set; }
        public int filesNum { get; set; }
    }
}