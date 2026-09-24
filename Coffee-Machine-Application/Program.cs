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

        public static void PrintStatus(OrderStatus status)
        {
            ConsoleView console = new ();
            switch (status)
            {
                case OrderStatus.Failed:
                    console.DisplayOrderStatus("[Order status] Order failed!");
                    break;
                case OrderStatus.WaitingForIngredients:
                    console.DisplayOrderStatus("[Order status] Waiting for ingredients...");
                    break;
                case OrderStatus.WaitingForVendingMachine:
                    console.DisplayOrderStatus("[Order status] Waiting for vending machine...");
                    break;
                case OrderStatus.Sourcing:
                    console.DisplayOrderStatus("[Order status] Sourcing ingredients...");
                    break;
                case OrderStatus.Preparing:
                    console.DisplayOrderStatus("[Order status] Preparing order...");
                    break;
                case OrderStatus.Delivered:
                    console.DisplayOrderStatus("[Order status] Order delivered!");
                    break;
                case OrderStatus.Cancelled:
                    console.DisplayOrderStatus("[Order status] Order cancelled!");
                    break;

                default:
                    console.DisplayOrderStatus("[Order status] Order received!");
                    break;
                    
            }
        }
    }
}
