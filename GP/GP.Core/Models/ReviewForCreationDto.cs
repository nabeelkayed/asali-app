using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Models
{
    public class ReviewForCreationDto 
    {
        public ReviewForCreationDto()
        {
            Photos1 =  new List<string>();
        }

        public string ReviewText { get; set; }
        public int Rate { get; set; }
        public string Service { get; set; }
        public List<IFormFile> PhotoFiles { get; set; }
        public List<string> Photos1 { get; set; }
        public string Sentement { get; set; }
    }
}
