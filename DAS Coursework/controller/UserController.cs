using System;
using DAS_Coursework.utils;

namespace DAS_Coursework.controller
{
    public static class UserController
    {
        public static void GetUserMenu()
        {
            string[] UserOptions = new[]{
                    "Find A Route",
                    "Display information about a station",
                    "Go Back"
            };

            int response = MenuDisplay.GetMenu(UserOptions, new[] { "This is the customer menu", "What action do you want to perform:" });

            if (response == 2)
            {
                MainController.GetMainMain();
            }
            else if (response == 1)
            {
                GetDisplayInformationMenu();
            }
            else
            {
                GetDisplayRouteMenu();
            }
        }


        public static void GetDisplayInformationMenu()
        {
            //TODO: fetch these stations from the models
            string[] LineOptions = new[]{
                    "Wembley Station",
                    "Baker Street",
                    "Go Back"
            };

            int response = MenuDisplay.GetMenu(LineOptions, new[] { "Display information about a station", "Please select the station you want to view:" });

            if (response == 2)
            {
                GetUserMenu();
            }
            else
            {
                DisplayStation(LineOptions[response]);
            }
        }


        public static void DisplayStation(string station)
        {
            Console.Clear();
            TitleCreator.GetTitle();

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(station);
            Console.ResetColor();
            Console.WriteLine("");

            Console.WriteLine("Lines available are:");

            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetUserMenu();
            }

        }

        public static void GetDisplayRouteMenu()
        {
            //TODO: fetch these stations from the models
            string[] LineOptions = new[]{
                    "Wembley Station",
                    "Baker Street",
                    "Go Back"
            };

            int start = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", "Please select your start station:" });

            int end = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", $"Please select your ending station: \n\nStart destination: {LineOptions[start]}" });

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\nThe fastest route from {LineOptions[start]} to {LineOptions[end]}");
            Console.ResetColor();

            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetUserMenu();
            }
        }

    }
}

