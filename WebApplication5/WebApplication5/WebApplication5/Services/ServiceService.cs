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
    public class ServiceService
    {
        private IGenericRepository<Service> _db;
        public ServiceService(IGenericRepository<Service> user)
        {
            _db = user;
        }


        public async Task<Service> AddAsync(Service model)
        {
            await _db.InsertAsync(model);
            return model;
        }

        public async Task<Service> GetAsync(int id)
        {
            return await _db.Table.Where(e => e.ServiceId == id).FirstOrDefaultAsync();
        }
        public async Task<Service> UpdateAsync(Service model)
        {
            return await _db.UpdateAsync(model);
        }

        public IQueryable<Service> GetAll(int?  userId = null,string role=null)
        {

            var data = _db.TrackAll.AsQueryable();

            if (userId != null)
            {
                if (role == "User")
                {
                    data = data.Where(e => e.ReqBy == userId);
                }
               
                
            }

            return data;

        }

    }
}
