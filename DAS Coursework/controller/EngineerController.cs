using System;
using System.IO;
using System.Linq;
using DAS_Coursework.models;
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
            else if(response == 2) {
                CloseTrack();
            }else if(response == 3)
            {
                OpenTrack();
            }else if(response == 4)
            {
                ListClosedTracks();
            }else if (response == 5)
            {
                ListDelays();
            }
        }

        public static void AddJourneyDelay()
        {
            string endingStation;
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

            string[] endingOptions = StationOptions.Where(s => !s.Contains(StationOptions[end])).ToArray();
            int endIdx = MenuDisplay.GetMenu(endingOptions, new[] { "Add A Journey Delay", $"Please select your ending station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

            if (end == endingOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }
            endingStation = endingOptions[endIdx];

            string[] directionOption = MainController.graph.FindLineDirections(LineOptions[start]).Append("BOTH").Append("Cancel").ToArray();
            int direction = MenuDisplay.GetMenu(directionOption, new[] { "Add A Journey Delay", $"Please select the direction of the track: \n\nSelected Line: ({LineOptions[end]})" });

            if (direction == directionOption.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\nEnter the delay you want to apply travel time on {LineOptions[start]} starting from {StationOptions[end]} AND from {endingStation} (in minute)");
            Console.ResetColor();

            double delay = Parser.AcceptDoubleInformation();

            //apply delay to the edge with the starting point and line
            var outcome = MainController.graph.AddDelayToEdge(StationOptions[end], LineOptions[start], delay, directionOption[direction], endingStation);

            if (outcome)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\nYou have applied a delay of {delay}min to journey to {LineOptions[start]} starting from {StationOptions[end]} AND  {endingStation} IN {directionOption[direction]} Direction");
                Console.ResetColor();
            }

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
            int end = MenuDisplay.GetMenu(StationOptions, new[] { "Remove Journey Delay", $"Please select your starting station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

            if (end == StationOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] endingOptions = StationOptions.Where(s => !s.Contains(StationOptions[end])).Append("Cancel").ToArray();
            int endIdx = MenuDisplay.GetMenu(endingOptions, new[] { "Remove Journey Delay", $"Please select your ending station of the journey delay: \n\nSelected Line: ({LineOptions[start]})" });

            if (end == endingOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }


            string[] directionOption = MainController.graph.FindLineDirections(LineOptions[start]).Append("BOTH").Append("Cancel").ToArray();
            int direction = MenuDisplay.GetMenu(directionOption, new[] { "Add A Journey Delay", $"Please select the direction of the track: \n\nSelected Line: ({LineOptions[end]})" });

            if (direction == directionOption.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            double? delayTime = null;
            models.Delay? delay1 = null;
            models.Delay? delay2 = null;

            if (directionOption[direction] == "BOTH")
            {
                 delay1 = MainController.graph.FindDelay(StationOptions[end], endingOptions[endIdx], LineOptions[start], directionOption[0]);
                 delay2 = MainController.graph.FindDelay(endingOptions[endIdx],  StationOptions[end], LineOptions[start], directionOption[1]);

                if (delay1 != null && delay2 != null)
                {
                    delayTime = delay1.Time;
                }
            }
            else
            {

                 delay1 = MainController.graph.FindDelay(StationOptions[end], endingOptions[endIdx],LineOptions[start], directionOption[direction]);

                if (delay1 != null)
                {
                    delayTime = delay1.Time;
                }

            }


            if(delayTime != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n\nThere is a delay of {delayTime}min on {LineOptions[start]} starting from {StationOptions[end]} and {endingOptions[endIdx]} in {directionOption[direction]} direction");
                Console.WriteLine("Do you want to remove it (TRUE / FALSE)");
                Console.ResetColor();

                bool response = Parser.AcceptBooleanInformation();
                if (response)
                {

                    if (directionOption[direction] == "Both")
                    {

                        Edge[] starts;
                        Edge[] ends;

                        starts = MainController.graph.FindPathBetweenStations(
                            StationOptions[end], endingOptions[endIdx],
                            LineOptions[start], directionOption[0], false);

                        if (starts.Length == 0)
                        {
                            starts = MainController.graph.FindPathBetweenStations(
                                endingOptions[endIdx], StationOptions[end],
                                LineOptions[start], directionOption[0], false);
                        }

                        ends = MainController.graph.FindPathBetweenStations(
                            StationOptions[end], endingOptions[endIdx],
                            LineOptions[start], directionOption[1], false);

                        if (ends.Length == 0)
                        {
                            ends = MainController.graph.FindPathBetweenStations(endingOptions[endIdx], StationOptions[end], LineOptions[start], directionOption[1], false);
                        }

                        MainController.graph.RemoveDelay(delay1);
                        MainController.graph.RemoveDelay(delay2);

                        starts[0].RemoveDelay();
                        ends[0].RemoveDelay();
                    }
                    else
                    {
                        var path = MainController.graph.FindPathBetweenStations(StationOptions[end], endingOptions[endIdx], LineOptions[start], directionOption[direction], false);
                        MainController.graph.RemoveDelay(delay1);
                        path[0].RemoveDelay();
                    }

                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\nThere is no delay on {LineOptions[start]} starting from {StationOptions[end]}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }

        public static void CloseTrack()
        {
            string[] LineOptions = data.GetData.GetUniqueLines().Append("Back").ToArray();

            int lineIdx = MenuDisplay.GetMenu(LineOptions, new[] { "Close Track", "Please select the line:" });

            if (lineIdx == LineOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] StationOptions = MainController.graph.VerticesConnectedToLine(LineOptions[lineIdx]).Append("Cancel").ToArray();

            int startIdx = MenuDisplay.GetMenu(StationOptions, new[] { "Close Track", $"Please select your starting station: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (startIdx == StationOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] endingOptions = StationOptions.Where(s => !s.Contains(StationOptions[startIdx])).ToArray();
            int endIdx = MenuDisplay.GetMenu(endingOptions, new[] { "Close Track", $"Please select your ending station: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (endIdx == endingOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }


            string[] directionOption = MainController.graph.FindLineDirections(LineOptions[lineIdx]).Append("BOTH").Append("Cancel").ToArray();
            int direction = MenuDisplay.GetMenu(directionOption, new[] { "Close Track", $"Please select the direction of the track: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (direction == directionOption.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            if(direction == 2)
            {
                 models.Edge[] start;
                models.Edge[] end;

                 start = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[0], false);

                if(start.Length == 0)
                {
                    start = MainController.graph.FindPathBetweenStations(endingOptions[endIdx], StationOptions[startIdx], LineOptions[lineIdx], directionOption[0], false);
                }

                end = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[1], false);

                if(end.Length == 0)
                {
                    end = MainController.graph.FindPathBetweenStations(endingOptions[endIdx], StationOptions[startIdx], LineOptions[lineIdx], directionOption[1], false);
                }

                if(end.Length !=0 && start.Length != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You will be closing the track from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]} direction");
                    Console.Write("1.");
                    for (int i = 0; i < start.Length; i++)
                    {
                        Console.Write(start[i].fromVerticex.Name + $" {(i == start.Length - 1 ? $" -> {start[i].toVerticex.Name}" : " -> ")} ");
                    }
                    Console.WriteLine();

                    Console.Write("2.");
                    for (int i = 0; i < end.Length; i++)
                    {
                        Console.Write(end[i].fromVerticex.Name + $" {(i == end.Length - 1 ? $" -> {end[i].toVerticex.Name}" : " -> ")} ");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Are you sure you want to proceed (TRUE / FALSE)");
                    Console.ResetColor();
                    var newResponse = utils.Parser.AcceptBooleanInformation();

                    if (!newResponse)
                    {
                        GetEngineerMenu();
                        return;
                    }
                    else
                    {
                        MainController.graph.CloseEdges(start);
                        MainController.graph.CloseEdges(end);
                        Console.WriteLine($"\n You have successfully closed the path from {StationOptions[startIdx]} to to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no path from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in the {directionOption[direction]} direction");
                }
            }
            else
            {
                var path = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[direction], false);

                if(path.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You will be closing the track from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    for (int i = 0; i < path.Length; i++)
                    {
                        Console.Write(path[i].fromVerticex.Name + $" {(i == path.Length-1 ?$" -> {path[i].toVerticex.Name}":" -> ")} ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Are you sure you want to proceed (TRUE / FALSE)");
                    Console.ResetColor();
                    var newResponse = utils.Parser.AcceptBooleanInformation();

                    if (!newResponse)
                    {
                        GetEngineerMenu();
                        return;
                    }
                    else
                    {
                        MainController.graph.CloseEdges(path);
                        Console.WriteLine($"\n You have successfully closed the path from {StationOptions[startIdx]} to to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    }

                }
                else
                {
                    Console.WriteLine($"There is no path from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in the {directionOption[direction]} direction");
                }

            }


            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }

        }

        public static void OpenTrack()
        {
            string[] LineOptions = data.GetData.GetUniqueLines().Append("Back").ToArray();

            int lineIdx = MenuDisplay.GetMenu(LineOptions, new[] { "Open Track", "Please select the line:" });

            if (lineIdx == LineOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] StationOptions = MainController.graph.VerticesConnectedToLine(LineOptions[lineIdx]).Append("Cancel").ToArray();

            int startIdx = MenuDisplay.GetMenu(StationOptions, new[] { "Open Track", $"Please select your starting station: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (startIdx == StationOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            string[] endingOptions = StationOptions.Where(s => !s.Contains(StationOptions[startIdx])).ToArray();
            int endIdx = MenuDisplay.GetMenu(endingOptions, new[] { "Open Track", $"Please select your ending station: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (endIdx == endingOptions.Length - 1)
            {
                GetEngineerMenu();
                return;
            }


            string[] directionOption = MainController.graph.FindLineDirections(LineOptions[lineIdx]).Append("BOTH").Append("Cancel").ToArray();
            int direction = MenuDisplay.GetMenu(directionOption, new[] { "Open Track", $"Please select the direction of the track: \n\nSelected Line: ({LineOptions[lineIdx]})" });

            if (direction == directionOption.Length - 1)
            {
                GetEngineerMenu();
                return;
            }

            if (direction == 2)
            {
                models.Edge[] start;
                models.Edge[] end;

                start = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[0], true);

                if (start.Length == 0)
                {
                    start = MainController.graph.FindPathBetweenStations(endingOptions[endIdx], StationOptions[startIdx], LineOptions[lineIdx], directionOption[0], true);
                }

                end = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[1], true);

                if (end.Length == 0)
                {
                    end = MainController.graph.FindPathBetweenStations(endingOptions[endIdx], StationOptions[startIdx], LineOptions[lineIdx], directionOption[1], true);
                }

                if (end.Length != 0 && start.Length != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You will be opening the track from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]} direction");
                    Console.Write("1.");
                    for (int i = 0; i < start.Length; i++)
                    {
                        Console.Write(start[i].fromVerticex.Name + $" {(i == start.Length - 1 ? $" -> {start[i].toVerticex.Name}" : " -> ")} ");
                    }
                    Console.WriteLine();

                    Console.Write("2.");
                    for (int i = 0; i < end.Length; i++)
                    {
                        Console.Write(end[i].fromVerticex.Name + $" {(i == end.Length - 1 ? $" -> {end[i].toVerticex.Name}" : " -> ")} ");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Are you sure you want to proceed (TRUE / FALSE)");
                    Console.ResetColor();
                    var newResponse = utils.Parser.AcceptBooleanInformation();

                    if (!newResponse)
                    {
                        GetEngineerMenu();
                        return;
                    }
                    else
                    {
                        MainController.graph.OpenEdges(start);
                        MainController.graph.OpenEdges(end);
                        Console.WriteLine($"\n You have successfully openned the path from {StationOptions[startIdx]} to to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no path from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in the {directionOption[direction]} direction");
                }
            }
            else
            {
                var path = MainController.graph.FindPathBetweenStations(StationOptions[startIdx], endingOptions[endIdx], LineOptions[lineIdx], directionOption[direction], true);

                if (path.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You will be opening the track from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    for (int i = 0; i < path.Length; i++)
                    {
                        Console.Write(path[i].fromVerticex.Name + $" {(i == path.Length - 1 ? $" -> {path[i].toVerticex.Name}" : " -> ")} ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Are you sure you want to proceed (TRUE / FALSE)");
                    Console.ResetColor();
                    var newResponse = utils.Parser.AcceptBooleanInformation();

                    if (!newResponse)
                    {
                        GetEngineerMenu();
                        return;
                    }
                    else
                    {
                        MainController.graph.OpenEdges(path);
                        Console.WriteLine($"\n You have successfully opened the path from {StationOptions[startIdx]} to to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in {directionOption[direction]}");
                    }

                }
                else
                {
                    Console.WriteLine($"There is no path from {StationOptions[startIdx]} to {endingOptions[endIdx]} on the {LineOptions[lineIdx]} in the {directionOption[direction]} direction");
                }

            }


            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }

        }

        public static void ListClosedTracks()
        {
            string[] LineOptions = data.GetData.GetUniqueLines().Append("Back").ToArray();
            var count = 1;

            Console.WriteLine("\nBelow are the list of closed tracks:");

            foreach(var line in LineOptions)
            {
                string[] directions = MainController.graph.FindLineDirections(line);
                foreach(var d in directions)
                {
                    var closed = MainController.graph.FindTracks(line, d, true);
                    foreach(var p in closed)
                    {
                        Console.WriteLine($"{count}. {line} ({d}): {p.fromVerticex.Name} -> {p.toVerticex.Name}");
                        count++;
                    }
                }
            }

            if(count == 0)
            {
                Console.WriteLine("There are no closed tracks");
            }

            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }

        }

        public static void ListDelays()
        {
            Console.WriteLine($"\nThese are the list of delays");
            foreach(var delay in MainController.graph.delays)
            {
                delay.Display();
            }

            if (MainController.graph.delays.Length == 0)
            {
                Console.WriteLine("There are no delay");
            }

            Console.WriteLine("\nPress enter to go back");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Enter)
            {
                GetEngineerMenu();
            }
        }
    }
}

