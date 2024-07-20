using GP.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealWord.Core.Models;
using RealWord.Core.Services;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GP.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/businesses")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _IReviewService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReviewsController(IReviewService reviewService, IWebHostEnvironment hostEnvironment)
        {
            _IReviewService = reviewService ??
                throw new ArgumentNullException(nameof(reviewService));
            _hostEnvironment = hostEnvironment ??
                throw new ArgumentNullException(nameof(hostEnvironment));
        }

        [AllowAnonymous] //othenticational optinal or just in the business side
        [HttpGet("{businessId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewBusinessDto>>> GetReviewsForBusiness(Guid businessId)
        {
            var reviewsToReturn = await _IReviewService.GetReviewsForBusinessAsync(businessId);
            if (reviewsToReturn != null)
            {
                if (reviewsToReturn.Count() > 0)
                {
                    foreach (ReviewBusinessDto review in reviewsToReturn)
                    {
                        if (review.UserPhoto != null)
                        {
                            review.UserPhoto = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, review.UserPhoto);
                        }

                        foreach (PhotoDto photo in review.Photos)
                        {
                            photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                        }
                    }

                    return Ok(reviewsToReturn);
                }

            }

            return NotFound();
        }

        [HttpGet("feedreviews")]//shoud go to the user controler
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetFeedReviews([FromQuery] FeedReviewsParameters feedReviewsParameters)
        {
            var reviewsToReturn = await _IReviewService.GetFeedReviewsAsync(feedReviewsParameters);
            if (reviewsToReturn != null)
            {

                if (reviewsToReturn.Count() > 0)
                {
                    foreach (ReviewDto review in reviewsToReturn)
                    {
                        if (review.UserPhoto != null)
                        {
                            review.UserPhoto = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, review.UserPhoto);
                        }

                        foreach (PhotoDto photo in review.Photos)
                        {
                            photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                        }
                    }

                    return Ok(reviewsToReturn);
                }
            }
            return NotFound();
        }

        [HttpGet("{businessId}/businessreviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetBusinessReviews(Guid businessId, [FromQuery] BusinessReviewsParameters businessReviewsParameters)
        {
            var reviewsToReturn = await _IReviewService.GetBusinessReviewsAsync(businessId,businessReviewsParameters);
            if (reviewsToReturn.Count() > 0)
            {
                foreach (ReviewDto review in reviewsToReturn)
                {
                    if (review.UserPhoto != null)
                    {
                        review.UserPhoto = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, review.UserPhoto);
                    }

                    foreach (PhotoDto photo in review.Photos)
                    {
                        photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                    }
                }

                return Ok(reviewsToReturn);
            }

            return NotFound();
        }


        [HttpPost("{businessId}/reviews")]
        public async Task<IActionResult> CreateReviewForBusiness(Guid businessId,[FromForm] ReviewForCreationDto reviewForCreationDto)
        {

            foreach(IFormFile photo in reviewForCreationDto.PhotoFiles)
            {
                var imageName = await SaveImage(photo);
                reviewForCreationDto.Photos1.Add(imageName);
            }

            reviewForCreationDto.Sentement = "Positive";
            /*ReviewText r = new ReviewText();
            r.text = reviewForCreationDto.ReviewText;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(r), Encoding.UTF8, "application/json");
                
                string endpoint = "http://192.168.43.163:5000/predict";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        reviewForCreationDto.Sentement = Response.Content.ReadAsStringAsync().Result;
                    }
                }
            }*/

            /*  using (var client = new HttpClient())
              {
                  client.BaseAddress = new Uri("http://192.168.1.8:5000/predict");

                  //HTTP POST
                  var postTask = client.PostAsync("student", student);
                  { "text":"bad"}
                  postTask.Wait();

                  var result = postTask.Result;
                  if (result.IsSuccessStatusCode)
                  {
                      return RedirectToAction("Index");
                  }
              }*/

            bool created =await _IReviewService.CreateReviewForBusinessAsync(businessId, reviewForCreationDto);
            
            if (created)
            {
                return Ok();
            }

            return NotFound();
        }
  

        [HttpDelete("{businessId}/reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            var isAuthorized = await _IReviewService.IsAuthorized(businessId, reviewId);
            if (!isAuthorized)
            {
                return Forbid();
            }

            await _IReviewService.DeleteReviewAsync(reviewId);
            return NoContent();
        }


        [HttpPost("{businessId}/reviews/{reviewId}/getimages")]
        public async Task<ActionResult<IEnumerable<string>>> GetImages(Guid businessId, Guid reviewId)
        {
            var imageNames = await _IReviewService.GetImages(reviewId);

            if (imageNames.Count()!=0)
            {
                var tt = new List<string>();
                foreach (string imageName in imageNames)
                {
                    var ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, imageName);
                    tt.Add(ImageSrc);
                }

                return Ok(tt);
            }

            return NotFound();
        }

        [HttpPost("{businessId}/reviews/{reviewId}/addimages")]
        public async Task<IActionResult> UploadImage(Guid businessId ,Guid reviewId, [FromForm] IFormFile imageFile)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            string imageName = await SaveImage(imageFile);

            var uploaded = await _IReviewService.UploadImage(businessId, reviewId, imageName);
            if (uploaded)
            {
                return Ok();
            }

            return NotFound();
        }

        [NonAction]
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
        [HttpPost("{businessId}/reviews/{reviewId}/cool")]
        public async Task<IActionResult> CoolReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.CoolReviewAsync(reviewId);
            return Ok();
        }

        [HttpDelete("{businessId}/reviews/{reviewId}/cool")]
        public async Task<IActionResult> UnCoolReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.UncoolReviewAsync(reviewId);
            return Ok();
        }

        [HttpPost("{businessId}/reviews/{reviewId}/useful")]
        public async Task<IActionResult> UsefulReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.UsefulReviewAsync(reviewId);
            return Ok();
        }

        [HttpDelete("{businessId}/reviews/{reviewId}/useful")]
        public async Task<IActionResult> UnUsefulReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.UnusefulReviewAsync(reviewId);
            return Ok();
        }

        [HttpPost("{businessId}/reviews/{reviewId}/funny")]
        public async Task<IActionResult> FunnyReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.FunnyReviewAsync(reviewId);
            return Ok();
        }

        [HttpDelete("{businessId}/reviews/{reviewId}/funny")]
        public async Task<IActionResult> UnFunnyReview(Guid businessId, Guid reviewId)
        {
            var isReviewExists = await _IReviewService.ReviewExistsAsync(businessId, reviewId);
            if (!isReviewExists)
            {
                return NotFound();
            }

            await _IReviewService.UnfunnyReviewAsync(reviewId);
            return Ok();
        }
    }
}