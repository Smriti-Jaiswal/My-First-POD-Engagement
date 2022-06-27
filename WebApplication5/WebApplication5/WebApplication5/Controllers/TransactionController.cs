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
using WebApplication5.Options;
using WebApplication5.Services;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _TransactionService;
        private readonly UserService _UserService;
        private readonly AccountService _AccountService;

        public TransactionController(TransactionService transactionService, UserService userService, AccountService accountService)
        {
            _TransactionService = transactionService;
            _UserService = userService;
            _AccountService = accountService;
        }


        [HttpGet]
        [Produces(typeof(singleResponse<TransactionModel>))]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
            var oModel =  _TransactionService.GetAcconuntDetails(int.Parse(userId));
            return Ok(oModel);
        }

        [HttpGet("[Action]")]
        [Produces(typeof(ArrayResponse<TransactionResModel>))]
        [Authorize]
        public async Task<ActionResult> GetUserTransaction()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;
            var oModel = _TransactionService.GetUserTransaction(int.Parse(userId)).Select(e => new TransactionResModel
            {
                amount = e.Amount,
                date = e.CreatedOn,
                id = e.TransactionId,
                type = e.Type,
                madeById = e.MadeBy

            }).ToList();
            foreach (var item in oModel)
            {
                var user = await _UserService.GetAsync(item.madeById);
                item.username =  user.FirstName; 
            }
            return Ok(oModel);
        }

        [HttpPost]
        [Produces(typeof(singleResponse<TransactionModel>))]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] TransactionModel viewModel)
        {
            var response = new singleResponse<TransactionModel>();
            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;

                // credit entry
                Transaction creditTrs = new Transaction();
                creditTrs.Amount = viewModel.amount;
                creditTrs.CreatedOn = DateTime.Now;
                creditTrs.IsReqTransaction = false;
                creditTrs.Type = TransactionType.Credit.ToString();
                creditTrs.UserId = viewModel.userId;
                creditTrs.MadeBy = int.Parse(userId);

                await _TransactionService.AddAsync(creditTrs);

                // debit entry
                Transaction debitTrs = new Transaction();
                debitTrs.Amount = viewModel.amount;
                debitTrs.CreatedOn = DateTime.Now;
                debitTrs.IsReqTransaction = false;
                debitTrs.Type = TransactionType.Debit.ToString();
                debitTrs.UserId = int.Parse(userId);
                debitTrs.MadeBy = viewModel.userId;

                await _TransactionService.AddAsync(debitTrs);

                response.message = "Transferred successfully";
                response.model = viewModel;
              
            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
