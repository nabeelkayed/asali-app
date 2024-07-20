﻿using AutoMapper;
using GP.Core.Models;
using GP.Core.Services;
using Microsoft.AspNetCore.Http;
using RealWord.Core.Models;
using RealWord.Data.Entities;
using RealWord.Data.Repositories;
using RealWord.Utils.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWord.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _IReviewRepository;
        private readonly IBusinessRepository _IBusinessRepository;
        private readonly IBusinessService _IBusinessService;
        private readonly IUserService _IUserService;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IBusinessRepository businessRepository,
        IBusinessService businessService, IUserService userService, IMapper mapper)
        {
            _IReviewRepository = reviewRepository ??
               throw new ArgumentNullException(nameof(reviewRepository));
            _IBusinessRepository = businessRepository ??
               throw new ArgumentNullException(nameof(businessRepository));
            _IBusinessService = businessService ??
               throw new ArgumentNullException(nameof(businessService));
            _IUserService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ReviewExistsAsync(Guid businessId, Guid reviewId)
        {
            var businessExists = await _IBusinessService.BusinessExistsAsync(businessId);
            if (!businessExists)
            {
                return false;
            }

            var reviewExists = await _IReviewRepository.ReviewExistsAsync(reviewId);
            if (!reviewExists)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsAuthorized(Guid businessId, Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            var currentUserId = await _IUserService.GetCurrentUserIdAsync();

            var isAuthorized = currentUserId == review.UserId;
            return isAuthorized;
        }

        public async Task<ReviewDto> GetReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return null;
            }

            var reviewToReturn = MapReview(Guid.Empty, review);
            return reviewToReturn;
        }

        public async Task<IEnumerable<ReviewBusinessDto>> GetReviewsForBusinessAsync(Guid businessId)
        {
            var reviews = await _IReviewRepository.GetReviewsForBusinessAsync(businessId);
            if (reviews != null)
            {
                var reviewsToReturn = _mapper.Map<List<ReviewBusinessDto>>(reviews);

                return reviewsToReturn;
            }

            return null;

        
        }

        public async Task<IEnumerable<ReviewDto>> GetFeedReviewsAsync(FeedReviewsParameters feedReviewsParameters)
        {
            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var reviews = await _IReviewRepository.GetFeedReviewsAsync(currentUserId, feedReviewsParameters);
            if (reviews != null)
            {
                if (!reviews.Any())
                {
                    return null;
                }

                var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews, a => a.Items["currentUserId"] = currentUserId);

                return reviewsDto;
            }
                return null;
        }

        public async Task<IEnumerable<ReviewDto>> GetBusinessReviewsAsync(Guid busienssId, BusinessReviewsParameters businessReviewsParameters)
        {
            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var reviews = await _IReviewRepository.GetBusinessReviewsAsync(busienssId, businessReviewsParameters);
            if (!reviews.Any())
            {
                return null;
            }

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews, a => a.Items["currentUserId"] = currentUserId);

            return reviewsDto;
        }


        public async Task<bool> CreateReviewForBusinessAsync(Guid businessId, ReviewForCreationDto reviewForCreationDto)
        {
            /*  var businessId = await _IBusinessService.GetCurrentBusinessIdAsync(businessId);
              if (businessId == Guid.Empty)
              {
                  return false;
              }*/

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();

            var reviewEntityForCreation = _mapper.Map<Review>(reviewForCreationDto);

            await _IReviewRepository.CreateReviewAsync(businessId, currentUserId, reviewEntityForCreation, reviewForCreationDto.Photos1);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            _IReviewRepository.DeleteReview(review);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CoolReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isCool = await _IReviewRepository.IsCoolAsync(currentUserId, review.ReviewId);
            if (isCool)
            {
                return false;
            }

            await _IReviewRepository.CoolReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UncoolReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isCool = await _IReviewRepository.IsCoolAsync(currentUserId, review.ReviewId);
            if (!isCool)
            {
                return false;
            }

            _IReviewRepository.UncoolReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UsefulReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isUseful = await _IReviewRepository.IsUsefulAsync(currentUserId, review.ReviewId);
            if (isUseful)
            {
                return false;
            }

            await _IReviewRepository.UsefulReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnusefulReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isUseful = await _IReviewRepository.IsUsefulAsync(currentUserId, review.ReviewId);
            if (!isUseful)
            {
                return false;
            }

            _IReviewRepository.UnusfulReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FunnyReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isFunny = await _IReviewRepository.IsFunnyAsync(currentUserId, review.ReviewId);
            if (isFunny)
            {
                return false;
            }

            await _IReviewRepository.FunnyReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnfunnyReviewAsync(Guid reviewId)
        {
            var review = await _IReviewRepository.GetReviewAsync(reviewId);
            if (review == null)
            {
                return false;
            }

            var currentUserId = await _IUserService.GetCurrentUserIdAsync();
            var isFunny = await _IReviewRepository.IsFunnyAsync(currentUserId, review.ReviewId);
            if (!isFunny)
            {
                return false;
            }

            _IReviewRepository.UnfunnyReviewAsync(currentUserId, review.ReviewId);
            await _IReviewRepository.SaveChangesAsync();

            return true;
        }
        private ReviewDto MapReview(Guid currentUserId, Review review)
        {
            var reviewDto = _mapper.Map<ReviewDto>(review);
            var profileDto = _mapper.Map<UserProfileDto>(review.User, a => a.Items["currentUserId"] = currentUserId);
           // reviewDto.User = profileDto;

            return reviewDto;
        }

        public async Task<bool> UploadImage(Guid busienssId, Guid reviewId, string imageName)
        {
            var userId = await _IUserService.GetCurrentUserIdAsync();
            await _IReviewRepository.UploadImage(busienssId, reviewId, userId, imageName);
            await _IReviewRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetImages(Guid reviewId)
        {
           var images = await _IReviewRepository.GetImages(reviewId);
           return images;
        }
    }
}