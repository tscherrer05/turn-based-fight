using System.Collections.Generic;
using Game.Controller.Request;
using Game.Utils;

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
                    new Choice(1, "Histoire"),
                    new Choice(2, "Combat aléatoire")
                }
            };

            return mainMenuModel;
        }
    }
}