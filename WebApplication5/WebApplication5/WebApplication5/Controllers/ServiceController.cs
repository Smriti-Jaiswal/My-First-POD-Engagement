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
    public class ServiceController : ControllerBase
    {
        private readonly TransactionService _TransactionService;
        private readonly UserService _UserService;
        private readonly AccountService _AccountService;
        private readonly ServiceService _ServiceService;

        public ServiceController(TransactionService transactionService,
            ServiceService serviceService,
            UserService userService, AccountService accountService)
        {
            _TransactionService = transactionService;
            _UserService = userService;
            _AccountService = accountService;
            _ServiceService = serviceService;
        }

        [HttpPost("[Action]")]
        [Produces(typeof(ArrayResponse<ServiceApproveModel>))]
        [Authorize]
        public async Task<ActionResult> ApproveService([FromBody] ServiceApproveModel viewModel)
        {
            var response = new singleResponse<ServiceApproveModel>();
            try
            {
                if (viewModel.id == null)
                {
                    response.message = $"Id is required";
                    return BadRequest(response);
                }
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;

                var oModel = await _ServiceService.GetAsync(viewModel.id);
                oModel.IsApproved = viewModel.isApproved;

                if (oModel.ReqWhat == "WITHDRAW")
                {
                    // debit entry
                    Transaction debitTrs = new Transaction();
                    debitTrs.Amount = oModel.Amount ?? 0;
                    debitTrs.CreatedOn = DateTime.Now;
                    debitTrs.IsReqTransaction = true;
                    debitTrs.Type = TransactionType.Debit.ToString();
                    debitTrs.UserId = oModel.ReqBy;
                    debitTrs.MadeBy = int.Parse(userId);

                    await _TransactionService.AddAsync(debitTrs);
                }

                await _ServiceService.UpdateAsync(oModel);

                response.message = "Requested updated successfully";
                response.model = viewModel;

            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Produces(typeof(singleResponse<ServiceReqModel>))]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] ServiceReqModel viewModel)
        {
            var response = new singleResponse<ServiceReqModel>();
            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Name").Value;

                //if (viewModel.type == "DEBITCARD")
                //{
                //    var debitCardExist = _ServiceService.GetAll(int.Parse(userId)).Where(e => e.ReqWhat == "DEBITCARD" && e.IsApproved == true).Count() > 0;
                //    if (debitCardExist)
                //    {
                //        response.message = $"Multiple cards not allowed";
                //        return BadRequest(response);
                //    }
                //}
                //if (viewModel.type == "CHEQUE")
                //{
                //    var chequeExist = _ServiceService.GetAll(int.Parse(userId)).Where(e => e.ReqWhat == "CHEQUE" && e.IsApproved == true).Count() > 0;
                //    if (chequeExist)
                //    {
                //        response.message = $"Multiple cheques not allowed";
                //        return BadRequest(response);
                //    }
                //}

                Service oModel = new Service();
                oModel.ReqWhat = viewModel.type;
                oModel.CreatedOn = DateTime.Now;
                oModel.ReqBy = int.Parse(userId);
                oModel.Amount =  viewModel.amount;
                oModel.IsApproved = null;

                await _ServiceService.AddAsync(oModel);

                response.message = "Requested successfully";
                response.model = viewModel;
              
            }
            catch (Exception ex)
            {
                response.message = $"{ex.ToString()}";
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Produces(typeof(ArrayResponse<ServiceResModel>))]
        [Authorize]
        public async Task<ActionResult> GetAll(int? id)
        {
            var response = new ArrayResponse<ServiceResModel>();
            try
            {
                var userRole = ((ClaimsIdentity)User.Identity).FindFirst("Role").Value;
                var data = _ServiceService.GetAll(id,userRole).ToList();

                var viewModel = new List<ServiceResModel>();

                foreach (var item in data)
                {
                    var each = new ServiceResModel();
                    each.date = item.CreatedOn;
                    each.id = item.ServiceId;
                    each.type = item.ReqWhat;
                    each.status = item.IsApproved == null ? "Pending" : item.IsApproved == true ? "Approved" : "Rejected";

                    var user = await _UserService.GetAsync(item.ReqBy);

                    if (user != null)
                    {
                        each.reqBy = user.FirstName;
                    }

                    viewModel.Add(each);

                }

                response.message = "Requested successfully";
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
