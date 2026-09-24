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
        private static string MajorLineBreaker = "====================================================";
        private static string MinorLineBreaker = "====================================================";

        public MainMenuChoice GetMainMenuChoice()
        {
            while (true)
            {
                Console.WriteLine(MajorLineBreaker);
                Console.WriteLine("Welcome to the Coffee Shop");
                Console.WriteLine("[1] Register");
                Console.WriteLine("[2] Login");
                Console.WriteLine("[3] Quit");
                Console.WriteLine(MajorLineBreaker);

                Console.WriteLine("\nEnter your choice: ");
                string userInput = Console.ReadLine();

                if (Enum.TryParse<MainMenuChoice>(userInput, out MainMenuChoice choice) &&
                    Enum.IsDefined(typeof(MainMenuChoice), choice))
                {
                    return choice;
                }
                Console.WriteLine("Enter valid inputs only");
            }
        }

        public OrderMenu GetOrderChoice()
        {
            while (true)
            {
                Console.WriteLine(MajorLineBreaker);
                Console.WriteLine("Welcome to the Coffee Shop");
                Console.WriteLine("[1] PlaceOrder");
                Console.WriteLine("[2] CheckStock");
                Console.WriteLine("[3] Back");
                Console.WriteLine(MajorLineBreaker);

                Console.WriteLine("\nEnter your choice: ");
                string userInput = Console.ReadLine();

                if (Enum.TryParse<OrderMenu>(userInput, out OrderMenu choice) &&
                    Enum.IsDefined(typeof(OrderMenu), choice))
                {
                    return choice;
                }

                Console.WriteLine("Enter valid inputs only");
            }
        }

        public CoffeeType GetCoffeeChoice()
        {
            while (true)
            {
                Console.WriteLine(MajorLineBreaker);
                Console.WriteLine("[1] Americano");
                Console.WriteLine("[2] Cappucino"); 
                Console.WriteLine("[3] Espresso");
                Console.WriteLine("[4] Latte");
                Console.WriteLine(MajorLineBreaker);

                Console.WriteLine("\nEnter your choice: ");
                string userInput = Console.ReadLine();

                if (Enum.TryParse<CoffeeType>(userInput, out CoffeeType choice) &&
                    Enum.IsDefined(typeof(CoffeeType), choice))
                {
                    return choice;
                }

                Console.WriteLine("Enter valid inputs only");
            }
        }

        public QuantityRange GetOrderQuantity()
        {
            while (true)
            {
                Console.WriteLine(MajorLineBreaker);
                Console.WriteLine("[1] Small");
                Console.WriteLine("[2] Medium");
                Console.WriteLine("[3] Large");
                Console.WriteLine(MajorLineBreaker);

                Console.WriteLine("\nEnter your choice: ");
                string userInput = Console.ReadLine();

                if (Enum.TryParse<QuantityRange>(userInput, out QuantityRange choice) &&
                    Enum.IsDefined(typeof(QuantityRange), choice))
                {
                    return choice;
                }

                Console.WriteLine("Enter valid inputs only");
            }
        }

        public string GetUserName()
        {
            Console.WriteLine("Enter user name:");
            string input = Console.ReadLine();
            return input;
        }

        public void DisplayStock(IngredientQuantity currentStockQuantity)
        {
            Console.WriteLine(MinorLineBreaker);

            Console.WriteLine($"Coffee Bean: {currentStockQuantity.CoffeeBean}");
            Console.WriteLine($"Milk: {currentStockQuantity.Milk}");
            Console.WriteLine($"Water: {currentStockQuantity.Water}");
            Console.WriteLine($"Sugar: {currentStockQuantity.Sugar}");

            Console.WriteLine(MinorLineBreaker);
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
         
        public void DisplayRegistrationStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                Console.WriteLine("Registered successfully!");
            }
            else
            {
                Console.WriteLine("User already exists! Kindly Login to continue");
            }
        }

        public void DisplayLoginStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Failed to login. Try again.");
            }
        }
        public void DisplayOrderStatus(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(1000);
        }
        public void DisplayExitMessage()
        {
            Console.WriteLine(MajorLineBreaker);
            Console.WriteLine("Thank you for visiting!");
            Console.WriteLine(MajorLineBreaker);
        }
    }
}
