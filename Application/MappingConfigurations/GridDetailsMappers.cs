using AutoMapper;
using Domain.Settings;

namespace Application.MappingConfigurations
{
    public class GridDetailsMappers : Profile
    {
        public GridDetailsMappers()
        {
            CreateMap<GridDetails, GridParam>()
                .ForMember(destination => destination.DisplayLength,
                    opts => opts.MapFrom(source => source.length))
                .ForMember(destination => destination.DisplayStart,
                    opts => opts.MapFrom(source => source.start))
                .ForMember(destination => destination.SortDir,
                    opts => opts.MapFrom(source => source.order[0].dir))
                .ForMember(destination => destination.SortCol,
                    opts => opts.MapFrom(source => source.order[0].column))
                .ForMember(destination => destination.Search,
                    opts => opts.MapFrom(source => source.search.value));
        }
    }
}
