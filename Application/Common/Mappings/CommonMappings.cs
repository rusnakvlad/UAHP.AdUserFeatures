using Application.Common.CommonDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;
public class CommonMappings : Profile
{
    public CommonMappings()
    {
        CreateMap<Ad, AdShortInfoDTO>();
    }
}
