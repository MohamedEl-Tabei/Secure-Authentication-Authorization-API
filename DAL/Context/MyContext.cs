using DAL.Models;
using DAL.ModelsConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class MyContext : IdentityDbContext<AppUser>
    {
        public DbSet<OTP> OTPs  { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            base.OnModelCreating(builder);
        }

    }
}
