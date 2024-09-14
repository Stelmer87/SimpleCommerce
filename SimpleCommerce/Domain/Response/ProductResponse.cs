using SimpleCommerce.Domain.Request;

namespace SimpleCommerce.Domain.Response;

public class ProductResponse : AddProductRequest
{
    public int Id { get; set; }
}