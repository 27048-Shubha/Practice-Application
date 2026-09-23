using Coffee_Machine_Application.Model;

namespace Coffee_Machine_Application.Repository
{
    public class OrderRepository
    {
        private List<Order> orders;
        internal OrderRepository()
        {
            this.orders = new();
        }

        public void Add(Order order)
        {
            this.orders.Add(order);
        }
    }
}
