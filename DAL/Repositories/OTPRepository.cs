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

        public async Task<bool> IsVerifiedEmailAsync(string email)
        {
            return await _myContext.OTPs.AnyAsync(
                o => o.Target == email &&
                     o.TargetType == OTPTargetType.Email &&
                     o.IsVerified &&
                     o.ExpirationTime > DateTime.UtcNow&&
                     o.CodeHash == null
            );
        }

        public async Task<bool> IsVerifiedPhoneNumberAsync(string phoneNumber)
        {
            return await _myContext.OTPs.AnyAsync(
                o => o.Target == phoneNumber &&
                     o.TargetType == OTPTargetType.PhoneNumber &&
                     o.IsVerified &&
                     o.ExpirationTime > DateTime.UtcNow&&
                     o.CodeHash==null
            );
        }
    }
}
