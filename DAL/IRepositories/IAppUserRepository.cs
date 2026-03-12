using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> IsPhoneNumberExistAsync(string phoneNumber);
    }
}
