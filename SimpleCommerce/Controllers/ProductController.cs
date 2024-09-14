using Microsoft.AspNetCore.Mvc;
using SimpleCommerce.Domain.Request;
using SimpleCommerce.Services;

namespace SimpleCommerce.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpPost]
    public IActionResult AddProduct(AddProductRequest request)
    {
        var response = _productRepository.AddProduct(request);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetProducts([FromQuery] int? count, [FromQuery] int? offset)
    {
        var response = _productRepository.GetProducts(count, offset);

        if (response.Any() == false)
        {
            return NotFound();
        }

        return Ok(response);
    }


    [HttpGet("{Id:int}")]
    public IActionResult GetProductById(int Id)
    {
        try
        {
            var response = _productRepository.GetProductById(Id);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("{Id:int}")]
    public IActionResult UpdateProductById(int Id, AddProductRequest request)
    {
        var response = _productRepository.UpdateProduct(Id, request);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }


    [HttpDelete("{Id:int}")]
    public IActionResult DeleteProducyById(int Id)
    {
        try
        {
            _productRepository.DeleteProduct(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}
