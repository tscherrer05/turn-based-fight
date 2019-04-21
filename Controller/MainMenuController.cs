using System.Collections.Generic;
using Game.Controller.Request;

namespace Game.Controller
{
    public class MainMenuController
    {
        public MainMenuModel MainMenu()
        {
            var mainMenuModel = new MainMenuModel()
            {
                 Choices = new List<Choice>
                {
                    new Choice(1, "Training fight"),
                    new Choice(2, "Exit")
                }
            };

            return mainMenuModel;
        }
    }
}