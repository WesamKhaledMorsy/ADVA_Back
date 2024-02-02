using ADVA.DB;
using ADVA.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ADVA
{
    public class AppDBContext : DbContext
    {

        //private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDBContext( DbContextOptions<AppDBContext> options) : base(options)
        {
           // _httpContextAccessor= httpContextAccessor;
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>(entity =>
            {
                entity
                    .HasMany<Employee>(e => e.Employees)
                    .WithOne(e => e.DepartmentObj) //Each comment from MainSuppliersData points back to its parent
                    .HasForeignKey(e => e.FkDepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Employee>(entity =>
            {
                entity
                    .HasMany(e => e.Employees)                   
                    .WithOne(e=>e.ManagerObj)
                    .HasForeignKey(e=>e.FkManagerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Employee>(entity =>
            {
                entity
                    .HasOne<Department>(e=>e.DepartmentObj)
                    .WithMany(e => e.Employees)
                    .HasForeignKey(e => e.FkDepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
