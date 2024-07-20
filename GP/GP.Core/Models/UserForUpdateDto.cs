using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Models
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IFormFile PhotoFile { get; set; }
        public string HeadLine { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        
        //public string Lon { get; set; }
        //public string Lat { get; set; }
    }
}