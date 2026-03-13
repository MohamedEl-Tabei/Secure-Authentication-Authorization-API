using DAL.Context;
using DAL.Enums;
using DAL.IRepositories;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<OTP> GetByTargetAsync(string target)
        {
            return await _myContext.OTPs.FirstOrDefaultAsync(o => o.Target == target);
        }
    }
}
