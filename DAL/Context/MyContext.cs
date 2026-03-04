using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class MyContext:IdentityDbContext<AppUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        
    }
}
