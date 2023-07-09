namespace Infrastructure.Services.Interfaces;
using StackExchange.Redis;

public interface IRedisCacheConnectionService
{
    public IConnectionMultiplexer Connection { get; }
}