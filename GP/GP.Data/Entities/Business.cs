using GP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealWord.Data.Entities
{
    public class Business
    {
        public Business()
        {
            Followers = new List<BusinessFollowers>();
            Reviews = new List<Review>();
            Photos = new List<Photo>();

            Services = new List<Service>();
            OpenDays = new List<OpenDay>();
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
        public string CoverPhoto { get; set; }


        public List<BusinessFollowers> Followers { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Service> Services { get; set; }
        public List<OpenDay> OpenDays { get; set; }

        public BusinessOwner BusinessOwner { get; set; }
    }
}