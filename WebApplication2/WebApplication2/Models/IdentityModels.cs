using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace WebApplication2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string EMail { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public int NumberOfRepos = 0;
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}