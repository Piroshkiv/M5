using Basket.Host.Models.Dtos;
using Basket.Host.Models.Response;
using Basket.Host.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace Basket.Host.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisCacheConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RedisConfig _config;

        public CacheService(
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisCacheConnectionService,
            IOptions<RedisConfig> config,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _redisCacheConnectionService = redisCacheConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;
        }



        public Task<BasketProductDto?> AddAsync(string key, BasketProductDto value)
        => AddInternalAsync(key, value);

        public async Task<bool> RemoveProductAsync(string key, int value)
            => await RemoveProductInternalAsync(key, value);
        public async Task<BasketProductDto?> IncrementProductAsync(string key, int value)
            => await IncrementProductInternalAsync(key, value);
        public async Task<BasketProductDto?> DecrementProductAsync(string key, int value)
            => await DecrementProductInternalAsync(key, value);
        public async Task<BasketProductDto?> GetProductByIdAsync(string key, int id)
            => await GetProductByIdInternalAsync(key, id);

        public async Task<BasketDto> GetAsync(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(key);

            if (!serialized.HasValue)
            {
                return null!;
            }

            var deserialized = _jsonSerializer.Deserialize<BasketDto>(serialized.ToString());

            return deserialized;
        }

        public async Task<bool> ClearAsync(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);
            var expiry = _config.CacheTimeout;

            return await redis.KeyDeleteAsync(cacheKey);
        }

        private string GetItemCacheKey(string userId) => $"{userId}";
        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();

        private async Task<BasketProductDto?> AddInternalAsync(string key, BasketProductDto value,
            IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            _logger.LogInformation(serialized.ToString());

            var deserialized = serialized.HasValue ?
                _jsonSerializer.Deserialize<BasketDto>(serialized.ToString())
                : new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };

            if (deserialized.Products.Any(p => p.Product!.Equals(value.Product)))
                return null;

            var products = deserialized.Products.ToList();
            products.Add(value);
            deserialized.Products = products;
            deserialized.Size++;

           serialized = _jsonSerializer.Serialize(deserialized);

            await redis.StringSetAsync(cacheKey, serialized, expiry);

            return new BasketProductDto { Product = products.Last().Product, Quantity = products.Last().Quantity };
        }

        private async Task<bool> RemoveProductInternalAsync(string key, int value,
    IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            _logger.LogInformation(serialized.ToString());

            var deserialized = serialized.HasValue ?
                _jsonSerializer.Deserialize<BasketDto>(serialized.ToString())
                : new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };

            if (!deserialized.Products.Any(p => p.Product!.Equals(value)))
                return false;

            var products = deserialized.Products.ToList();
            products.Remove(products.Single(p => p.Product!.Equals(value)));
            deserialized.Products = products;
            deserialized.Size--;

            serialized = _jsonSerializer.Serialize(deserialized);

            await redis.StringSetAsync(cacheKey, serialized, expiry);

            return true;
        }

        private async Task<BasketProductDto?> IncrementProductInternalAsync(string key, int value,
    IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            _logger.LogInformation(serialized.ToString());

            var deserialized = serialized.HasValue ?
                _jsonSerializer.Deserialize<BasketDto>(serialized.ToString())
                : new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };

            if (!deserialized.Products.Any(p => p.Product!.Equals(value)))
                return null;

            var product = deserialized.Products.Single(p => p.Product!.Equals(value));

            product.Quantity++;

            serialized = _jsonSerializer.Serialize(deserialized);

            await redis.StringSetAsync(cacheKey, serialized, expiry);

            return product;
        }

        private async Task<BasketProductDto?> DecrementProductInternalAsync(string key, int value,
IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            _logger.LogInformation(serialized.ToString());

            var deserialized = serialized.HasValue ?
                _jsonSerializer.Deserialize<BasketDto>(serialized.ToString())
                : new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };

            if (!deserialized.Products.Any(p => p.Product!.Equals(value)))
                return null;

            var product = deserialized.Products.Single(p => p.Product!.Equals(value));

            product.Quantity--;

            serialized = _jsonSerializer.Serialize(deserialized);

            await redis.StringSetAsync(cacheKey, serialized, expiry);

            return product;
        }

        private async Task<BasketProductDto?> GetProductByIdInternalAsync(string key, int id,
IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            _logger.LogInformation(serialized.ToString());

            var deserialized = serialized.HasValue ?
                _jsonSerializer.Deserialize<BasketDto>(serialized.ToString())
                : new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };

            if (!deserialized.Products.Any(p => p.Product!.Equals(id)))
                return null;

            return deserialized.Products.FirstOrDefault(p => p.Product.Equals(id));
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

    }
}