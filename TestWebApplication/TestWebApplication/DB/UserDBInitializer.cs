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
            users.ForEach(s => context.users.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { Title = "Chemistry",      Credits = 3, },
                new Course { Title = "Microeconomics", Credits = 3, },
                new Course { Title = "Macroeconomics", Credits = 3, },
                new Course { Title = "Calculus",       Credits = 4, },
                new Course { Title = "Trigonometry",   Credits = 4, },
                new Course { Title = "Composition",    Credits = 3, },
                new Course { Title = "Literature",     Credits = 4, }
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID = 1, CourseID = 1, Grade = 1 },
                new Enrollment { StudentID = 1, CourseID = 2, Grade = 3 },
                new Enrollment { StudentID = 1, CourseID = 3, Grade = 1 },
                new Enrollment { StudentID = 2, CourseID = 4, Grade = 2 },
                new Enrollment { StudentID = 2, CourseID = 5, Grade = 4 },
                new Enrollment { StudentID = 2, CourseID = 6, Grade = 4 },
                new Enrollment { StudentID = 3, CourseID = 1            },
                new Enrollment { StudentID = 4, CourseID = 1,           },
                new Enrollment { StudentID = 4, CourseID = 2, Grade = 4 },
                new Enrollment { StudentID = 5, CourseID = 3, Grade = 3 },
                new Enrollment { StudentID = 6, CourseID = 4            },
                new Enrollment { StudentID = 7, CourseID = 5, Grade = 2 },
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}
    {
    }
}