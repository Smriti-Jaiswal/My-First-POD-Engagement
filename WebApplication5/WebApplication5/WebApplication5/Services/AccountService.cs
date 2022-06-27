using api.Entity;
using api.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication5.Entity.Models;

namespace WebApplication5.Services
{
    public class AccountService
    {
        private IGenericRepository<Account> _db;
        public AccountService(IGenericRepository<Account> user)
        {
            _db = user;
        }


        public async Task<Account> AddAsync(Account model)
        {
            await _db.InsertAsync(model);
            return model;
        }

        public async Task<Account> GetAsync(int? acctId)
        {
            return await _db.Table.Where(e => e.AccountId == acctId).FirstOrDefaultAsync();
        }

    }
}
