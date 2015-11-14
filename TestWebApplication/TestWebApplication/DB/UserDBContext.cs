﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using TestWebApplication.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestWebApplication.Models
{
    public class UserDBContext : DbContext
    {
        public DbSet<FUser> FUsers { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<RepositoryClass> Repositories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}