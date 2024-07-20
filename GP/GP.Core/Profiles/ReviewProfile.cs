using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealWord.Data.Entities;
using RealWord.Core.Models;

namespace RealWord.Core.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                    .ForMember(
                    dest => dest.FunnyCount,
                    opt => opt.MapFrom(src => src.Funny.Count()))
                    .ForMember(
                    dest => dest.CoolCount,
                    opt => opt.MapFrom(src => src.Cool.Count()))
                    .ForMember(
                    dest => dest.UsefulCount,
                    opt => opt.MapFrom(src => src.Useful.Count()))
                    .ForMember(
                    dest => dest.BusinessName,
                    opt => opt.MapFrom(src => src.Business.BusinessName))
                   .ForMember(
                    dest => dest.Cool,
                    opt => opt.MapFrom((src, dest, destMember, context) => src.Cool.Select(s => s.UserId).ToList()
                              .Contains((Guid)context.Items["currentUserId"])))
                   .ForMember(
                    dest => dest.Funny,
                    opt => opt.MapFrom((src, dest, destMember, context) => src.Funny.Select(s => s.UserId).ToList()
                              .Contains((Guid)context.Items["currentUserId"])))
                   .ForMember(
                    dest => dest.Useful,
                    opt => opt.MapFrom((src, dest, destMember, context) => src.Useful.Select(s => s.UserId).ToList()
                              .Contains((Guid)context.Items["currentUserId"])))
                    .ForMember(
                    dest => dest.UserFirstName,
                    opt => opt.MapFrom(src => src.User.FirstName))
                    .ForMember(
                    dest => dest.UserLastName,
                    opt => opt.MapFrom(src => src.User.LastName))
                    .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.User.Username))
                    .ForMember(
                    dest => dest.UserPhoto,
                    opt => opt.MapFrom(src => src.User.Photo))
               /*     .ForMember(
                    dest => dest.Photos,
                    opt => opt.MapFrom(src => src.Photos.Select(c=>c.PhotoName).ToList()))*/;

            CreateMap<Review, ReviewBusinessDto>()
                  .ForMember(
                  dest => dest.FunnyCount,
                  opt => opt.MapFrom(src => src.Funny.Count()))
                  .ForMember(
                  dest => dest.CoolCount,
                  opt => opt.MapFrom(src => src.Cool.Count()))
                  .ForMember(
                  dest => dest.UsefulCount,
                  opt => opt.MapFrom(src => src.Useful.Count()))
                  .ForMember(
                  dest => dest.BusinessName,
                  opt => opt.MapFrom(src => src.Business.BusinessName))
                  .ForMember(
                  dest => dest.UserFirstName,
                  opt => opt.MapFrom(src => src.User.FirstName))
                  .ForMember(
                  dest => dest.UserLastName,
                  opt => opt.MapFrom(src => src.User.LastName))
                  .ForMember(
                  dest => dest.Username,
                  opt => opt.MapFrom(src => src.User.Username))
                  .ForMember(
                  dest => dest.UserPhoto,
                  opt => opt.MapFrom(src => src.User.Photo));

            CreateMap<Photo, PhotoDto>()
           .ForMember(
              dest => dest.PhotoName,
              opt => opt.MapFrom(src => src.PhotoName));
            CreateMap<ReviewForCreationDto, Review>()
                ;
        }
    }
}