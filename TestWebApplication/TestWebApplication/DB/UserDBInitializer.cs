using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestWebApplication.Models;
using TestWebApplication.DB;

namespace TestWebApplication.DB
{
    public class UserDBInitializer : DropCreateDatabaseIfModelChanges<UserDBContext>
    {
        protected override void Seed(UserDBContext context)
        {
            var users = new List<FUser>
            {
                new FUser { FirstName = "Carson",   LastName = "Alexander",age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Meredith", LastName = "Alonso",   age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Arturo",   LastName = "Anand",    age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Gytis",    LastName = "Barzdukas",age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Yan",      LastName = "Li",       age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Peggy",    LastName = "Justice",  age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Laura",    LastName = "Norman",   age=21,gender=1, city ="SPb" },
                new FUser { FirstName = "Nino",     LastName = "Olivetto", age=21,gender=1, city ="SPb" }
            };
            users.ForEach(s => context.FUser.Add(s));
            context.SaveChanges();

            var files = new List<Files>
            {
                new Files { name = "file1",         type = "text",content="some text", repo=2 },
                new Files { name = "file2",         type = "text",content="some text blah" },
                new Files { name = "one_more_file", type = "text",content="some text more" },
                new Files { name = "and more",      type = "text",content="more some text" },
                new Files { name = "OneMoreThing",  type = "text",content="some more text" }
            };
            files.ForEach(s => context.Files.Add(s));
            context.SaveChanges();

            var repos = new List<Repositories>
            {
                new Repositories {name="file_repo",owner=2,filesNum=1,usersNum=1 },
                new Repositories {name="file_repo2",owner=4,filesNum=1,usersNum=1 },
                new Repositories {name="repo_file",owner=1,filesNum=1,usersNum=1 },
                new Repositories {name="some_repos",owner=3,filesNum=1,usersNum=1 }
            };
            repos.ForEach(s => context.Repositories.Add(s));
            context.SaveChanges();
        }
    }
}
