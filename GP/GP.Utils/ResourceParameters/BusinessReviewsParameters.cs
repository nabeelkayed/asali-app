using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Utils.ResourceParameters
{
    public class BusinessReviewsParameters
    {
        //public string Category { get; set; } = "";//search
        public int Rate { get; set; } = 0;//search
        public string Service { get; set; } = "";//search
        public bool Trendy { get; set; } = false;//sort
        public string Resent { get; set; } = ""; //sort
        public bool Positive { get; set; } = false; //search
       // public bool Neutral { get; set; } = false; //search
        public bool Negative { get; set; } = false; //search

        // public int Limit { get; set; } = 20;
        //public int Offset { get; set; } = 0;
    }
}
