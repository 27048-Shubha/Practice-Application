using Coffee_Machine_Application.Model;
using Coffee_Machine_Application.Utilities;

namespace Coffee_Machine_Application.Repository
{
    public class OrderRepository
    {
        private readonly string filePath = "orders.json";
        private List<Order> orders;
        internal OrderRepository()
        {
            this.orders = new();
        }

        public async Task Add(Order order)
        {
            this.orders.Add(order);
            await AsyncJsonFileHandler<Order>.WriteData(filePath, orders);
        }
    }
}
