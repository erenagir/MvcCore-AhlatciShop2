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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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



        [ValidationBehavior(typeof(LoginValidator))]
        public async Task<Result<TokenDto>> Login(LoginVM loginVM)
        {
            var result = new Result<TokenDto>();
            var hashedPassword = CipherUtils.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.Password);

            var existsUser = await _uWork.GetRepository<Account>().GetSingleByFilterAsync(x => x.Username.Trim().ToUpper() == loginVM.Username.Trim().ToUpper()
            && x.Password == hashedPassword);
            
            if (existsUser is null)
            {
                throw new NotFoundException($"{loginVM.Username} kullanıcı adı bulunamadı yada şifre yanlış");
            }
            var existsCustomer = await _uWork.GetRepository<Customer>().GetById(existsUser.CustomerId);

            var expireMinute = Convert.ToInt32(_configuration["Jwt:Expire"]);

            var expireDate = DateTime.Now.AddMinutes(expireMinute);

            // token üret ve return et
            var tokenString = GenerateJwtToken(existsUser, expireDate,existsCustomer);
            result.Data = new TokenDto
            {
                Token=tokenString,
                Expiredate=expireDate,
            };
            return result;
        }

        [ValidationBehavior(typeof(ReisterValidator))]
        public async Task<Result<bool>> Reister(ReisterVM createUserVM)
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

            _uWork.GetRepository<Customer>().Add(customerEntity);
            _uWork.GetRepository<Account>().Add(accountEntity);
            result.Data = await _uWork.ComitAsync();

            return result;
        }

       


        private string GenerateJwtToken(Account account, DateTime expireDate,Customer customer)
        {
            var secretkey = _configuration["Jwt:SigningKey"];
            var ıssuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audiance"];




            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretkey); // appsettings.json içinde JWT ayarlarınızı yapmalısınız

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Issuer = ıssuer,

                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,account.Username),
                    new Claim(ClaimTypes.Role,account.Role.ToString()),
                    new Claim(ClaimTypes.Email,customer.Email),
                    
                    new Claim(ClaimTypes.Surname,customer.Surname),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
