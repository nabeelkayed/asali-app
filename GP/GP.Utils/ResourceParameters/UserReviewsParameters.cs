using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Utils.ResourceParameters
{
    public class UserReviewsParameters
    {
        //public string Category { get; set; } = "";//search
        public int Rate { get; set; } = 0;//search
        //public bool Trendy { get; set; } = false;//sort
        public bool Resent { get; set; } = true; //sort
        public bool Positive { get; set; } = false; //search
       // public bool Neutral { get; set; } = false; //search
        public bool Negative { get; set; } = false; //search


        // public int Limit { get; set; } = 20;
        //public int Offset { get; set; } = 0;
    }
}
