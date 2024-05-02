using System;

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

			int response = utils.MenuDisplay.GetMenu(UserOptions, new[]{"This is the customer menu", "What action do you want to perform:"});

            if (response == 2)
            {
                MainController.GetMainMain();
            }else if(response == 1)
            {
                GetDisplayInformationMenu();
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

            int response = utils.MenuDisplay.GetMenu(LineOptions, new[] { "Display information about a station", "Which line does the station belong to" });

            if (response == 2)
            {
                GetUserMenu();
            }
            else
            {

            }
        }


        public static void DisplayStation()
        {

        }



    }
}

