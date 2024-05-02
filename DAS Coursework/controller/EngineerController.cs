using System;
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
            }
        }

        public static void AddJourneyDelay()
        {

        }
    }
}

