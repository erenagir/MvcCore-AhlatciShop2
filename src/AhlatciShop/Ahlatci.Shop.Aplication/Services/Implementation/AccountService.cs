using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Account;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUWork _db;
        private readonly IConfiguration _configuration;

        public AccountService(IMapper mapper, IUWork db, IConfiguration configuration)
        {
            _mapper = mapper;
            _db = db;
            _configuration = configuration;
        }
        

         [ValidationBehavior(typeof(CreateUserValidator))]
        public async Task<Result<bool>> CreateUser(CreateUserVM createUserVM)
        {
            var UserNameExist=await _db.GetRepository<Account>().AnyAsync(x=>x.Username.Trim().ToUpper() ==
            createUserVM.Username.Trim().ToUpper());
            if (UserNameExist)
            {
                throw new AlreadyExistsException("Kullanıcı adı daha önceden Kullanılmaktadır");
            }
            var UserEmailExist = await _db.GetRepository<Customer>().AnyAsync(x => x.Email.Trim().ToUpper() ==
            createUserVM.Username.Trim().ToUpper());
            if (UserEmailExist)
            {
                throw new AlreadyExistsException("Email daha kullanılmıştır Kullanılmaktadır");
            }
            var  result= new Result<bool>();
            var customerEntity = _mapper.Map<CreateUserVM,Customer>(createUserVM);
            var accountEntity = _mapper.Map<CreateUserVM, Account>(createUserVM);
            accountEntity.Password = CipherUtils
                .EncryptString(_configuration["AppSettings:SecretKey"],accountEntity.Password);
            
            
            await _db.GetRepository<Customer>().add(customerEntity);
           
            await _db.GetRepository<Account>().add(accountEntity);
            customerEntity.Account = accountEntity; 
            var customerAccountCreateResult =await _db.ComitAsync();
            if (customerAccountCreateResult)
            {
                accountEntity.Customer = customerEntity;
              var customerAccountUpdateResult= await _db.ComitAsync();
                result.Data = customerAccountUpdateResult;
                return result;
            }

            return result;
        }

        public async Task<Result<TokenDto>> Login(LoginVM loginVM)
        {
           var existsUsers= await _db.GetRepository<Account>()
                .GetByFilterAsync(x=>x.Username.Trim().ToUpper()==loginVM.Username.Trim().ToUpper()
               && loginVM.Password == CipherUtils.DecryptString(_configuration["AppSettings:SecretKey"],x.Password));

            if(!existsUsers.Any())
            {

            }
        }
    }
}
