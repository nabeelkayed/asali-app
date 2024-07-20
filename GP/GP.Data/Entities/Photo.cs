using System;
using System.Collections.Generic;
using System.Text;

namespace RealWord.Data.Entities
{
    public class Photo
    {
        public Guid PhotoId { get; set; }
        public string PhotoName { get; set; }

        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ReviewId { get; set; }
        public Review Review { get; set; }
    }
}