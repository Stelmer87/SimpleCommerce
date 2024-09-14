using AutoMapper;
using SimpleCommerce.Domain.Request;
using SimpleCommerce.Domain.Response;
using SimpleCommerce.Infrastructure;
using SimpleCommerce.Infrastructure.Entities;

namespace SimpleCommerce.Services;

public interface IProductRepository
{
    ProductResponse AddProduct(AddProductRequest request);
    void DeleteProduct(int Id);
    ProductResponse GetProductById(int Id);
    List<ProductResponse> GetProducts(int? count = null, int? offset = null);
    ProductResponse UpdateProduct(int Id, AddProductRequest request);

}

public class ProductRepository : IProductRepository
{

    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public ProductRepository(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _dataContext = context;
    }

    public ProductResponse AddProduct(AddProductRequest request)
    {

        var entity = _mapper.Map<ProductEntity>(request);
        _dataContext.Add(entity);
        _dataContext.SaveChanges();
        return _mapper.Map<ProductResponse>(entity);
    }

    public void DeleteProduct(int Id)
    {
        var product = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        if (product is null)
        {
            throw new Exception("Object not found");
        }

        _dataContext.Products.Remove(product);
        _dataContext.SaveChanges();
    }

    public ProductResponse GetProductById(int Id)
    {
        var product = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        if (product is null)
        {
            throw new Exception("Object not found");
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public List<ProductResponse> GetProducts(int? count = null, int? offset = null)
    {

        var products = _dataContext.Products.ToList();
        if (count != null && offset != null)
        {
            if (products.Count > 0)
            {
                products = products
                    .Skip((int)offset)
                    .Take((int)count)
                    .ToList();
            }
        }

        return _mapper.Map<List<ProductResponse>>(products);
    }

    public ProductResponse UpdateProduct(int Id, AddProductRequest request)
    {
        var entity = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        entity.Price = request.Price;
        entity.Name = request.Name;

        _dataContext.Update(entity);
        _dataContext.SaveChanges();

        return _mapper.Map<ProductResponse>(entity);
    }
}
