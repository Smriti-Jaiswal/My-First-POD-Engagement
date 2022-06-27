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
    public class UserController : ControllerBase
    {
        private readonly UserService _UserService;
        private readonly AccountService _AccountService;
        private readonly TransactionService _TransactionService;

        public UserController(UserService userService, AccountService accountService, TransactionService transactionService)
        {
            _UserService = userService;
            _TransactionService = transactionService;
            _AccountService = accountService;
        }

        #region Create User
        [HttpPost]
        [Produces(typeof(singleResponse<UserModel>))]
       // [Authorize]
        public async Task<ActionResult> Create([FromBody] UserModel viewModel)
        {
            var response = new singleResponse<UserModel>();


            if (!System.Enum.IsDefined(typeof(Role), viewModel.userType))
            {
                response.message = $"Can't create {viewModel.userType}";
                return BadRequest(response);
            }


            var emailExist = _UserService.GetAll(isDelete: false).Where(e => e.Email != null && e.Email.Equals(viewModel.email)).Count() > 0;

            if (emailExist)
            {
                response.message = $"{viewModel.email} already exist";
                return BadRequest(response);
            }

            try
            {

                User oModel = new User();

                oModel.CreatedOn = DateTime.Now;
                oModel.IsDeleted = false;
                // CreatedBy = _user.Name == null ? _user.Name : "InitialCreate",
                oModel.Password = Hash.Encrypt(viewModel.password);

                // account tb
                Account oAccount = new Account();

                if (viewModel.userType != Role.Admin.ToString())
                {   
                    oAccount.AccountNumber = viewModel.accountNumber;
                    oAccount = await _AccountService.AddAsync(oAccount);
                }


                oModel = viewModel.ToModel(oModel);

                if (viewModel.userType != Role.Admin.ToString())
                { 
                    oModel.AccountId = oAccount.AccountId;
                }

                oModel = await _UserService.AddAsync(oModel);

                if (viewModel.userType != Role.Admin.ToString()) 
                { 
                    var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;

                    // transaction tb
                    Transaction oTransaction = new Transaction();
                    oTransaction.Amount = viewModel.openAmount;
                    oTransaction.CreatedOn = DateTime.Now;
                    oTransaction.IsReqTransaction = false;
                    oTransaction.Type = TransactionType.Credit.ToString();
                    oTransaction.UserId = oModel.UserId;
                    oTransaction.MadeBy = int.Parse(userId);

                    await _TransactionService.AddAsync(oTransaction);
                }

                viewModel.password = null;
                response.model = viewModel;

                response.message = $"User created succesfully";
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
                var data = _UserService.GetAll(isDelete: false).Where(e => e.UserType != Role.Admin.ToString() && e.UserId != int.Parse(userId)).ToList();
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


        #region Get One User
        [HttpGet("{id}")]
        [Produces(typeof(singleResponse<UserModel>))]
        [Authorize]
        public async Task<ActionResult> GetOne(int? id)
        {
            var response = new singleResponse<UserModel>();

            try
            {
                var data = await _UserService.GetAsync(id);
                
                    var each = new UserModel();
                    each.address = data.Address;
                    each.city = data.City;
                    each.firstName = data.FirstName;
                    each.lastName = data.LastName;
                each.dateOfBirth = data.DateOfBirth;
                each.gender = data.Gender;
                    each.email = data.Email;
                each.country = data.Country;
                each.userType = data.UserType;
                    each.id = data.UserId;

                    
                response.model = each;

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

        #region Update One User
        [HttpPut]
        [Produces(typeof(singleResponse<UserModel>))]
        [Authorize]
        public async Task<ActionResult> UpdateOne([FromBody] UserModel viewModel)
        {
            var response = new singleResponse<UserModel>();

            var exist = _UserService.GetAll(email: viewModel.email, isDelete: false).Where(e => e.UserId != viewModel.id).Count() > 0;

            if (exist)
            {
                response.message = $"{viewModel.email} already exist";
                return BadRequest(response);
            }


            try
            {
                var data = await _UserService.GetAsync(viewModel.id);

                data.Address = viewModel.address;
                data.City = viewModel.city;
                data.FirstName = viewModel.firstName;
                data.LastName = viewModel.lastName;
                data.Gender = viewModel.gender;
                data.DateOfBirth = viewModel.dateOfBirth;
                data.Email = viewModel.email;
                data.Country = viewModel.country;
                data.UserType = viewModel.userType;

                await _UserService.UpdateAsync(data);

                response.model = viewModel;

                response.message = $"User update succesfully";
            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }


            return Ok(response);
        }
        #endregion


        #region Delete One User
        [HttpDelete("{id}")]
        [Produces(typeof(singleResponse<UserModel>))]
        [Authorize]
        public async Task<ActionResult> DeleteOne(int? id)
        {
            var response = new singleResponse<UserModel>();


            try
            {
                var data = await _UserService.GetAsync(id);

                data.IsDeleted = true;

                await _UserService.UpdateAsync(data);

                response.model = null;

                response.message = $"User deleted succesfully";
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
