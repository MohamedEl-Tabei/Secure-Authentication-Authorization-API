using DAL.Context;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOTPRepository OTPRepository { get; }
        public IAppUserRepository AppUserRepository { get; }

        private readonly MyContext _myContext;
        private IDbContextTransaction? Transaction { get; set; }


        public UnitOfWork(IOTPRepository oTPRepository,IAppUserRepository appUserRepository, MyContext myContext)
        {
            OTPRepository = oTPRepository;
            AppUserRepository = appUserRepository;
            _myContext = myContext;
        }

        public async Task BeginTransactionAsync()
        {
            Transaction = await _myContext.Database.BeginTransactionAsync();
        }



        public async Task SaveChangesAsync()
        {
            try
            {
                await _myContext.SaveChangesAsync();
                if (Transaction != null) await Transaction.CommitAsync();
            }
            catch
            {
                await Transaction.RollbackAsync();
                throw;
            }
        }
    }
}
