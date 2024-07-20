﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealWord.Data.Entities;
using RealWord.Data.Repositories;
using RealWord.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GP.Data.Entities;

namespace RealWord.Core.Auth
{
    public class BusinessAuth : IBusinessAuth
    {
        private readonly IConfiguration _config;

        public BusinessAuth(IConfiguration config)
        {
            _config = config ??
                throw new ArgumentNullException(nameof(config));
        }

        public string Generate(BusinessOwner businessOwner)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, businessOwner.FirstName),
                new Claim(ClaimTypes.Email, businessOwner.Email)
        };


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
