using System.ComponentModel.DataAnnotations;
using System;

namespace WebApplication2.Models
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Repository name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public int Owner { get; set; }

        [Display(Name = "Users")]
        public string Users { get; set; }

        [Display(Name = "Files")]
        public string Files { get; set; }

        [Display(Name = "Tags")]
        public string tags { get; set; }

        [Display(Name = "Last Change")]
        public DateTime LastChangeR { get; set; }
    }

}
