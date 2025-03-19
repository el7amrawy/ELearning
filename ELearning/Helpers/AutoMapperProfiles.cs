using AutoMapper;
using ELearning.Core.Models;
using ELearning.ViewModels;
using ELearning.Areas.Dashboard.ViewModels;

namespace ELearning.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserProfile_ViewModel>();
            CreateMap<EditUserProfile_ViewModel, AppUser>();
            CreateMap<EditUserProfile_ViewModel, UserProfile_ViewModel>();

            /* Dashboard Area */
            CreateMap<AppUser, AdminProfile_ViewModel>();
            CreateMap<AdminProfile_ViewModel, AppUser>().ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}