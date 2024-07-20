using AutoMapper;
using System;
using System.Linq;
using RealWord.Data.Entities;
using RealWord.Core.Models;
using GP.Data.Entities;
using GP.Core.Models;

namespace RealWord.Core.Profiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {

            CreateMap<BusinessOwner, BusinessDto>();
            CreateMap<BusinessOwner, BusinessOwnerDto>()

                              /*  .ForMember(
                                    dest => dest.FavoritesCount,
                                    opt => opt.MapFrom(src => src.Favorites.Count))
                                .ForMember(
                                    dest => dest.TagList,
                                    opt => opt.MapFrom(src => src.Tags.Select(s => s.TagId).ToList()))
                                .ForMember(
                                    dest => dest.Favorited,
                                    opt => opt.MapFrom((src, dest, destMember, context) => src.Favorites.Select(s => s.UserId).ToList()
                                              .Contains((Guid)context.Items["currentUserId"])))*/;

            CreateMap<Business, BusinessProfileDto>()
                .ForMember(
                    dest => dest.Services,
                    opt => opt.MapFrom(src => src.Services.Select(c=>c.ServiceName).ToList()))
                .ForMember(
                    dest => dest.Rate1,
                    opt => opt.MapFrom(src => src.Reviews.Where(r=>r.Rate==1).Count()))
                .ForMember(
                    dest => dest.Rate2,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 2).Count()))
                .ForMember(
                    dest => dest.Rate3,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 3).Count()))
                .ForMember(
                    dest => dest.Rate4,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 4).Count()))
                .ForMember(
                    dest => dest.Rate5,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 5).Count()))
                .ForMember(
                    dest => dest.AvgRate,
                    opt => opt.MapFrom(src => (int)Math.Round(src.Reviews.Select(a=> a.Rate).Count() == 0 ? 0 : src.Reviews.Select(a => a.Rate).ToList().Average())))
                .ForMember(
                    dest => dest.RateCount,
                    opt => opt.MapFrom(src => src.Reviews.Count()))
                 /* .ForMember(
                      dest => dest.Photos,
                      opt => opt.MapFrom(src => src.Photos.Select(c=>c.PhotoName).ToList()))*/
                 .ForMember(
                    dest => dest.Following,
                    opt => opt.MapFrom((src, dest, destMember, context) => src.Followers.Select(s => s.UserId).ToList()
                              .Contains((Guid)context.Items["currentUserId"])));


            CreateMap<Business, BusinessBusinessProfileDto>()
                .ForMember(
                    dest => dest.Services,
                    opt => opt.MapFrom(src => src.Services.Select(c => c.ServiceName).ToList()))
                .ForMember(
                    dest => dest.Rate1,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 1).Count()))
                .ForMember(
                    dest => dest.Rate2,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 2).Count()))
                .ForMember(
                    dest => dest.Rate3,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 3).Count()))
                .ForMember(
                    dest => dest.Rate4,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 4).Count()))
                .ForMember(
                    dest => dest.Rate5,
                    opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rate == 5).Count()))
                .ForMember(
                    dest => dest.AvgRate,
                    opt => opt.MapFrom(src => (int)Math.Round(src.Reviews.Select(a => a.Rate).Average())))
                .ForMember(
                    dest => dest.RateCount,
                    opt => opt.MapFrom(src => src.Reviews.Count()))
                 /* .ForMember(
                      dest => dest.Photos,
                      opt => opt.MapFrom(src => src.Photos.Select(c=>c.PhotoName).ToList()))*/
   ;

            CreateMap<Business, DashDto>()
                     .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.BusinessOwner.FirstName))
                   .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.BusinessOwner.LastName))
                  .ForMember(
                    dest => dest.Services,
                    opt => opt.MapFrom(src => src.Services.OrderByDescending(r => r.ServiceName).Select(c => c.ServiceName).ToList()))
                  .ForMember(
                    dest => dest.ServicesReviews,
                    opt => opt.MapFrom(src => src.Reviews.OrderByDescending(r=>r.Service).GroupBy(c=>c.Service).Select(c=>c.Count()).ToList()))
                  .ForMember(
                    dest => dest.ServicesPositive,
                    opt => opt.MapFrom(src => src.Reviews.OrderByDescending(r => r.Service).Where(c=>c.Sentement =="Positive").GroupBy(c => c.Service).Select(c => c.Count()).ToList()))
                  .ForMember(
                    dest => dest.ServicesNegative,
                    opt => opt.MapFrom(src => src.Reviews.OrderByDescending(r => r.Service).Where(c => c.Sentement == "Negative").GroupBy(c => c.Service).Select(c => c.Count()).ToList()))
                  .ForMember(
                    dest => dest.Rate1,
                    opt => opt.MapFrom(src => src.Reviews.Where(c=>c.Rate==1).Count()))
                  .ForMember(
                    dest => dest.Rate2,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Rate == 2).Count()))
                  .ForMember(
                    dest => dest.Rate3,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Rate == 3).Count()))
                  .ForMember(
                    dest => dest.Rate4,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Rate == 4).Count()))
                  .ForMember(
                    dest => dest.Rate5,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Rate == 5).Count()))
                  .ForMember(
                    dest => dest.Positive,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Sentement == "Positive").Count()))
                  .ForMember(
                    dest => dest.Negative,
                    opt => opt.MapFrom(src => src.Reviews.Where(c => c.Sentement == "Negative").Count()))
                ; //done

            CreateMap<BusinessForCreationDto, BusinessOwner>(); //done
            CreateMap<BusinessForUpdateDto, BusinessOwner>(); //done
            CreateMap<BusinessForUpdatePasswordDto, BusinessOwner>();
            CreateMap<BusinessLoginDto, BusinessOwner>(); //done
            CreateMap<BusinessProfileForUpdateDto, Business>(); //done
            CreateMap<BusinessProfileSetupDto, Business>(); //done
            CreateMap<OpenDay, OpenDayDto>()
                  .ForMember(
                    dest => dest.Day,
                    opt => opt.MapFrom(src => src.Day))
                  .ForMember(
                    dest => dest.Starttime,
                    opt => opt.MapFrom(src => src.StartTime))
                  .ForMember(
                    dest => dest.Endtime,
                    opt => opt.MapFrom(src => src.EndTime)); //done
            CreateMap<Photo, PhotoDto>()
              .ForMember(
                 dest => dest.PhotoName,
                 opt => opt.MapFrom(src => src.PhotoName));
        }
    }
}
