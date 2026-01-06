using AutoMapper;
using MIRS.Application.DTOs;
using MIRS.Web.ViewModels;

namespace MIRS.Web.Mappings;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<LoginViewModel, LoginDto>();
        CreateMap<RegisterViewModel, RegisterDto>();
    }
}