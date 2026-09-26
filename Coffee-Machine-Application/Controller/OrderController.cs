namespace Coffee_Machine_Application.Controller
{
    using Coffee_Machine_Application.Enums;
    using Coffee_Machine_Application.Model;
    using Coffee_Machine_Application.Service;
    using Coffee_Machine_Application.View;

    public class OrderController
    {
        private readonly ConsoleView _console;
        private readonly OrderService _orderService;
        private readonly StockService _stockService;

        internal OrderController(ConsoleView console, OrderService orderService, StockService stockService)
        {
            this._console = console;
            this._orderService = orderService;
            this._stockService = stockService;
        }

        public void RunOrderMenu()
        {
            while (true)
            {
                try
                {
                    this.CheckStockQuantity();
                    OrderMenu choice = this._console.GetOrderChoice();
                    switch (choice)
                    {
                        case OrderMenu.PlaceOrder:
                            this.PlaceOrder();
                            break;

                        case OrderMenu.CheckStock:
                            this.CheckStockQuantity();
                            break;

                        case OrderMenu.Back:
                            return;
                    }
                }
                catch (Exception e)
                {
                    this._console.DisplayMessage(e.Message);
                }
            }
        }

        public async Task StartMachine(CoffeeMachine machine, CancellationToken cancellationToken)
        {
            machine.Status = MachineStatus.Available;
            await this._orderService.ProcessOrder(machine, cancellationToken);
        }

        public void PlaceOrder()
        {
            CoffeeType type = this._console.GetCoffeeChoice();
            QuantityRange quantity = this._console.GetOrderQuantity();

            OrderDTO order = new OrderDTO(Guid.NewGuid(), type, quantity, DateTime.Now, Guid.NewGuid());
            this._orderService.EnqueueOrder(order);
        }

        public void CheckStockQuantity()
        {
            IngredientQuantity currentStockQuantity = this._stockService.FetchCurrentQuantity();
            this._console.DisplayStock(currentStockQuantity);
        }

        public void RefillStock(IngredientType ingredient)
        {
            this._stockService.RefillStock(ingredient); // store refill 
        }
    }
}
