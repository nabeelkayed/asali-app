using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Models
{
    public class DashDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }
        public int Rate4 { get; set; }
        public int Rate5 { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
        public List<string> Services { get; set; }
        public List<int> ServicesReviews { get; set; }
        public List<int> ServicesPositive { get; set; }
        public List<int> ServicesNegative { get; set; }


    }
}