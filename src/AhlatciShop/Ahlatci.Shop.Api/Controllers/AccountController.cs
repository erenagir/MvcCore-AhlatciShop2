using Ahlatci.Shop.Aplication.Models.Dtos.Category;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Categories;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        
        [HttpPost("create")]

        public async Task<ActionResult< Result<int>>> CreateCategory(CreateUserVM createUserVM)
        {
            var entity = await _accountService.CreateUser(createUserVM);
            return Ok(entity);
        }
       
    }
}