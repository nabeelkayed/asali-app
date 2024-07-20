using Microsoft.EntityFrameworkCore;
using RealWord.Data.Entities;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GPDbContext _context;

        public UserRepository(GPDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> UserExistsAsync(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            bool userExists = await _context.Users.AnyAsync(u => u.Username == username);
            return userExists;
        }

        public async Task<bool> EmailAvailableAsync(string email)
        {
            var emailNotAvailable = await _context.Users.Select(a => a.Email).ContainsAsync(email);
            return emailNotAvailable;
        }

        public async Task<User> LoginUserAsync(User user)
        {
            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email
                                                             && u.Password == user.Password);
            return loginUser;
        }

        public async Task<User> GetUserAsync(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var user = await _context.Users.Include(u => u.Reviews)
                                           .Include(u => u.Followings)
                                           .Include(u => u.Cool)
                                           .Include(u => u.Funny)
                                           .Include(u => u.Useful)
                                           .Include(u => u.Photos)
                                           .FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task<User> GetUserAsNoTrackingAsync(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Reviews)
                                           .Include(u => u.Followings)
                                           .Include(u => u.Cool)
                                           .Include(u => u.Funny)
                                           .Include(u => u.Useful)
                                           .Include(u => u.Photos)
                                           .FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void UpdateUser(User updatedUser, User userForUpdate)
        {

            if (!string.IsNullOrWhiteSpace(userForUpdate.FirstName))
            {
                updatedUser.FirstName = userForUpdate.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.LastName))
            {
                updatedUser.LastName = userForUpdate.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.Username))
            {
                updatedUser.Username = userForUpdate.Username.ToLower();
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.Email))
            {
                updatedUser.Email = userForUpdate.Email.ToLower();
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.Photo))
            {
                updatedUser.Photo = userForUpdate.Photo;
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.Bio))
            {
                updatedUser.Bio = userForUpdate.Bio;
            }

            if (!string.IsNullOrWhiteSpace(userForUpdate.HeadLine))
            {
                updatedUser.HeadLine = userForUpdate.HeadLine;
            }

           /* if (!string.IsNullOrWhiteSpace(userForUpdate.Lon))
            {
                updatedUser.Lon = userForUpdate.Lon;
            }
            if (!string.IsNullOrWhiteSpace(userForUpdate.Lat))
            {
                updatedUser.Lat = userForUpdate.Lat;
            }*/
        }

        public void UpdateUserPassword(User updatedUser)
        {

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddUserImage(string username,string imageName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            user.Photo = imageName;
        }

        public async Task<List<string>> GetUserPhotos(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var photos = await _context.Photos.Where(u => u.UserId == user.UserId).Select(a=>a.PhotoName).ToListAsync();
            return photos;
        }

        public async Task<List<Review>> GetUserReviewsAsync(string username, UserReviewsParameters userReviewsParameters)
        {
            var reviews =  _context.Reviews.Where(u => u.User.Username == username)
                                           .Include(r => r.User)
                                           .Include(r => r.Business)
                                           .Include(r => r.Cool)
                                           .Include(r => r.Useful)
                                           .Include(c => c.Photos)
                                           .Include(r => r.Funny).AsQueryable();

            if (userReviewsParameters.Rate != 0)
            {
                reviews = reviews.Where(b => b.Rate == userReviewsParameters.Rate);
            }

            if (userReviewsParameters.Negative)
            {
                reviews = reviews.Where(r=>r.Sentement == "Negative");
            }

            if (userReviewsParameters.Positive)
            {
                reviews = reviews.Where(r => r.Sentement == "Positive");
            }

            if (userReviewsParameters.Resent)
            {
                reviews = reviews.OrderByDescending(r=>r.CreatedAt);
            }
            else
            {
                reviews = reviews.OrderBy(r => r.CreatedAt);
            }

            if (reviews != null)
            {
                var allReviews = await reviews.ToListAsync();
                return allReviews;
            }

            return null;
        }
    }
}