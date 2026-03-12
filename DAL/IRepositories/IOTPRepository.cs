using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    public interface IOTPRepository:IRepository<OTP>
    {
        Task<bool> IsVerifiedEmailAsync(string email);
        Task<bool> IsVerifiedPhoneNumberAsync(string phoneNumber);
    }
}
