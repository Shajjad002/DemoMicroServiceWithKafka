using Confluent.Kafka;
using Shared;
using System.Text.Json;

namespace PruductAPI.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly List<Product> Products = new();

        public ProductService(IProducer<Null, string> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task AddProduct(Product product)
        {
            Products.Add(product);

            try
            {
                var result = await _producer.ProduceAsync(
                    "add-product-topic",
                    new Message<Null, string> { Value = JsonSerializer.Serialize(product) });

                if (result?.Status != PersistenceStatus.Persisted)
                {
                    Products.Remove(product);
                }
            }
            catch
            {
                // remove optimistic add on failure
                Products.Remove(product);
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
                var result = await _producer.ProduceAsync("delete-product-topic", new Message<Null, string> { Value = id.ToString() });
            }
        }
    }
}
