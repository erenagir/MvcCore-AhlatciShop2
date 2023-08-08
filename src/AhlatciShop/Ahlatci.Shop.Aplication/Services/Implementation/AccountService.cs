using Ahlatci.Shop.Aplication.Behaviors;
using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.Dtos.Account;
using Ahlatci.Shop.Aplication.Models.RequestModels.Account;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Validators.Account;
using Ahlatci.Shop.Aplication.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.UWork;
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
        private readonly IUWork _uWork;
        private readonly IConfiguration _configuration;

        public AccountService(IMapper mapper, IUWork db, IConfiguration configuration)
        {
            _mapper = mapper;
            _uWork = db;
            _configuration = configuration;
        }
        

       

        public Task<Result<TokenDto>> Login(LoginVM loginVM)
        {
            throw new NotImplementedException();
        }

        [ValidationBehavior(typeof(CreateUserValidator))]
        public async Task<Result<bool>> Reister(CreateUserVM createUserVM)
        {
            var result = new Result<bool>();
            //await _uWork.GetRepository<Account>().AnyAsync(x => x.Username.Trim().ToUpper() == createUserVM.Username.Trim().ToUpper());
            //Aynı kullanıcı adı daha önce girilmiş mi.
            var usernameExists = await _uWork.GetRepository<Account>().AnyAsync(x => x.Username.Trim().ToUpper() == createUserVM.Username.Trim().ToUpper());
            if (usernameExists)
            {
                throw new AlreadyExistsException($"{createUserVM.Username} kullanıcı adı daha önce seçilmiştir. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            //Eposta adresi kullanılıyor mu.
            var emailExists = await _uWork.GetRepository<Customer>().AnyAsync(x => x.Email.Trim().ToUpper() == createUserVM.Email.Trim().ToUpper());
            if (emailExists)
            {
                throw new AlreadyExistsException($"{createUserVM.Email} eposta adresi kullanılmaktadır. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            //Gelen model Customer türüne maplandi
            var customerEntity = _mapper.Map<Customer>(createUserVM);
            //Gelen model Account türüne maplandi.
            var accountEntity = _mapper.Map<Account>(createUserVM);
            //Kullanıcının parolasını şifreleyerek kaydedelim.
            accountEntity.Password = CipherUtils
                .EncryptString(_configuration["AppSettings:SecretKey"], accountEntity.Password);

            accountEntity.Customer = customerEntity;

            _uWork.GetRepository<Customer>().add(customerEntity);
            _uWork.GetRepository<Account>().add(accountEntity);
            result.Data = await _uWork.ComitAsync();

            return result;
        }
        
        //public async Task<Result<TokenDto>> Login(LoginVM loginVM)
        //{


        //   //var existsUsers= await _db.GetRepository<Account>()
        //   //     .GetByFilterAsync(x=>x.Username.Trim().ToUpper()==loginVM.Username.Trim().ToUpper()
        //   //    && loginVM.Password == CipherUtils.DecryptString(_configuration["AppSettings:SecretKey"],x.Password));

        //   // if(!existsUsers.Any())
        //   // {

        //   // }
        //}
    }
}
