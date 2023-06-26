using System;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMCrud.Models
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
        public DbSet<Department> Departments
        {
            get;
            set;
        }
        public DbSet<Employee> Employees
        {
            get;
            set;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Department>().ToTable("Department");
            builder.Entity<Employee>().ToTable("Employee");
        }
    }
}

