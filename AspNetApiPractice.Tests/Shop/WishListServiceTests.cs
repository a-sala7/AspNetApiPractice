using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Data.Repository.User;
using AspNetApiPractice.Data.UnitOfWork;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Shop;
using Moq;
using System.Linq.Expressions;

namespace AspNetApiPractice.Tests.Shop;

public class WishListServiceTests
{
    private IUnitOfWork _unitOfWork;
    private IRepository<WishlistsProducts> _wishlistsProductsRepository;
    private IRepository<Product> _productRepository;
    private IUserRepository _userRepository;
    private IWishListService _wishListService;
    private const string USER_ID = "3b56ea49-e878-48d1-add7-5fed0dee055c";
    private const string NOT_FOUND_USER_ID = "00000000-0000-0000-0000-000000000000";
    public WishListServiceTests()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        _unitOfWork = mockUnitOfWork.Object;
        mockUnitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
        var mockUserRepository = new Mock<IUserRepository>();
        _userRepository = mockUserRepository.Object;
        mockUserRepository
            .Setup(x => x.Exists(USER_ID))
            .ReturnsAsync(() =>
            {
                return true;
            });
        mockUserRepository
            .Setup(x => x.Exists(NOT_FOUND_USER_ID))
            .ReturnsAsync(() =>
            {
                return false;
            });
        var mockProductsRepository = new Mock<IRepository<Product>>();
        _productRepository = mockProductsRepository.Object;
        mockProductsRepository
            .Setup(x => x.GetById(It.IsAny<int>()))
            .ReturnsAsync(() =>
            {
                return new Product();
            });
        ReconstructWishListService();
    }

    private void ReconstructWishListService()
    {
        _wishListService = new WishListService(_wishlistsProductsRepository, _productRepository, _unitOfWork, _userRepository);
    }

    [Fact]
    public async Task AddProduct_ProductDoesntExist_ThrowsNotFoundException()
    {
        //Arrange
        var mockProductsRepository = new Mock<IRepository<Product>>();
        _productRepository = mockProductsRepository.Object;
        mockProductsRepository
            .Setup(x => x.GetById(123))
            .ReturnsAsync(() =>
            {
                return null;
            });
        ReconstructWishListService();

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _wishListService.AddProduct(USER_ID, 123);
        });
    }

    [Fact]
    public async Task AddProduct_DoesntAlreadyExistInWishList_AddsProduct()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(Enumerable.Empty<WishlistsProducts>);
        ReconstructWishListService();

        //Act & Assert
        await _wishListService.AddProduct(USER_ID, 2);
    }

    [Fact]
    public async Task AddProduct_AlreadyExistsInWishList_Throws()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(() =>
            {
                return new WishlistsProducts[]
                {
                    new() { UserId = USER_ID, ProductId = 1 }
                };
            });
        ReconstructWishListService();

        //Act && Assert
        await Assert.ThrowsAsync<AppException>(async () => {
            await _wishListService.AddProduct(USER_ID, 1);
        });
    }

    [Fact]
    public async Task RemoveProduct_DoesntExistInWishList_Throws()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(Enumerable.Empty<WishlistsProducts>);
        ReconstructWishListService();

        //Act & Assert
        await Assert.ThrowsAsync<AppException>(async () =>
        {
            await _wishListService.RemoveProduct(USER_ID, 2);
        });
    }

    [Fact]
    public async Task RemoveProduct_ExistsInWishList_RemovesProduct()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(() =>
            {
                return new WishlistsProducts[]
                {
                    new() { UserId = USER_ID, ProductId = 1 }
                };
            });
        ReconstructWishListService();

        //Act && Assert
        await _wishListService.RemoveProduct(USER_ID, 1);
    }

    [Fact]
    public async Task GetWishList_UserNotFound_ThrowsNotFoundException()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(Enumerable.Empty<WishlistsProducts>());
        ReconstructWishListService();

        //Act && Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _wishListService.GetWishList(NOT_FOUND_USER_ID);
        });
    }

    [Fact]
    public async Task AddProduct_UserNotFound_ThrowsNotFoundException()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(Enumerable.Empty<WishlistsProducts>);
        ReconstructWishListService();

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _wishListService.AddProduct(NOT_FOUND_USER_ID, 1);
        });
    }

    [Fact]
    public async Task RemoveProduct_UserNotFound_ThrowsNotFoundException()
    {
        //Arrange
        var mockWishlistsProductsRepository = new Mock<IRepository<WishlistsProducts>>();
        _wishlistsProductsRepository = mockWishlistsProductsRepository.Object;
        mockWishlistsProductsRepository
            .Setup(x => x.All(It.IsAny<Expression<Func<WishlistsProducts, bool>>>()))
            .ReturnsAsync(() =>
            {
                return new WishlistsProducts[]
                {
                    new() { UserId = USER_ID, ProductId = 1 }
                };
            });
        ReconstructWishListService();

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _wishListService.RemoveProduct(NOT_FOUND_USER_ID, 1);
        });
    }
}
