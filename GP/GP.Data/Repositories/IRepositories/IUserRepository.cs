using RealWord.Data.Entities;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealWord.Data.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<bool> EmailAvailableAsync(string email);
        Task<User> LoginUserAsync(User user);
        Task<User> GetUserAsync(string username);
        Task<User> GetUserAsNoTrackingAsync(string username);
        Task CreateUserAsync(User user);
        void UpdateUser(User updatedUser, User userForUpdate);
        void UpdateUserPassword(User updatedUser);
        Task SaveChangesAsync();
        Task AddUserImage(string userName,string imageName);
        Task<List<string>> GetUserPhotos(string username);
        Task<List<Review>> GetUserReviewsAsync(string username, UserReviewsParameters userReviewsParameters);
    }
}