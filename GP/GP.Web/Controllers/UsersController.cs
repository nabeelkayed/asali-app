using GP.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWord.Core.Models;
using RealWord.Core.Services;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GP.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _IUserService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UsersController(IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            _IUserService = userService ??
               throw new ArgumentNullException(nameof(userService));

            _hostEnvironment = hostEnvironment ??
               throw new ArgumentNullException(nameof(hostEnvironment));
        }

        [AllowAnonymous]
        [HttpPost("users/login")]
        public async Task<ActionResult<UserDto>> UserLogin(UserLoginDto userLogin)
        {
            var logedinUserToReturn = await _IUserService.LoginUserAsync(userLogin);
            if (logedinUserToReturn != null)
            {
                return Ok(logedinUserToReturn);
            }

            return NotFound(new { messege = "The email or the password is not correct" });
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserProfileDto>> GetCurrentUser()
        {
            var userToReturn = await _IUserService.GetCurrentUserAsync();
            if (userToReturn == null)
            {
                return Ok(userToReturn);
            }

            //will not enter becouse we don't call this api unless we loged in
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet("user/{username}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(string username)
        {
            var userToReturn = await _IUserService.GetUserProfileAsync(username);
            if (userToReturn != null)
            {
                if (userToReturn.Photo != null)
                {
                    userToReturn.Photo = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, userToReturn.Photo);
                }

                foreach (PhotoDto photo in userToReturn.Photos)
                {
                    photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                }

                return Ok(userToReturn);
            }

            return NotFound(new { messege = "The user does not exist" });
        }

        [AllowAnonymous]
        [HttpGet("user/{username}/reviews")]
        public async Task<ActionResult<UserProfileDto>> GetUserReviews(string username, [FromQuery] UserReviewsParameters userReviewsParameters)
        {
            var reviewsToReturn = await _IUserService.GetUserReviewsAsync(username, userReviewsParameters);
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

        //for delete
        [AllowAnonymous]
        [HttpGet("user/{username}/photos")]
        public async Task<ActionResult<List<string>>> GetUserPhotos(string username)
        {
            var imageNames = await _IUserService.GetUserPhotos(username);

            if (imageNames.Count() != 0)
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

        [AllowAnonymous]
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(UserForCreationDto userForCreation)
        {
            bool userCreated = await _IUserService.CreateUserAsync(userForCreation);
            if (userCreated)
            {
                return Ok();
            }

            return NotFound(new { messege = "The email is alredy used" });
        }

        //for delete
        [HttpPost("user/photo")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        {
            string imageName = await SaveImage(imageFile);

            var uploaded = await _IUserService.UploadImage(imageName);
            if (uploaded)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut("user/photo")]
        public async Task<IActionResult> UpdateImage(string imageName, [FromForm] IFormFile imageFile)
        {
            DeleteImage(imageName);

            string imageName1 = await SaveImage(imageFile);

            var uploaded = await _IUserService.UploadImage(imageName);
            if (uploaded)
            {
                return Ok();
            }

            return Ok();

            // return NotFound();
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
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

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromForm] UserForUpdateDto userForUpdate)
        {
            DeleteImage(userForUpdate.Photo);

            string imageName1 = await SaveImage(userForUpdate.PhotoFile);

            userForUpdate.Photo = imageName1;

            bool userUpdated = await _IUserService.UpdateUserAsync(userForUpdate);
            if (userUpdated)
            {
                return Ok();
            }

            return NotFound(new { messege = "The email dose not exist" });
        }

        [HttpPut("user/password")]
        public async Task<IActionResult> UpdateUserPassword(UserForUpdatePasswordDto userForUpdatePassword)
        {
            bool userPasswordUpdated = await _IUserService.UpdateUserPasswordAsync(userForUpdatePassword);
            if (userPasswordUpdated)
            {
                return Ok();
            }

            return NotFound(new { messege = "The old password in not correct" });
        }
    }
}