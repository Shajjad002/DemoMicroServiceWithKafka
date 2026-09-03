using Confluent.Kafka;
using Shared;
using System.Text.Json;

namespace OrderAPI.OrderServices
{
    public interface IOrderServices
    {
        Task StartConsumingService();
        void AddOrder(Order order);

        List<Product> GetProducts();
        List<OrderSummary> GetOrderSummary();
    }
    public class OrderServices(IConsumer<Null, string> consumer) : IOrderServices
    {
        private const string AddProductTopic = "add-product-topic";
        private const string DeleteProductTopic = "delete-product-topic";
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public async Task StartConsumingService()
        {
            await Task.Delay(10);
            consumer.Subscribe([AddProductTopic, DeleteProductTopic]);
            while (true)
            {
                var response = consumer.Consume();
                if (!string.IsNullOrEmpty(response.Message.Value))
                {
                    //check if topic == add product topic
                    if(response.Topic == AddProductTopic)
                    {
                        var product = JsonSerializer.Deserialize<Product>(response.Message.Value);
                        Products.Add(product!);
                    }

                }
            }
        }
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public List<OrderSummary> GetOrderSummary()
        {
            throw new NotImplementedException();
        }
        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }


    }

}
