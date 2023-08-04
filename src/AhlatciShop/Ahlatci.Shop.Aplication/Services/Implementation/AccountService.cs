using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Account;
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
        public async Task<bool> CreateUser(CreateUserVM createUserVM)
        {
            var customerEntity = _mapper.Map<Customer>(createUserVM);
            var accountEntity = _mapper.Map<Account>(createUserVM);
            accountEntity.Password = CipherUtils
                .EncryptString(_configuration["AppSetting:SecretKey"],accountEntity.Password);
            
            
            await _db.GetRepository<Customer>().add(customerEntity);
           customerEntity.Account = accountEntity; 
            await _db.GetRepository<Account>().add(accountEntity);
            
            var result =await _db.ComitAsync();
            return result;
        }
    }
}
