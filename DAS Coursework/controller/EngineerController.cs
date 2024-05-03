using System;
using DAS_Coursework.utils;

namespace DAS_Coursework.controller
{
    public static class EngineerController
    {
        public static void GetEngineerMenu()
        {
            string[] EngineerOptions = new[]{
                    "Add journey time delays on track sections.",
                    "Remove journey time delay on track section",
                    "Close track sections",
                    "Open track sections",
                    "list of closed-track sections",
                    "list of delayed journey track sections, with normal time and delayed time",
                    "Go Back"
            };

            int response = utils.MenuDisplay.GetMenu(EngineerOptions, new[] { "This is the engineer menu", "What action do you want to perform:" });

            if (response == 6)
            {
                MainController.GetMainMain();
            }else if(response == 0)
            {
                AddJourneyDelay();
            }
        }

        public static void AddJourneyDelay()
        {
            //TODO: fetch these stations from the models
            string[] LineOptions = new[]{
                    "Wembley Station",
                    "Baker Street",
                    "Go Back"
            };

            int start = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", "Please select your start station:" });

            if (start == 2)
            {
                GetEngineerMenu();
            }

            int end = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", $"Please select your ending station: \n\nStart destination: {LineOptions[start]}" });

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\nEnter the delay you want to add distance from {LineOptions[start]} to {LineOptions[end]} (in minute)");
            Console.ResetColor();

            int delay = Parser.AcceptIntegerInformation();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\nYou have applied a delay of {delay}min to journey from {LineOptions[start]} to {LineOptions[end]}");
            Console.ResetColor();


            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }

        public static void RemoveJourneyDelay()
        {
            //TODO: fetch these stations from the models
            string[] LineOptions = new[]{
                    "Wembley Station",
                    "Baker Street",
                    "Go Back"
            };


            int start = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", "Please select your start station:" });

            if(start == 2)
            {
                GetEngineerMenu();
            }

            int end = MenuDisplay.GetMenu(LineOptions, new[] { "Find A Route", $"Please select your ending station: \n\nStart destination: {LineOptions[start]}" });

            //TODO: fetch the journey section

            var delay = 0;

            if(delay > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n\nThere is a delay of {delay}min between {LineOptions[start]} and {LineOptions[end]}");
                Console.WriteLine("Do you want to remove it (TRUE / FALSE)");
                Console.ResetColor();

                bool response = Parser.AcceptBooleanInformation();
                if (response)
                {
                    //TODO: pass station as argument
                    RemoveDelay();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\nThere is no delay between {LineOptions[start]} and {LineOptions[end]}");
                Console.ResetColor();
            }


            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }

        private static void RemoveDelay()
        {
            //TODO: station will be passed as a arg

            // TODO: set station delay to 0
        }
    }
}

