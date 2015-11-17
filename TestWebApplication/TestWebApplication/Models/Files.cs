using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplication.Models
{
    public class Files
    {
        public int FilesID { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public int repo { get; set; }// was string type.don't know why. believe it will contain repos id
    }
}