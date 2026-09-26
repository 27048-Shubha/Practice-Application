using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
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
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly MachineService _machineService;

        internal MainController(ConsoleView console, OrderController orderController, AuthService authService, CancellationTokenSource cancellationTokenSource, MachineService machineService)
        {
            this._console = console;
            this._orderController = orderController;
            this._authService = authService;
            this._cancellationTokenSource = cancellationTokenSource;
            this._machineService = machineService;

        }

        public async Task Run()
        {
            List<CoffeeMachine> machineList = new ();
            machineList.Add(new CoffeeMachine(1));
            machineList.Add(new CoffeeMachine(2));
            machineList.Add(new CoffeeMachine(3));

            this.InitializeStock();

            _ = Task.Run(() => this._orderController.StartMachine(machineList[0], _cancellationTokenSource.Token));
            _ = Task.Run(() => this._orderController.StartMachine(machineList[1], _cancellationTokenSource.Token));
            _ = Task.Run(() => this._orderController.StartMachine(machineList[2], _cancellationTokenSource.Token));

            while (true)
            {
                try
                {
                    MainMenuChoice choice = this._console.GetMainMenuChoice();
                    switch (choice)
                    {
                        case MainMenuChoice.Register:
                            await this.Register();
                            break;

                        case MainMenuChoice.Login:
                            this.Login();
                            break;

                        case MainMenuChoice.PowerOffMachine1:
                            this._console.DisplayMessage("Powering off machine 1...");
                            this._machineService.PowerOff(machineList[0]);
                            break;

                        case MainMenuChoice.PowerOnMachine1:
                            this._console.DisplayMessage("Powering on machine 1...");
                            this._machineService.PowerOn(machineList[0]);
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

        public async Task Register()
        {
            string userName = this._console.GetUserName();
            if (this._authService.IsUserExists(userName))
            {
                this._console.DisplayRegistrationStatus(false);
                return;
            }

            await this._authService.Register(userName);
            this._console.DisplayRegistrationStatus(true);
        }

        public void Login()
        {
            string userName = this._console.GetUserName();

            if (!this._authService.Login(userName))
            {
                this._console.DisplayLoginStatus(false);
                return;
            }

            this._console.DisplayLoginStatus(true);
            this._orderController.RunOrderMenu();
        }
    }
}
