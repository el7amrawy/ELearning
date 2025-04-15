using AutoMapper;
using ELearning.Areas.Dashboard.ViewModels;
using ELearning.Core.Models;

namespace ELearning.Areas.Dashboard.Helpers
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            /* Dashboard Area */
            CreateMap<AppUser, AdminProfile_ViewModel>();
            CreateMap<AdminProfile_ViewModel, AppUser>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Category_ViewModel, Category>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<EditCategory_ViewModel, Category>().ReverseMap();
        }
    }
}