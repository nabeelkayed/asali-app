﻿using GP.Core.Models;
using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Models
{
    public class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public string ReviewText { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public string BusinessId { get; set; }
        public string BusinessName { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Username { get; set; }
        public string UserPhoto { get; set; }

        //public UserProfileDto User { get; set; }
        //public BusinessProfileDto Business { get; set; }

        public bool Cool { get; set; }
        public bool Useful { get; set; }
        public bool Funny { get; set; }

        public int CoolCount { get; set; }
        public int UsefulCount { get; set; }
        public int FunnyCount { get; set; }

        public List<PhotoDto> Photos { get; set; }
    }
}