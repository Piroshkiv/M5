using System.Linq;
using System.Threading;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    private readonly CatalogItemDto _testItemDto = new CatalogItemDto()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrand = new CatalogBrandDto() { Id = 1 },
        CatalogType = new CatalogTypeDto() { Id = 1 },
        PictureUrl = "1.png"
    };

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _catalogTypeRepository.Object, _catalogBrandRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedDataSuccess = new PaginatedData<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(pagingPaginatedDataSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
             It.Is<int>(i => i == testPageIndex),
             It.Is<int>(i => i == testPageSize),
             It.IsAny<int?>(),
             It.IsAny<int?>())).Returns((Func<PaginatedDataResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        int testIndex = 1;

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.IsAny<int>())).ReturnsAsync(_testItem);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
         It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByIdAsync(testIndex);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        int testIndex = 100;

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.IsAny<int>())).Returns((Func<CatalogBrand>)null!);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetByIdAsync(testIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByBrandAsync_Success()
    {
        string? testBrand = ".Net";

        var testItemsList = new List<CatalogItem>();
        testItemsList.Add(_testItem);

        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.IsAny<string>())).ReturnsAsync(testItemsList);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
         It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByBrandAsync(testBrand);

        // assert
        Assert.True(result.Count() > 0);
    }

    [Fact]
    public async Task GetByBrandAsync_Failed()
    {
        string? testBrand = string.Empty;

        var testEmptyItemsList = new List<CatalogItem>();

        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.IsAny<string>())).ReturnsAsync(testEmptyItemsList);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
         It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByBrandAsync(testBrand);

        // assert
        Assert.True(result.Count() == 0);
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        string? testType = "Mug";

        var testItemsList = new List<CatalogItem>();
        testItemsList.Add(_testItem);

        _catalogItemRepository.Setup(s => s.GetByTypeAsync(
            It.IsAny<string>())).ReturnsAsync(testItemsList);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
         It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByTypeAsync(testType);

        // assert
        Assert.True(result.Count() > 0);
    }

    [Fact]
    public async Task GetByTypeAsync_Failed()
    {
        string? testType = string.Empty;

        var testEmptyItemsList = new List<CatalogItem>();

        _catalogItemRepository.Setup(s => s.GetByTypeAsync(
            It.IsAny<string>())).ReturnsAsync(testEmptyItemsList);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
         It.Is<CatalogItem>(i => i.Equals(_testItem)))).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByTypeAsync(testType);

        // assert
        Assert.True(result.Count() == 0);
    }
}