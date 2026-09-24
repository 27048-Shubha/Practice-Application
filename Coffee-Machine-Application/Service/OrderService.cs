using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
using Coffee_Machine_Application.Repository;

namespace Coffee_Machine_Application.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly StockService _stockService;
        private OrderStatus orderStatus;

        internal OrderService(OrderRepository orderRepository, StockService stockService)
        {
            this._orderRepository = orderRepository;
            this._stockService = stockService;
            this.orderStatus = OrderStatus.Received;
        }

        public async Task AddOrder(OrderDTO order)
        {
            Order orderDetails =
                new()
                {
                    OrderId = order.OrderId,
                    Customer = SessionHandler.CurrentUser,
                    CoffeeType = order.Type,
                    Quantity = order.Quantity,
                    Status = this.orderStatus,
                    ReceivedTime = order.ReceivedTime,
                    VendingMachineId = order.VendingMachineId,
                };
            orderDetails.SourcingEndTime = await this.SourceIngredients(orderDetails);
            orderDetails.ProcessingEndTime = await this.PrepareOrder(orderDetails);
            orderDetails.DeliveredTime = this.GetDeliveryTime(orderDetails);
            this._orderRepository.Add(orderDetails);
        }

        public async Task<DateTime> SourceIngredients(Order order)
        {
            int value = 0;
            order.Status = OrderStatus.Sourcing;

            if (!this._stockService.IsAllIngredientsAvailable(order.CoffeeType, order.Quantity))
            {
                order.Status = OrderStatus.WaitingForIngredients; //TODO:EVENTS
                value = this._stockService.RefillIngredients();
            }

            await Task.Delay(value);
            return DateTime.Now;
        }
        public async Task<DateTime> PrepareOrder(Order order)
        {
            order.Status = OrderStatus.Preparing;
            int value = this._stockService.ConsumeIngredients(order.CoffeeType, order.Quantity);
            await Task.Delay(value);
            return DateTime.Now;
        }

        public DateTime GetDeliveryTime(Order order)
        {
            order.Status = OrderStatus.Delivered;
            return DateTime.Now;
        }
    }
}
