using AutoMapper;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;

namespace BookTravel.APi.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<TravelerDto, ApplicationUser>();

            CreateMap<BookingDto, Booking>();

            CreateMap<Bus, BusDto>().ReverseMap();
            CreateMap< AppointmentDto, Appointment>()
                .ForMember(dst=>dst.Buses,opt=>opt.Ignore())
                .ReverseMap();

            CreateMap<TravelerViewDto, ApplicationUser>()
                .ReverseMap();

            CreateMap<ApplicationUser, AuthModelDto>();
                
        }
    }
}
