using api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Services;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _UserService;
        public AuthController(UserService UserService) => _UserService = UserService;

        [HttpPost]
        [Produces(typeof(singleResponse<LoginResModel>))]
        public ActionResult<LoginResModel> Create(LoginReqModel model)
        {

            var response = new singleResponse<LoginResModel>();

            if (model == null)
            {
                response.message = "Invalid credentials";
                return BadRequest(response);
            }

            var token = _UserService.DoLogin(email: model.email, password: model.password);


            if (token == null)
            {
                response.message = "Username or password is incorrect";
                return BadRequest(response);
            }

            var viewModel = new LoginResModel
            {
                email = model.email,
                token = token,
            };

            response.message = "Login successfully";
            response.model = viewModel;

            return Ok(response);
        }
    }
}
