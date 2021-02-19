using AutoMapper;
using VideoMonitoring.Domain.DTOs.Servers;
using VideoMonitoring.Domain.Entities;

namespace VideoMonitoring.Infra.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<Server, ServerDTO>().ReverseMap();
        }
    }
}
