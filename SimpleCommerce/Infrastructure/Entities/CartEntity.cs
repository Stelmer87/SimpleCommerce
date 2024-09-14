namespace SimpleCommerce.Infrastructure.Entities;

internal class CartEntity
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ProductEntity> Products { get; } = new();
}
