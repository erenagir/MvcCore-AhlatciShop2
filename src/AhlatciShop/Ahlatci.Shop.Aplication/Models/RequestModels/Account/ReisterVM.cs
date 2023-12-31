﻿using Ahlatci.Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Models.RequestModels.Account
{
    public class ReisterVM
    {
        public int CityId { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
     
        public string PasswordAgain { get; set; }
        
    }
}
