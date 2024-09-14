using AutoMapper;
using SimpleCommerce.Infrastructure.Entities;
using SimpleCommerce.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using SimpleCommerce.Domain.Response;
using Microsoft.EntityFrameworkCore;

namespace SimpleCommerce.Services;

public interface ICartRepository
{
    CartResponse AddProductToCart(int userId, int productId);
    void DeleteCartByUserId(int userId);
    CartResponse DeleteProductFromCart(int userId, int productId);
    CartResponse GetCartByUserId(int userId);

}

public class CartRepository : ICartRepository
{
    private readonly IProductRepository _productRepository;
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public CartRepository(IProductRepository productRepository,
        DataContext dataContext,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public CartResponse AddProductToCart(int userId, int productId)
    {
        var productToAdd = _productRepository.GetProductById(productId);
        if (productToAdd == null)
        {
            throw new Exception($"Product with id: {productId} cannot be added");
        }

        var cart = GetOrCreateCart(userId);

        var productEntity = _dataContext
            .Products
            .FirstOrDefault(x => x.Id == productToAdd.Id);

        cart.Products.Add(productEntity);

        _dataContext.SaveChanges();

        return _mapper.Map<CartResponse>(cart);
    }


    public void DeleteCartByUserId(int userId)
    {
        var cart = _dataContext.Carts.FirstOrDefault(x => x.UserId == userId);
        if (cart == null)
        {
            throw new Exception($"Cart for UserId: {userId} does not exist");
        }

        _dataContext.Remove(cart);
        _dataContext.SaveChanges();
    }

    public CartResponse DeleteProductFromCart(int userId, int productId)
    {
        var productToRemove = _productRepository.GetProductById(productId);
        if (productToRemove == null)
        {
            throw new Exception($"Product with id: {productId} cannot be removed cause it doesnt exist");
        }

        var cart = GetOrCreateCart(userId);

        var productEntity = cart
            .Products
            .FirstOrDefault(x => x.Id == productToRemove.Id);

        if (productEntity == null) throw new Exception($"Product: {productToRemove.Id} {productToRemove.Name} does not exist in Cart: {cart.Name}");

        cart.Products.Remove(productEntity);

        _dataContext.SaveChanges();

        return _mapper.Map<CartResponse>(cart);
    }

    public CartResponse GetCartByUserId(int userId)
    {
        var cart = GetOrCreateCart(userId);
        return _mapper.Map<CartResponse>(cart);
    }

    private CartEntity GetOrCreateCart(int userId)
    {
        var cart = _dataContext.Carts.Where(x => x.UserId == userId)
            .Include(x => x.Products)
            .FirstOrDefault();

        if (cart != null) return cart;

        var newCart = new CartEntity()
        {
            UserId = userId,
            Name = "Cart_Of_User_" + userId,
        };

        _dataContext.Carts.Add(newCart);
        _dataContext.SaveChanges(true);

        return newCart;

    }
}

