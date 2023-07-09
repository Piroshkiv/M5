using System.Collections.Generic;
using Basket.Host.Models.Dtos;
using Basket.Host.Models.Request;
using Basket.Host.Models.Response;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using Moq;

namespace Basket.UnitTests.Services
{
    public class BasketServiceTest
    {
        private readonly IBasketService _basketService;
        private readonly Mock<ICacheService> _cacheService;

        public BasketServiceTest()
        {
            _cacheService = new Mock<ICacheService>();
            _basketService = new BasketService(_cacheService.Object);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            var testKey = "1";
            var testProductDto = new BasketProductDto() { Product = 1 };
            var testRequest = new AddProductRequest() { Id = 1 };
            var testResponse = new AddProductResponse() { Product = new BasketProductDto { Product = 1, Quantity = 1 } };

            _cacheService.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<BasketProductDto>())).ReturnsAsync((Func<BasketProductDto>)null!);

            // act
            var result = await _basketService.AddAsync(testKey, testRequest);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testKey = "1";
            var testProductDto = new BasketProductDto() { Product = 1, Quantity = 1 };
            var testRequest = new AddProductRequest() { Id = 1 };
            var testResponse = new AddProductResponse() { Product = new BasketProductDto { Product = 1, Quantity = 1 } };

            _cacheService.Setup(s => s.AddAsync(It.IsAny<string>(), It.IsAny<BasketProductDto>())).ReturnsAsync(testProductDto);

            // act
            var result = await _basketService.AddAsync(testKey, testRequest);

            // assert
            result!.Product!.Product.Should().Be(testResponse.Product.Product);
            result!.Product!.Quantity.Should().Be(testResponse.Product.Quantity);
        }

        public async Task GetAsync_Failed()
        {
            // arrange
            var testKey = "1";
            _cacheService.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync((Func<BasketDto>)null!);

            // act
            var result = await _basketService.GetAsync(testKey);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            // arrange
            var testKey = "1";
            var testBasketDto = new BasketDto() { Products = new List<BasketProductDto>(), Size = 0 };
            var testResponse = new ProductsResponse() { Products = new List<BasketProductDto>(), Size = 0 };

            _cacheService.Setup(s => s.GetAsync(It.IsAny<string>())).ReturnsAsync(testBasketDto);

            // act
            var result = await _basketService.GetAsync(testKey);

            // assert
            result.Should().NotBeNull();
        }
    }
}
