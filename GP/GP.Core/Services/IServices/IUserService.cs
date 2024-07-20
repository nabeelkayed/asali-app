using GP.Core.Models;
using Microsoft.AspNetCore.Http;
using RealWord.Core.Models;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealWord.Core.Services
{
    public interface IUserService
    {
        Task<UserDto> LoginUserAsync(UserLoginDto userLogin);
        Task<UserProfileDto> GetCurrentUserAsync();
        Task<Guid> GetCurrentUserIdAsync();
        Task<UserProfileDto> GetUserProfileAsync(string username);
        Task <bool> CreateUserAsync(UserForCreationDto userForCreation);
        Task<bool> UpdateUserAsync(UserForUpdateDto userForUpdate);
        Task<bool> UpdateUserPasswordAsync(UserForUpdatePasswordDto userForUpdatePassword);
        Task<bool> UploadImage(string imageName);
        Task<List<string>> GetUserPhotos(string username);
        Task<List<ReviewDto>> GetUserReviewsAsync(string username , UserReviewsParameters userReviewsParameters);
    }
}