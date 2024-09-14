namespace SimpleCommerce.Infrastructure.Entities;

internal class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public List<CartEntity> Carts { get; } = new();
}
