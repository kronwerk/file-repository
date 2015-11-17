using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplication.Models
{
    public class Repositories
    {
        public int RepositoriesID { get; set; }
        public string name { get; set; }
        public int owner { get; set; }//was string, but will contain Users id
        public int usersNum { get; set; }
        public int filesNum { get; set; }
    }
}