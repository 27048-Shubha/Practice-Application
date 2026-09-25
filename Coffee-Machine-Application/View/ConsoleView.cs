using Coffee_Machine_Application.Enums;
using Coffee_Machine_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.View
{
    public class ConsoleView
    {
        private object _consoleLock = new ();
        private static string MajorLineBreaker = "====================================================";
        private static string MinorLineBreaker = "====================================================";

        private void WriteToConsole(string message)
        {
            lock (_consoleLock)
            {
                Console.WriteLine($"{message}");
            }
        }
        private string ReadFromConsole()
        {
            lock (_consoleLock)
            {
                return Console.ReadLine();
            }
        }

        public MainMenuChoice GetMainMenuChoice()
        {
            while (true)
            {
                this.WriteToConsole(MajorLineBreaker);
                this.WriteToConsole("Welcome to the Coffee Shop");
                this.WriteToConsole("[1] Register");
                this.WriteToConsole("[2] Login");
                this.WriteToConsole("[3] Quit");
                this.WriteToConsole(MajorLineBreaker);

                this.WriteToConsole("\nEnter your choice: ");
                string userInput = this.ReadFromConsole();

                if (Enum.TryParse<MainMenuChoice>(userInput, out MainMenuChoice choice) &&
                    Enum.IsDefined(typeof(MainMenuChoice), choice))
                {
                    return choice;
                }
                this.WriteToConsole("Enter valid inputs only");
            }
        }

        public OrderMenu GetOrderChoice()
        {
            while (true)
            {
                this.WriteToConsole(MajorLineBreaker);
                this.WriteToConsole("Welcome to the Coffee Shop");
                this.WriteToConsole("[1] PlaceOrder");
                this.WriteToConsole("[2] CheckStock");
                this.WriteToConsole("[3] Back");
                this.WriteToConsole(MajorLineBreaker);

                this.WriteToConsole("\nEnter your choice: ");
                string userInput = this.ReadFromConsole();

                if (Enum.TryParse<OrderMenu>(userInput, out OrderMenu choice) &&
                    Enum.IsDefined(typeof(OrderMenu), choice))
                {
                    return choice;
                }

                this.WriteToConsole("Enter valid inputs only");
            }
        }

        public CoffeeType GetCoffeeChoice()
        {
            while (true)
            {
                this.WriteToConsole(MajorLineBreaker);
                this.WriteToConsole("[1] Americano");
                this.WriteToConsole("[2] Cappucino"); 
                this.WriteToConsole("[3] Espresso");
                this.WriteToConsole("[4] Latte");
                this.WriteToConsole(MajorLineBreaker);

                this.WriteToConsole("\nEnter your choice: ");
                string userInput = this.ReadFromConsole();

                if (Enum.TryParse<CoffeeType>(userInput, out CoffeeType choice) &&
                    Enum.IsDefined(typeof(CoffeeType), choice))
                {
                    return choice;
                }

                this.WriteToConsole("Enter valid inputs only");
            }
        }

        public QuantityRange GetOrderQuantity()
        {
            while (true)
            {
                this.WriteToConsole(MajorLineBreaker);
                this.WriteToConsole("[1] Small");
                this.WriteToConsole("[2] Medium");
                this.WriteToConsole("[3] Large");
                this.WriteToConsole(MajorLineBreaker);

                this.WriteToConsole("\nEnter your choice: ");
                string userInput = this.ReadFromConsole();

                if (Enum.TryParse<QuantityRange>(userInput, out QuantityRange choice) &&
                    Enum.IsDefined(typeof(QuantityRange), choice))
                {
                    return choice;
                }

                this.WriteToConsole("Enter valid inputs only");
            }
        }

        public string GetUserName()
        {
            this.WriteToConsole("Enter user name:");
            string input = this.ReadFromConsole();
            return input;
        }

        public void DisplayStock(IngredientQuantity currentStockQuantity)
        {
            this.WriteToConsole(MinorLineBreaker);

            this.WriteToConsole($"Coffee Bean: {currentStockQuantity.CoffeeBean}");
            this.WriteToConsole($"Milk: {currentStockQuantity.Milk}");
            this.WriteToConsole($"Water: {currentStockQuantity.Water}");
            this.WriteToConsole($"Sugar: {currentStockQuantity.Sugar}");

            this.WriteToConsole(MinorLineBreaker);
        }

        public void DisplayMessage(string message)
        {
            this.WriteToConsole(message);
        }
         
        public void DisplayRegistrationStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                this.WriteToConsole("Registered successfully!");
            }
            else
            {
                this.WriteToConsole("User already exists! Kindly Login to continue");
            }
        }

        public void DisplayLoginStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                this.WriteToConsole("Login successful!");
            }
            else
            {
                this.WriteToConsole("Failed to login. Try again.");
            }
        }
        public void DisplayOrderStatus(string message)
        {
            this.WriteToConsole(message);
            Thread.Sleep(1000);
        }
        public void DisplayExitMessage()
        {
            this.WriteToConsole(MajorLineBreaker);
            this.WriteToConsole("Thank you for visiting!");
            this.WriteToConsole(MajorLineBreaker);
        }
    }
}
