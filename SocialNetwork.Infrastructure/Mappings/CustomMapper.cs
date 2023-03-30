using AutoMapper;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Infrastructure.Mappings;

public class CustomMapper : ICustomMapper
{
    private readonly IMapper _mapper;

    public CustomMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) where TDestination : class
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TDestination : class
    {
        return _mapper.Map(source, destination);
    }
}
