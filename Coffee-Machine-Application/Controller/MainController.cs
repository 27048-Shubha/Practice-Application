using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Service;
using Coffee_Machine_Application.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Controller
{
    public class MainController
    {
        private readonly ConsoleView _console;
        private readonly OrderController _orderController;
        private readonly AuthService _authService;
        internal MainController(ConsoleView console, OrderController orderController, AuthService authService)
        {
            this._console = console;
            this._orderController = orderController;
            this._authService = authService;
        }

        public async Task Run()
        {
            this.InitializeStock();

            while (true)
            {
                try
                {
                    MainMenuChoice choice = this._console.GetMainMenuChoice();
                    switch (choice)
                    {
                        case MainMenuChoice.Register:
                            this.Register();
                            break;

                        case MainMenuChoice.Login:
                            await this.Login();
                            break;

                        case MainMenuChoice.Exit:
                            this._console.DisplayExitMessage();
                            return;
                    }
                }
                catch (Exception e)
                {
                    this._console.DisplayMessage(e.Message);
                }
            }
        }

        public void InitializeStock()
        {
            this._orderController.RefillStock(IngredientType.CoffeeBean);
            this._orderController.RefillStock(IngredientType.Water);
            this._orderController.RefillStock(IngredientType.Milk);
            this._orderController.RefillStock(IngredientType.Sugar);
        }

        public void Register()
        {
            string userName = this._console.GetUserName();
            if (this._authService.IsUserExists(userName))
            {
                this._console.DisplayRegistrationStatus(false);
                return;
            }

            this._console.DisplayRegistrationStatus(true);
        }

        public async Task Login()
        {
            string userName = this._console.GetUserName();

            if (!this._authService.Login(userName))
            {
                this._console.DisplayLoginStatus(false);
                return;
            }

            this._console.DisplayLoginStatus(true);
            await this._orderController.RunOrderMenu();
        }
    }
}
