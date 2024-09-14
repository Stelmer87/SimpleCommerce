using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SimpleCommerce.Domain.Request;
using SimpleCommerce.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace SimpleCommerce.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private const string UserIdHeader = "x-user-id";

    public CartController(ICartRepository cartService)
    {
        _cartRepository = cartService;
    }


    [HttpPut("products")]
    public IActionResult AddProductToCart([FromHeader(Name = UserIdHeader)][Required] string userId, 
        ProductCartRequest product)
    {
        try
        {
            var cart = _cartRepository.AddProductToCart(int.Parse(userId), product.ProductId);

            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }


    [HttpDelete("products")]
    public IActionResult RemoveProductFromCart([FromHeader(Name = UserIdHeader)][Required] string userId,
        ProductCartRequest product)
    {
        try
        {
            
            var cart = _cartRepository.AddProductToCart(int.Parse(userId), product.ProductId);

            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpGet("")]
    public IActionResult GetCartByUserId([FromHeader(Name = UserIdHeader)][Required] string userId)
    {
        try
        {
           
            var cart = _cartRepository.GetCartByUserId(int.Parse(userId));
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("")]
    public IActionResult DeleteCartById([FromHeader(Name = UserIdHeader)][Required] string userId)
    {
        try
        {
           
            _cartRepository.DeleteCartByUserId(int.Parse(userId));

            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}
