using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Models
{
    public class OpenDayDto
    {
        public string Day { get; set; }       
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }

    }
}