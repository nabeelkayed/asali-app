using Microsoft.AspNetCore.Http;
using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GP.Core.Models
{
    public class BusinessProfileSetupDto
    {
        public string BusinessName { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Category { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string MenuWebsite { get; set; }
        public string Bio { get; set; }
        public IFormFile CoverPhoto { get; set; }
        public List<string> Services { get; set; }
        public List<OpenDay> OpenDays { get; set; }
    }
}
