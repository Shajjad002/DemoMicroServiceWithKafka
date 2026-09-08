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
                    if (response.Topic == AddProductTopic)
                    {
                        var product = JsonSerializer.Deserialize<Product>(response.Message.Value);
                        Products.Add(product!);
                    }
                    else
                    {
                        var productId = int.Parse(response.Message.Value);
                        var product = Products.FirstOrDefault(p => p.Id == productId);
                        if (product != null)
                        {
                            Products.Remove(product);
                        }
                    }
                    ConstructProduct();



                }
            }
        }
        private void ConstructProduct()
        {
            Console.Clear();
            foreach (var item in Products)
            {
                Console.WriteLine($"ID:{item.Id}, Name: {item.Name}, Price: {item.Price}");
            }


        }

        public void AddOrder(Order order) => Orders.Add(order);


        public List<OrderSummary> GetOrderSummary()
        {
            var orderSummary = new List<OrderSummary>();
            foreach (var order in Orders)
            {
                var product = Products.FirstOrDefault(p => p.Id == order.ProductId);

                orderSummary.Add(new OrderSummary
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPrice = product.Price ?? 0,
                    OrderQuantity = order.Quantity,

                });
            }
            return orderSummary;
        }
        public List<Product> GetProducts() => Products;

        
    }

}
