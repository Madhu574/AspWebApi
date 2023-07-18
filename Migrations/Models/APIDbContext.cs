//using System;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeMCrud.Models
//{
//    public class APIDbContext : DbContext
//    {
//        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
//        public DbSet<Department> Departments
//        {
//            get;
//            set;
//        }
//        public DbSet<Employee> Employees
//        {
//            get;
//            set;
//        }
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            builder.Entity<Department>().ToTable("Department");
//            builder.Entity<Employee>().ToTable("Employee");

//        }
//    }
//}

using EmployeeDemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMCrud.Models
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; } // Added DbSet for Designation
        public object Designation { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Department>().ToTable("Department");
            builder.Entity<Employee>().ToTable("Employee");
            builder.Entity<Designation>().ToTable("Designation");
            // Mapping Designation to "Designation" table
        }
      

    }
}
