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
                    SourcingEndTime = await this.SourceIngredients(order.Type, order.Quantity),
                    ProcessingEndTime = await this.PrepareOrder(order.Type, order.Quantity),
                    DeliveredTime = this.GetDeliveryTime(),
                    VendingMachineId = order.VendingMachineId,
                };
            this._orderRepository.Add(orderDetails);
        }

        public async Task<DateTime> SourceIngredients(CoffeeType type, QuantityRange range)
        {
            int value = 0;
            orderStatus = OrderStatus.Sourcing;

            if (!this._stockService.IsAllIngredientsAvailable(type, range))
            {
                orderStatus = OrderStatus.WaitingForIngredients; //TODO:EVENTS
                value = this._stockService.RefillIngredients();
            }

            await Task.Delay(value);
            return DateTime.Now;
        }
        public async Task<DateTime> PrepareOrder(CoffeeType type, QuantityRange range)
        {
            orderStatus = OrderStatus.Preparing;
            int value = this._stockService.ConsumeIngredients(type, range);
            await Task.Delay(value);
            return DateTime.Now;
        }

        public DateTime GetDeliveryTime()
        {
            orderStatus = OrderStatus.Delivered;
            return DateTime.Now;
        }
    }
}
