using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplication.Models
{
    public class FileModel
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string repo { get; set; }
    }
}