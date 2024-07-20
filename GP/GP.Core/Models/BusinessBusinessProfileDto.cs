using GP.Data.Entities;
using RealWord.Core.Models;
using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GP.Core.Models
{
    public class BusinessBusinessProfileDto
    {
        public BusinessBusinessProfileDto()
        {
            OpenDays = new List<OpenDayDto>();
            Photos = new List<PhotoDto>();
        }

        public Guid BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
        public string MenuWebsite { get; set; }
        public string Category { get; set; }
        public string PhoneNumber { get; set; }

        public int AvgRate { get; set; }
        public int RateCount { get; set; }

        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }
        public int Rate4 { get; set; }
        public int Rate5 { get; set; }

         
        public BusinessOwnerDto BusinessOwner { get; set; }
        public List<string> Services { get; set; }
        public List<OpenDayDto> OpenDays { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }
}