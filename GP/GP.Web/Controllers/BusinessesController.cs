using GP.Core.Models;
using GP.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWord.Core.Models;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GP.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/businesses")]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessService _IBusinessService;

        public BusinessesController(IBusinessService businessService)
        {
            _IBusinessService = businessService ??
                throw new ArgumentNullException(nameof(businessService));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<BusinessDto>> BusnissLogin(BusinessLoginDto businessLogin)
        {
            var logedinbusinessToReturn = await _IBusinessService.LoginBusinessAsync(businessLogin);
            if (logedinbusinessToReturn != null)
            {
                return Ok(logedinbusinessToReturn);
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessForCreationDto businessForCreationDto)
        {
            bool createBusiness = await _IBusinessService.CreateBusinessAsync(businessForCreationDto);
            if (createBusiness)
            {
                return Ok();
            }

            return NotFound("The Business is exist");
        }

        [HttpPut("setup")]
        public async Task<IActionResult> BusnissProfileSetup(BusinessProfileSetupDto businessProfileSetup)
        {
            bool setupBusinessProfile = await _IBusinessService.SetupBusinessProfileAsync(businessProfileSetup);//الشركة ما بتبين الا لما يجهز البروفايل ونظهر اشي بدل على هيك
            if (setupBusinessProfile)
            {
                return Ok();
            }
            return NotFound();
        }

        [AllowAnonymous] //othintication optinal we should do it in maping
        [HttpGet("{businessId}/dash")]
        public async Task<ActionResult<DashDto>> GetBusinessdash(Guid businessId)
        {
            var dashtoreturn = await _IBusinessService.GetDash(businessId);
            return dashtoreturn;
        }
        [AllowAnonymous] //othintication optinal we should do it in maping
        [HttpGet("{businessId}")]
        public async Task<ActionResult<BusinessProfileDto>> GetBusinessProfile(Guid businessId)
        {
            var businessToReturn = await _IBusinessService.GetBusinessProfileAsync(businessId);
            if (businessToReturn == null)
            {
                return NotFound("The Business is not exist");
            }

            businessToReturn.CoverPhoto =String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, businessToReturn.CoverPhoto);

            if (businessToReturn.Photos.Count() != 0)
            {
                //var tt = new List<PhotoDto>();
                foreach (PhotoDto photo in businessToReturn.Photos)
                {
                    photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                    /*  var ImageSrc = 
                      tt.Add(ImageSrc);*/
                }
                //businessToReturn.Photos = tt;
            }

            return Ok(businessToReturn);
        }

        [HttpGet()]
        public async Task<ActionResult<BusinessProfileDto>> GetCurrentBusinessAsync()
        {
            var businessToReturn = await _IBusinessService.GetCurrentBusinessAsync();
            if (businessToReturn == null)
            {
                return NotFound();
            }

            return Ok(businessToReturn);
        }

        //[AllowAnonymous] //othentication optinal in the mabing we should do it
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BusinessProfileDto>>> GetBusinesses([FromQuery] BusinessesParameters businessesParameters)
        {
            var businessesToReturn = await _IBusinessService.GetBusinessesAsync(businessesParameters);
            if (businessesToReturn.Count() == 0)
            {
                return NotFound("There is no businesses match your search");
            }

            foreach(BusinessProfileDto business in businessesToReturn)
            {
                business.CoverPhoto = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, business.CoverPhoto);

            }

            return Ok(businessesToReturn);
        }

        [AllowAnonymous]//othentication optinal we should do it in maping
        [HttpGet("{businessId}/photos")]
        public async Task<ActionResult<IEnumerable<BusinessDto>>> GetPhotosForBusiness(Guid businessId)
        {
            var photos = await _IBusinessService.GetPhotosForBusinessAsync(businessId);
            if (photos.Count() > 0)
            {
                foreach (PhotoDto photo in photos)
                {
                    photo.PhotoName = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, photo.PhotoName);
                }

                return Ok(photos);
            }

            return NotFound();
        }
        [HttpGet("{businessId}/followers")]
        public async Task<ActionResult<UserProfileDto>> GetFollowersForBusiness(Guid businessId)
        {
            var followersToReturn = await _IBusinessService.GetFollowersForBusinessAsync(businessId);
            if (followersToReturn != null)
            {
                if (followersToReturn.Count() > 0)
                {
                    foreach (UserProfileDto follower in followersToReturn)
                    {
                        follower.Photo = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, follower.Photo);
                    }

                    return Ok(followersToReturn);

                }
            }


            return NotFound();

        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateBusinessProfileAsync(BusinessProfileForUpdateDto businessProfileForUpdate)
        {
            /*var isBusinessExists = await _IBusinessService.BusinessExistsAsync(businessUsername);
            if (!isBusinessExists)
            {
                return NotFound();
            }

            var isAuthorized = await _IBusinessService.IsAuthorized(businessUsername);
            if (!isAuthorized)
            {
                return Forbid();
            }*/

            bool updateBusinessProfile = await _IBusinessService.UpdateBusinessProfileAsync(businessProfileForUpdate);
            if (updateBusinessProfile)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateBusinessAsync(BusinessForUpdateDto businessForUpdate)
        {
            /*   var isBusinessExists = await _IBusinessService.BusinessExistsAsync(businessUsername);
               if (!isBusinessExists)
               {
                   return NotFound();
               }

               var isAuthorized = await _IBusinessService.IsAuthorized(businessUsername);
               if (!isAuthorized)
               {
                   return Forbid();
               }*/

            bool updateBusiness = await _IBusinessService.UpdateBusinessAsync(businessForUpdate);
            if (updateBusiness)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdateBusinessPassword(BusinessForUpdatePasswordDto businessForUpdatePassword)
        {
            bool updateBusinessPassword = await _IBusinessService.UpdateBusinessPasswordAsync(businessForUpdatePassword);
            if (updateBusinessPassword)
            {
                return Ok();

            }

            return NotFound();

        }

        [HttpDelete("{businessId}")]
        public async Task<IActionResult> DeleteBusiness(Guid businessId)
        {
            /*var isBusinessExists = await _IBusinessService.BusinessExistsAsync(businessId);
            if (!isBusinessExists)
            {
                return NotFound();
            }

            var isAuthorized = await _IBusinessService.IsAuthorized(businessId);
            if (!isAuthorized)
            {
                return Forbid();
            }*/

            bool deleteBusiness = await _IBusinessService.DeleteBusinessAsync(businessId);
            if (deleteBusiness)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("{businessId}/follow")]
        public async Task<IActionResult> FollowBusiness(Guid businessId)
        {
            bool followBusiness = await _IBusinessService.FollowBusinessAsync(businessId);
            if (followBusiness)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{businessId}/follow")]
        public async Task<IActionResult> UnfollowBusiness(Guid businessId)
        {
            bool unFollowBusiness = await _IBusinessService.UnfollowBusinessAsync(businessId);
            if (unFollowBusiness)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}