using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealWord.Data.Entities;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GPDbContext _context;

        public ReviewRepository(GPDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ReviewExistsAsync(Guid reviewId)
        {
            if (reviewId == null || reviewId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(reviewId));
            }

            bool reviewExists = await _context.Reviews.AnyAsync(c => c.ReviewId == reviewId);
            return reviewExists;
        }

        public async Task<Review> GetReviewAsync(Guid reviewId)
        {
            if (reviewId == null || reviewId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(reviewId));
            }

            var review = await _context.Reviews.Include(r => r.User)
                                                 .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            return review;
        }

        public async Task<List<Review>> GetReviewsForBusinessAsync(Guid businessId)
        {
            var businessReviews = await _context.Reviews.Where(c => c.BusinessId == businessId)
                                                   .Include(c => c.Business)
                                                   .Include(c => c.User)
                                                   .Include(c => c.Useful)
                                                   .Include(c => c.Cool)
                                                   .Include(c => c.Funny)
                                                   .Include(c => c.Photos)
                                                   //.ThenInclude(c => c.Followings)
                                                   .ToListAsync();
            return businessReviews;
        }

        public async Task<List<Review>> GetFeedReviewsAsync(Guid currentUserId, FeedReviewsParameters feedReviewsParameters)
        {
            var userFollowings = await _context.BusinessFollowers.Where(u => u.UserId == currentUserId)
                                                                 .Select(u => u.BusinessId)
                                                                 .ToListAsync();
            if (!userFollowings.Any())
            {
                return null;
            }

            var feedReviews = _context.Reviews.Include(r => r.User)
                                                .Include(r => r.Business)
                                                .Include(r => r.Cool)
                                                .Include(r => r.Useful)
                                                .Include(c => c.Photos)
                                                .Include(r => r.Funny)
                                                .Where(r => userFollowings.Contains(r.BusinessId))
                                                .AsQueryable();

            if (feedReviewsParameters.Category != "null")
            {
                var category = feedReviewsParameters.Category.Trim();
                feedReviews = feedReviews.Where(b => b.Business.Category == category);
            }

            if (feedReviewsParameters.Rate != 0)
            {
                feedReviews = feedReviews.Where(b => b.Rate == feedReviewsParameters.Rate);
            }

            if (feedReviewsParameters.Positive && feedReviewsParameters.Negative)
            {
                feedReviews = feedReviews.Where(b => b.Sentement == "Positive" || b.Sentement == "Negative");
            }
            else if (feedReviewsParameters.Negative)
            {
                feedReviews = feedReviews.Where(b => b.Sentement == "Negative");
            }
            else if (feedReviewsParameters.Positive)
            {
                feedReviews = feedReviews.Where(b => b.Sentement == "Positive");
            }

            if (feedReviewsParameters.Trendy)
            {
                feedReviews = feedReviews.Where(r => r.CreatedAt > DateTime.Now.AddDays(-7));
                feedReviews = feedReviews.OrderByDescending(r => r.Funny.Count() + r.Cool.Count() + r.Useful.Count()).Take(10);
            }

            if (feedReviewsParameters.Resent != "null")
            {
                switch (feedReviewsParameters.Resent)
                {
                    case "Resent":
                        feedReviews = feedReviews.OrderByDescending(r => r.CreatedAt);
                        break;
                    case "Rate":
                        feedReviews = feedReviews.OrderByDescending(r => r.Rate);
                        break;
                    case "Oldest":
                        feedReviews = feedReviews.OrderBy(r => r.CreatedAt);
                        break;
                    case "Useful":
                        feedReviews = feedReviews.OrderByDescending(r => r.Useful.Count());
                        break;
                    case "Cool":
                        feedReviews = feedReviews.OrderByDescending(r => r.Cool.Count());
                        break;
                    case "Funny":
                        feedReviews = feedReviews.OrderByDescending(r => r.Funny.Count());
                        break;
                }
            }

            var reviews = await feedReviews.ToListAsync();

            return reviews;
        }

        public async Task<List<Review>> GetBusinessReviewsAsync(Guid businessId, BusinessReviewsParameters businessReviewsParameters)
        {
            var businessReviews = _context.Reviews.Include(r => r.User)
                                                .Include(r => r.Business)
                                                .Include(r => r.Cool)
                                                .Include(r => r.Useful)
                                                .Include(c => c.Photos)
                                                .Include(r => r.Funny)
                                                .Where(r => r.BusinessId == businessId).AsQueryable();


            if (businessReviewsParameters.Service != "null")
            {
                var service = businessReviewsParameters.Service.Trim();
                businessReviews = businessReviews.Where(b => b.Service == service);
            }

            if (businessReviewsParameters.Rate != 0)
            {
                businessReviews = businessReviews.Where(b => b.Rate == businessReviewsParameters.Rate);
            }

            if (businessReviewsParameters.Positive && businessReviewsParameters.Negative)
            {
                businessReviews = businessReviews.Where(b => b.Sentement == "Positive" || b.Sentement == "Negative");
            }
            else if (businessReviewsParameters.Negative)
            {
                businessReviews = businessReviews.Where(b => b.Sentement == "Negative");
            }
            else if (businessReviewsParameters.Positive)
            {
                businessReviews = businessReviews.Where(b => b.Sentement == "Positive");
            }


            if (businessReviewsParameters.Trendy)
            {
                businessReviews = businessReviews.Where(r => r.CreatedAt > DateTime.Now.AddDays(-7));
                businessReviews = businessReviews.OrderByDescending(r => r.Funny.Count() + r.Cool.Count() + r.Useful.Count());
            }

            if (businessReviewsParameters.Resent != "null")
            {
                switch (businessReviewsParameters.Resent)
                {
                    case "Resent":
                        businessReviews = businessReviews.OrderByDescending(r => r.CreatedAt);
                        break;
                    case "Rate":
                        businessReviews = businessReviews.OrderByDescending(r => r.Rate);
                        break;
                    case "Oldest":
                        businessReviews = businessReviews.OrderBy(r => r.CreatedAt);
                        break;
                    case "Useful":
                        businessReviews = businessReviews.OrderByDescending(r => r.Useful.Count());
                        break;
                    case "Cool":
                        businessReviews = businessReviews.OrderByDescending(r => r.Cool.Count());
                        break;
                    case "Funny":
                        businessReviews = businessReviews.OrderByDescending(r => r.Funny.Count());
                        break;
                }
            }

            var reviews = await businessReviews.ToListAsync();

            return reviews;
        }

        public async Task CreateReviewAsync(Guid businessId, Guid currentUserId, Review review, List<string> Photos)
        {
            review.ReviewId = Guid.NewGuid();

            review.UserId = currentUserId;

            review.BusinessId = businessId;

            var timeStamp = DateTime.Now;
            review.CreatedAt = timeStamp;

            foreach (string photo in Photos)
            {
                var photo1 =new Photo { PhotoName = photo, ReviewId = review.ReviewId, 
                    UserId = review.UserId, BusinessId = review.BusinessId };
                await _context.Photos.AddAsync(photo1);
            }

            await _context.Reviews.AddAsync(review);
        }

        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
        }

        public async Task CoolReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var coolReview =
                new ReviewCool { ReviewId = reviewId, UserId = currentUserId };
            await _context.ReviewCool.AddAsync(coolReview);
        }

        public void UncoolReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var unCoolReview =
                new ReviewCool { ReviewId = reviewId, UserId = currentUserId };
            _context.ReviewCool.Remove(unCoolReview);
        }

        public async Task<bool> IsCoolAsync(Guid currentUserId, Guid reviewId)
        {
            var isCool =
                         await _context.ReviewCool.AnyAsync(af => af.UserId == currentUserId && af.ReviewId == reviewId);
            return isCool;
        }

        public async Task UsefulReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var usfulReview =
                new ReviewUseful { ReviewId = reviewId, UserId = currentUserId };
            await _context.ReviewUseful.AddAsync(usfulReview);
        }

        public void UnusfulReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var unUsfulReview =
                new ReviewUseful { ReviewId = reviewId, UserId = currentUserId };
            _context.ReviewUseful.Remove(unUsfulReview);
        }

        public async Task<bool> IsUsefulAsync(Guid currentUserId, Guid reviewId)
        {
            var isUsful =
                await _context.ReviewUseful.AnyAsync(af => af.UserId == currentUserId && af.ReviewId == reviewId);
            return isUsful;
        }

        public async Task FunnyReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var funnyReview =
                new ReviewFunny { ReviewId = reviewId, UserId = currentUserId };
            await _context.ReviewFunny.AddAsync(funnyReview);
        }

        public void UnfunnyReviewAsync(Guid currentUserId, Guid reviewId)
        {
            var unFunnyReview =
                new ReviewFunny { ReviewId = reviewId, UserId = currentUserId };
            _context.ReviewFunny.Remove(unFunnyReview);
        }

        public async Task<bool> IsFunnyAsync(Guid currentUserId, Guid reviewId)
        {
            var isFunny =
                await _context.ReviewFunny.AnyAsync(af => af.UserId == currentUserId && af.ReviewId == reviewId);
            return isFunny;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UploadImage(Guid businessId, Guid reviewId, Guid userId, string imageName)
        {
            var photo =
                         new Photo
                         {
                             UserId = userId,
                             ReviewId = reviewId,
                             BusinessId = businessId,
                             PhotoName = imageName
                         };
            await _context.Photos.AddAsync(photo);
        }

        public async Task<List<string>> GetImages(Guid reviewId)
        {
            return await _context.Photos.Where(c => c.ReviewId == reviewId)
                           .Select(c => c.PhotoName).ToListAsync();
        }
    }
}
