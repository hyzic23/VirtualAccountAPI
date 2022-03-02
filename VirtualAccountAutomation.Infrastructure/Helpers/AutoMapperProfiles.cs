using AutoMapper;
using VirtualAccountAutomation.Infrastructure.Dtos;
using VirtualAccountAutomation.Infrastructure.Models;

namespace VirtualAccountAutomation.Infrastructure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<VirtualAccountRequest, VirtualAccountRequestDto>().ReverseMap();
        }
    }
}