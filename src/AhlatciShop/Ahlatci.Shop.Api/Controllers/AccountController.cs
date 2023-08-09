using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{
    [ApiController]
    [Route("account")]
    [Authorize]//oturum açýlmadan kilitli
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        
        [HttpPost("reister")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<bool>>> Reister(ReisterVM reisterVM)
        {
            var result = await _accountService.Reister(reisterVM);
            return Ok(result);
        }
        [HttpPost("login")]
        [AllowAnonymous]//kiliti ezdik
        public async Task<ActionResult<Result<bool>>> LoginAccount(LoginVM loginVM)
        {
            var result = await _accountService.Login(loginVM);
            return Ok(result);
        }
        [HttpPost("update")]
        
        public async Task<ActionResult<Result<bool>>> update(UpdateUserVM updateUserVM)
        {
            var result = await _accountService.UpdateUser(updateUserVM);
            return Ok(result);
        }
    }
}