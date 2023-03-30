namespace SocialNetwork.Core.Interfaces;

public interface ICustomMapper
{
    public TDestination Map<TDestination>(object source) where TDestination : class;
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) where TDestination : class;
}
