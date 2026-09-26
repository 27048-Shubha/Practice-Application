using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
using Coffee_Machine_Application.Repository;

namespace Coffee_Machine_Application.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly StockService _stockService;

        private object queueLock = new();
        private Queue<Order> OrderQueue = new ();
        internal OrderService(OrderRepository orderRepository,  StockService stockService)
        {
            this._orderRepository = orderRepository;
            this._stockService = stockService;
        }

        public void EnqueueOrder(OrderDTO order)
        {
            lock (queueLock)
            {
                Order orderDetails =
                new()
                {
                    OrderId = order.OrderId,
                    Customer = SessionHandler.CurrentUser,
                    Status = OrderStatus.Received,
                    CoffeeType = order.Type,
                    Quantity = order.Quantity,
                    ReceivedTime = order.ReceivedTime,
                };
                OrderQueue.Enqueue(orderDetails);
            }
        }

        public async Task ProcessOrder(CoffeeMachine machine, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                
                Order? currentOrder = null;
                lock (queueLock)
                {
                    if (OrderQueue.Count != 0)
                    {
                        currentOrder = OrderQueue.Dequeue();
                    }
                }

                if (currentOrder != null)
                {
                    try
                    {
                        machine.CurrentOrderId = currentOrder.OrderId;
                        machine.Status = MachineStatus.NotAvailable;
                        currentOrder.VendingMachineId = machine.Id;
                        currentOrder.SourcingEndTime = await this.SourceIngredients(currentOrder, cancellationToken);
                        currentOrder.ProcessingEndTime = await this.PrepareOrder(currentOrder, cancellationToken);
                        currentOrder.DeliveredTime = this.GetDeliveryTime(currentOrder, machine);
                        await this._orderRepository.Add(currentOrder);
                    }
                    catch(OperationCanceledException)
                    {
                        currentOrder.Status = OrderStatus.Cancelled;
                        machine.Status = MachineStatus.NotAvailable;
                        await this._orderRepository.Add(currentOrder);
                    }
                }
            }
        }

        public async Task<DateTime> SourceIngredients(Order order, CancellationToken cancellationToken)
        {
            int value = 0;
            order.Status = OrderStatus.Sourcing;

            if (!this._stockService.IsAllIngredientsAvailable(order.CoffeeType, order.Quantity))
            {
                order.Status = OrderStatus.WaitingForIngredients;
                value = this._stockService.RefillIngredients();
            }

            await Task.Delay(value, cancellationToken);
            return DateTime.Now;
        }
        public async Task<DateTime> PrepareOrder(Order order, CancellationToken cancellationToken)
        {
            order.Status = OrderStatus.Preparing;
            int value = this._stockService.ConsumeIngredients(order.CoffeeType, order.Quantity);
            await Task.Delay(value, cancellationToken);
            return DateTime.Now;
        }

        public DateTime GetDeliveryTime(Order order, CoffeeMachine machine)
        {
            machine.Status = MachineStatus.NotAvailable;
            order.Status = OrderStatus.Delivered;
            return DateTime.Now;
        }
    }
}
