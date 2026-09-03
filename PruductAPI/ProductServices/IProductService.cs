using Shared;

namespace PruductAPI.ProductServices
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(int id);
    }
}
