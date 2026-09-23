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
                    SourcingEndTime = await this.SourceIngredients(order.Type),
                    ProcessingEndTime = await this.PrepareOrder(order.Type),
                    DeliveredTime = this.GetDeliveryTime(),
                    VendingMachineId = order.VendingMachineId,
                };
            this._orderRepository.Add(orderDetails);
        }

        public async Task<DateTime> SourceIngredients(CoffeeType type)
        {
            int value = 0;
            if (!this._stockService.IsAllIngredientsAvailable(type))
            {
                orderStatus = OrderStatus.WaitingForIngredients; //TODO:EVENTS
                value = this._stockService.RefillIngredients();
            }

            await Task.Delay(value);
            return DateTime.Now;
        }
        public async Task<DateTime> PrepareOrder(CoffeeType type)
        {
            int value = this._stockService.ConsumeIngredients(type);
            await Task.Delay(value);
            return DateTime.Now;
        }

        public DateTime GetDeliveryTime()
        {
            return DateTime.Now;
        }
    }
}
