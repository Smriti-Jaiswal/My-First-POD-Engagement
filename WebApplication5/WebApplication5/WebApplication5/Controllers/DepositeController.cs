using api.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication5.Entity.Models;
using WebApplication5.Mapper;
using WebApplication5.Options;
using WebApplication5.Services;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositeController : ControllerBase
    {
        private readonly UserService _UserService;
        private readonly AccountService _AccountService;
        private readonly TransactionService _TransactionService;

        public DepositeController(UserService userService, AccountService accountService, TransactionService transactionService)
        {
            _UserService = userService;
            _TransactionService = transactionService;
            _AccountService = accountService;
        }

        #region Create Deposite
        [HttpPost]
        [Produces(typeof(singleResponse<TransactionModel>))]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] TransactionModel viewModel)
        {
            var response = new singleResponse<TransactionModel>();

            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;

                // transaction tb
                Transaction oTransaction = new Transaction();
                oTransaction.Amount = viewModel.amount;
                oTransaction.CreatedOn = DateTime.Now;
                oTransaction.IsReqTransaction = false;
                oTransaction.Type = TransactionType.Credit.ToString();
                oTransaction.UserId = viewModel.userId;
                oTransaction.MadeBy = int.Parse(userId);

                await _TransactionService.AddAsync(oTransaction);
                response.model = viewModel;

                response.message = $"Deposited succesfully";
            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }


            return Ok(response);
        }
        #endregion


        #region Get User
        [HttpGet]
        [Produces(typeof(ArrayResponse<UserModel>))]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var response = new ArrayResponse<UserModel>();

            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
                var data = _UserService.GetAll(isDelete: false).Where(e => e.UserType != Role.Admin.ToString()).ToList();
                var view = new List<UserModel>();
                foreach (var item in data)
                {
                    var each = new UserModel();
                    each.address = item.Address;
                    each.city = item.City;
                    each.firstName = item.FirstName;
                    each.lastName = item.LastName;
                    each.dateOfBirth = item.DateOfBirth;
                    each.gender = item.Gender;
                    each.email = item.Email;
                    each.userType = item.UserType;
                    each.id = item.UserId;

                    var acct = await _AccountService.GetAsync(item.AccountId);

                    if(acct != null)
                    {
                        each.accountNumber = acct.AccountNumber;
                    }

                    view.Add(each);

                }
                response.model = view;

                response.message = $"User get succesfully";
            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }


            return Ok(response);
        }
        #endregion


    }
}
