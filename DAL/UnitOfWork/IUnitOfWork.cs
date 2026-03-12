using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUnitOfWork
    {
        IOTPRepository OTPRepository { get; }
        IAppUserRepository AppUserRepository { get; }
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        
    }
}
