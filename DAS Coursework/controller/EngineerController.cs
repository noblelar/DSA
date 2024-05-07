using System;
using System.Linq;
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
            else if (response == 1)
            {
                RemoveJourneyDelay();
            }
        }

        public static void AddJourneyDelay()
        {
            string? endingStation = null;
            string[] LineOptions = data.GetData.GetUniqueLines().Append("Back").ToArray();

            int start = MenuDisplay.GetMenu(LineOptions, new[] { "Add A Journey Delay", "Please select the line:" });

            if (start == LineOptions.Length-1)
            {
                GetEngineerMenu();
                return;
            }

            string[] StationOptions = MainController.graph.VerticesConnectedToLine(LineOptions[start]).Append("Cancel").ToArray();

            int end = MenuDisplay.GetMenu(StationOptions, new[] { "Add A Journey Delay", $"Please select your starting station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

            if (end == StationOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            Console.WriteLine("Do you want to apply the delay in Both Direction (True / False)");
            var newResponse = utils.Parser.AcceptBooleanInformation();

            if (newResponse)
            {
                string[] endingOptions = StationOptions.Where(s => !s.Contains(StationOptions[end])).ToArray();
                int endIdx = MenuDisplay.GetMenu(endingOptions, new[] { "Add A Journey Delay", $"Please select your ending station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

                if (end == StationOptions.Length - 1)
                {
                    GetEngineerMenu();
                    return;
                }
                endingStation = endingOptions[endIdx];
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"\n\nEnter the delay you want to apply travel time on {LineOptions[start]} starting from {StationOptions[end]} {(endingStation == null ? "(in minute)" : "")}");
            if(endingStation != null)
            {
                Console.Write($" AND {LineOptions[start]} from {endingStation}");
            }

            Console.WriteLine();
            Console.ResetColor();

            double delay = Parser.AcceptDoubleInformation();

            //apply delay to the edge with the starting point and line
            MainController.graph.AddDelayToEdge(StationOptions[end], LineOptions[start], delay);
            if(endingStation != null)
            {
                MainController.graph.AddDelayToEdge(endingStation, LineOptions[start], delay);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"\n\nYou have applied a delay of {delay}min to journey to {LineOptions[start]} starting from {StationOptions[end]}");
            if (endingStation != null)
            {
                Console.Write($" AND {LineOptions[start]} starting from {endingStation}");
            }
            Console.WriteLine();

            Console.ResetColor();
            Dijkstra.ShortestPath(MainController.graph, MainController.graph.FindVertexByName("WEMBLEY PARK"), MainController.graph.FindVertexByName("LONDON BRIDGE"));
            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }

        public static void RemoveJourneyDelay()
        {

            string[] LineOptions = data.GetData.GetUniqueLines().Append("Back").ToArray();
            int start = MenuDisplay.GetMenu(LineOptions, new[] { "Remove Journey Delay", "Please select the line:" });

            if (start == LineOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] StationOptions = MainController.graph.VerticesConnectedToLine(LineOptions[start]).Append("Cancel").ToArray();
            int end = MenuDisplay.GetMenu(StationOptions, new[] { "Add A Journey Delay", $"Please select your starting station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

            if (end == StationOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }            

            var edge = MainController.graph.GetEdgeWithStartVertexAndLine(StationOptions[end], LineOptions[start]);

            var delay = edge.delay;

            if(delay > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n\nThere is a delay of {delay}min on {LineOptions[start]} starting from {StationOptions[end]}");
                Console.WriteLine("Do you want to remove it (TRUE / FALSE)");
                Console.ResetColor();

                bool response = Parser.AcceptBooleanInformation();
                if (response)
                {
                    edge.RemoveDelay();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\nThere is no delay on {LineOptions[start]} starting from {StationOptions[end]}");
                Console.ResetColor();
            }

            Dijkstra.ShortestPath(MainController.graph, MainController.graph.FindVertexByName("WEMBLEY PARK"), MainController.graph.FindVertexByName("LONDON BRIDGE"));
            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }
    }
}

