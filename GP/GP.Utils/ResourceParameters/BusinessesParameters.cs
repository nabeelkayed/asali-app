﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Utils.ResourceParameters
{
    public class BusinessesParameters
    {
        public string Category { get; set; } = "";
        public int AvgRate { get; set; } = 0;
        public bool Near { get; set; } = false;
        public bool MostlyReviewd { get; set; } = false;
        public string Username { get; set; } = "";
        public string Search { get; set; } = "";
        public bool Followed { get; set; } = false;
        public double Lon { get; set; }
        public double Lat { get; set; }

        //  public string Followed { get; set; }
        //  public int Limit { get; set; } = 20;
        // public int Offset { get; set; } = 0;
    }
}
