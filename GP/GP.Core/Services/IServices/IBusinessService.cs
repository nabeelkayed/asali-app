﻿using System;
using System.Collections.Generic;
using RealWord.Utils.ResourceParameters;
using System.Threading.Tasks;
using RealWord.Core.Models;
using GP.Core.Models;
using RealWord.Data.Entities;

namespace GP.Core.Services
{
    public interface IBusinessService
    {
        Task<bool> BusinessExistsAsync(Guid businessId);
        Task<bool> IsAuthorized(Guid businessId);
        Task<BusinessDto> LoginBusinessAsync(BusinessLoginDto BusinessLogin);
        Task<BusinessBusinessProfileDto> GetCurrentBusinessAsync();
        //Task<Guid> GetCurrentBusinessIdAsync(string businessUsername);
        Task<BusinessProfileDto> GetBusinessProfileAsync(Guid businessId);
        Task<IEnumerable<BusinessProfileDto>> GetBusinessesAsync(BusinessesParameters businessesParameters);
        Task<List<PhotoDto>> GetPhotosForBusinessAsync(Guid businessId);
        Task<List<UserProfileDto>> GetFollowersForBusinessAsync(Guid businessId);
        Task<bool> CreateBusinessAsync(BusinessForCreationDto businessForCreation);
        Task<bool> SetupBusinessProfileAsync(BusinessProfileSetupDto businessProfileSetup);
        Task<bool> UpdateBusinessProfileAsync(BusinessProfileForUpdateDto businessProfileForUpdate);
        Task<bool> UpdateBusinessAsync(BusinessForUpdateDto businessForUpdate);
        Task<bool> UpdateBusinessPasswordAsync(BusinessForUpdatePasswordDto businessForUpdatePassword);
        Task<bool> DeleteBusinessAsync(Guid businessId);
        Task<bool> FollowBusinessAsync(Guid businessId);
        Task<bool> UnfollowBusinessAsync(Guid businessId);
        Task<DashDto> GetDash(Guid businessId);
    }
}