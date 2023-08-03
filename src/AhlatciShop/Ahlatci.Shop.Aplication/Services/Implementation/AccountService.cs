using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
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

        public AccountService(IMapper mapper, IUWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        public Task<bool> CreateUser(CreateUserVM createUserVM)
        {
            var customerEntity=_mapper.Map<Customer>(createUserVM);
            var accountEntity=_mapper.Map<Account>(createUserVM);
        }
    }
}
