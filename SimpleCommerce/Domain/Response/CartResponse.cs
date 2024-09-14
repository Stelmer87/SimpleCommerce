namespace SimpleCommerce.Domain.Response;

public class CartResponse
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public List<ProductResponse> Products { get; set; }
}
