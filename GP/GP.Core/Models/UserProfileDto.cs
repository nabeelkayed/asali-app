using RealWord.Core.Models;
using RealWord.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GP.Core.Models
{
    public class UserProfileDto
    {
        public UserProfileDto()
        {
            Photos = new List<PhotoDto>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string HeadLine { get; set; }
        public string Bio { get; set; }
        //public string Lon { get; set; }
        //public string Lat { get; set; }
        public int PhotosCount { get; set; }
        public string FollowingsCount { get; set; }
        public string LastReviewDate { get; set; }

        public List<PhotoDto> Photos { get; set; }

        //public List<ReviewDto> Reviews { get; set; } 
        //public List<ReviewCool> ReviewCool { get; set; }
        //public List<ReviewUseful> ReviewUseful { get; set; }
    }
}