using AutoMapper;
using RealWord.Data.Entities;
using RealWord.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GP.Core.Models;

namespace RealWord.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserProfileDto>()
                  .ForMember(
                    dest => dest.LastReviewDate,
                    opt => opt.MapFrom(src => src.Reviews.OrderByDescending(c=>c.CreatedAt).FirstOrDefault().CreatedAt))
                  .ForMember(
                    dest => dest.FollowingsCount,
                    opt => opt.MapFrom(src => src.Followings.Count()))
                 /*  .ForMember(
                    dest => dest.Photos,
                    opt => opt.MapFrom(src => src.Photos.Select(c=>c.PhotoName).ToList()))*/;

            CreateMap<Photo, PhotoDto>()
                .ForMember(
                   dest => dest.PhotoName,
                   opt => opt.MapFrom(src => src.PhotoName));
            CreateMap<UserLoginDto, User>();//done
            CreateMap<UserForCreationDto, User>();//done
            CreateMap<UserForUpdateDto, User>();//done if we change location we should change it
            CreateMap<UserForUpdatePasswordDto, User>();
        }
    }
}
