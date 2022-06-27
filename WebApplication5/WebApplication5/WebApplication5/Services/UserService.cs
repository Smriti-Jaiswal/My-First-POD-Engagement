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
    public class UserService
    {
        private IGenericRepository<User> _db;
        public UserService(IGenericRepository<User> user)
        {
            _db = user;
        }


        public async Task<User> AddAsync(User model)
        {
            await _db.InsertAsync(model);
            return model;
        }

        public IQueryable<User> GetAll(string email = null ,bool? isDelete = null)
        {

            var data = _db.TrackAll.AsQueryable();

            if (isDelete != null)
            {
                data = data.Where(c => c.IsDeleted == isDelete);
            }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
            {
                data = data.Where(c => c.Email.Contains(email));
            }

            return data;

        }

        public async Task<User> GetAsync(int? id)
        {
            return await _db.Table.Where(e => e.UserId == id).FirstOrDefaultAsync();
        }



        public async Task<User> UpdateAsync(User model)
        {
            return await _db.UpdateAsync(model);
        }

        public void DeleteAsync(User data)
        {
            _db.RemoveAsync(data);
        }

        public string DoLogin(string email = null, string password = null)
        {
            if (String.IsNullOrEmpty(email) && String.IsNullOrWhiteSpace(email))
            {
                return null;
            }
            var user = _db.Table.FirstOrDefault(x => (x.Email == email && x.Password == Hash.Encrypt(password)) && x.IsDeleted == false);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("mySecret_secrteyourSecret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Name", user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Role", user.UserType.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
