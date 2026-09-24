namespace Coffee_Machine_Application
{
    using Coffee_Machine_Application.Controller;
    using Coffee_Machine_Application.Model;
    using Coffee_Machine_Application.Repository;
    using Coffee_Machine_Application.Service;
    using Coffee_Machine_Application.View;

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            ConsoleView console = new ();
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

        public static void PrintStatus()
        {

        }
    }
}
