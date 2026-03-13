using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    public interface IOTPRepository:IRepository<OTP>
    {
        Task<OTP> GetByTargetAsync(string target);
    }
}
