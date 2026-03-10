using DAL.Context;
using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class OTPRepository : Repository<OTP>, IOTPRepository
    {
        public OTPRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
