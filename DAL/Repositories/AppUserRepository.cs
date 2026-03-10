using DAL.Context;
using DAL.IRepositories;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
