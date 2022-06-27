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
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using WebApplication5.ViewModel;

namespace WebApplication5.Services
{
    public class TransactionService
    {
        private IGenericRepository<Transaction> _db;
        public IConfiguration Configuration { get; }
        public TransactionService(IGenericRepository<Transaction> user, IConfiguration configuration)
        {
            _db = user;
            Configuration = configuration;
        }


        public async Task<Transaction> AddAsync(Transaction model)
        {
            await _db.InsertAsync(model);
            return model;
        }

        public IQueryable<Transaction> GetAll(bool? isDelete = null)
        {

            var data = _db.TrackAll.AsQueryable();

            return data;

        }

        public async Task<Transaction> GetAsync(int id)
        {
            return await _db.Table.Where(e => e.UserId == id).FirstOrDefaultAsync();
        }

        public IQueryable<Transaction> GetUserTransaction(int id)
        {
            return  _db.Table.Where(e => e.UserId == id).Include(p=> p.User).AsQueryable();
        }

        public BalanceDetailsModel GetAcconuntDetails(int id)
        {
            string query = @"
                            SELECT UserId
                                 , SUM(COALESCE(CASE WHEN Type = 'Debit' THEN Amount END,0)) total_debits
                                 , SUM(COALESCE(CASE WHEN Type = 'Credit' THEN Amount END,0)) total_credits
                                 , SUM(COALESCE(CASE WHEN Type = 'Credit' THEN Amount END,0)) - SUM(COALESCE(CASE WHEN Type = 'Debit' THEN Amount END,0)) balance 
                              FROM TransactionTable where UserId = " + id + " GROUP BY UserId;";
            string query1 = @"SELECT * FROM UserTable userTable inner join AccountTable acct on userTable.AccountId = acct.AccountId where userTable.UserId =" + id + " and userTable.IsDeleted = 0";

            var objresutl = new BalanceDetailsModel();

            try
            {
                var connectionString = Configuration.GetConnectionString("DBConnection");


                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(connectionString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        // objresutl.Load(myReader);

                        while (myReader.Read())
                        {
                            objresutl.balance = Convert.ToDecimal(myReader["balance"]);
                            objresutl.total_credits = Convert.ToInt32(myReader["total_credits"]);
                            objresutl.total_debits = Convert.ToInt32(myReader["total_debits"]);
                            objresutl.UserId = Convert.ToInt32(myReader["UserId"]);
                        }
                        myCon.Close();
                    }
                    myCon.Open();
                    using (SqlCommand myCommand1 = new SqlCommand(query1, myCon))
                    {
                        SqlDataReader myReader1 = myCommand1.ExecuteReader();
                        // objresutl.Load(myReader);

                        while (myReader1.Read())
                        {
                            objresutl.accountNumber = myReader1["AccountNumber"].ToString();
                        }
                        myCon.Close();
                    }
                    myCon.Open();
                    using (SqlCommand myCommand2 = new SqlCommand(query1, myCon))
                    {
                        SqlDataReader myReader2 = myCommand2.ExecuteReader();
                        // objresutl.Load(myReader);

                        while (myReader2.Read())
                        {
                            objresutl.firstName = myReader2["FirstName"].ToString();
                            objresutl.lastName = myReader2["LastName"].ToString();
                        }
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex;
            }

            return objresutl;
        }

        public async Task<Transaction> UpdateAsync(Transaction model)
        {
            return await _db.UpdateAsync(model);
        }

    }
}
