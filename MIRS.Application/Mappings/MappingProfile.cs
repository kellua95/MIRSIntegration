using AutoMapper;
using MIRS.Application.DTOs;
using MIRS.Domain.Models;

namespace MIRS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Issue, IssueDto>();
        CreateMap<CreateIssueDto, Issue>();
    }
}