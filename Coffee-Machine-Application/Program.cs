namespace Coffee_Machine_Application
{
    using Coffee_Machine_Application.Controller;
    using Coffee_Machine_Application.Enums;
    using Coffee_Machine_Application.Model;
    using Coffee_Machine_Application.Repository;
    using Coffee_Machine_Application.Service;
    using Coffee_Machine_Application.View;

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            ConsoleView console = new();

            StockRepository stockRepository = new ();
            OrderRepository orderRepository = new();
            UserRepository userRepository = new ();

            AuthService authService = new (userRepository); 
            StockService stockService = new (stockRepository);
            OrderService orderService = new (orderRepository, stockService);

            OrderController orderController = new(console, orderService, stockService);

            MainController mainController = new MainController(console, orderController, authService);

            await mainController.Run();
        }

        public static void PrintMachineStatus(CoffeeMachine machine, Guid orderId)
        {
            ConsoleView console = new();
            switch (machine.Status)
            {
                case MachineStatus.NotAvailable:
                    console.DisplayOrderStatus($"[Machine {machine.Id}] Processing Order:{orderId}");
                    break;

                default:
                    console.DisplayOrderStatus($"[Machine {machine.Id}] Waiting for orders");
                    break;
            }
        }

        public static void PrintOrderStatus(Order order)
        {
            ConsoleView console = new ();
            switch (order.Status)
            {
                case OrderStatus.Failed:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Order failed!");
                    break;
                case OrderStatus.WaitingForIngredients:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Waiting for ingredients...");
                    break;
                case OrderStatus.WaitingForVendingMachine:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Waiting for vending machine...");
                    break;
                case OrderStatus.Sourcing:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Sourcing ingredients...");
                    break;
                case OrderStatus.Preparing:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Preparing order...");
                    break;
                case OrderStatus.Delivered:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Order delivered!");
                    break;
                case OrderStatus.Cancelled:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Order cancelled!");
                    break;

                default:
                    console.DisplayOrderStatus($"[Order {order.OrderId}] Order received!");
                    break;
                    
            }
        }
    }
}
