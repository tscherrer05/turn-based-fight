using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Controller;
using Game.Controller.Request;
using Game.Model;
using Game.Utils;

namespace Game.Main
{
    class Program
    {
        private const string Name = "Autoplayer";
        private static Player Player;
        private static Dummy Dummy;
        private static Fight Fight;

        static void Main(string[] args)
        {
            var menuChoice = MainMenu();

            switch(menuChoice.Value)
            {
                case 1:
                    Intro();
                    break;
                case 2:
                    StartFight();
                    break;
                default:
                    Console.WriteLine("Game over.");
                    break;
            }

        }


        static Choice MainMenu()
        {
            var ctrl = new MainMenuController();
            var mainMenuModel = ctrl.MainMenu();
            
            var userInput = new UserInput(mainMenuModel.Choices);
            var userChoice = userInput.ReadChoice();

            return userChoice;
        }

        static void Intro()
        {
            Console.Write("Your name : ");
            var name = string.Empty;
            name = Console.ReadLine();
            // Validate the input and display an error message if invalid
            while (Entity.NameIsValid(name) == false)
            {
                Console.WriteLine("Only letters are allowed.");
                Console.WriteLine("Your name : ");
                name = Console.ReadLine();
            }

            Player = new Player(name, 100, 1, 1, 1, 10);
            Player.AddMultiplier(new Multiplier(EffectType.Sharp, 100));
            Dummy = new Dummy("Soldier", 100, 10);
            var sword = new Weapon("Sword", 8, 11, EffectType.Sharp);
            Dummy.Equip(sword);
            var exca = new Weapon("Excalibur", 20, 25, EffectType.Fire);
            Player.Equip(exca);
        }

        static string EntityStatus(Entity entity)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{entity.Name} health : {entity.Health}/{entity.MaxHealth}");
            stringBuilder.AppendLine("------------------------------------");
            return stringBuilder.ToString();
        }

        static string EntityCharacs(Entity entity)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{entity.Name}");
            stringBuilder.AppendLine($"{nameof(entity.Intelligence)} : {entity.Intelligence}");
            stringBuilder.AppendLine($"{nameof(entity.Endurance)} : {entity.Endurance}");
            stringBuilder.AppendLine($"{nameof(entity.Strength)} : {entity.Strength}");
            stringBuilder.AppendLine($"{nameof(entity.Dexterity)} : {entity.Dexterity}");
            return stringBuilder.ToString();
        }

        static Choice ReadPlayerChoice()
        {            
            var choices = new List<Choice>();
            var key = 1;
            foreach(var actionable in Player.Offensives)
            {
                choices.Add(new Choice(key, actionable.Name));
                key++;
            }
            var userInput = new UserInput(choices);
            return userInput.ReadChoice();
        }

        static void StartFight()
        {
            Player = new Player(Name, 100, 1, 1, 1, 10);
            Player.AddMultiplier(new Multiplier(EffectType.Sharp, 100));
            Dummy = new Dummy("Soldat", 100, 10);
            var sword = new Weapon("Epée", 8, 11, EffectType.Sharp);
            Dummy.Equip(sword);
            var exca = new Weapon("Excalibur", 20, 25, EffectType.Fire);
            Player.Equip(exca);

            Fight = new Fight(new List<Entity> {Player}, new List<Entity> {Dummy});

            foreach (var entity in Fight.PlayerTeam)
                Console.Write(EntityCharacs(entity));
            foreach (var entity in Fight.OpponentTeam)
                Console.Write(EntityCharacs(entity));

            while (!Fight.IsOver)
            {
                foreach (var entity in Fight.PlayerTeam)
                    Console.Write(EntityStatus(entity));
                foreach(var entity in Fight.OpponentTeam)
                    Console.Write(EntityStatus(entity));
                var playerChoice = ReadPlayerChoice();
                var turn = Fight.NextTurn(
                    new OffensiveAction(Player, Fight.OpponentTeam.ToList(), Player.Offensives[playerChoice.Value - 1])
                );
                while(!Fight.CurrentTurnIsOver())
                {
                    var action = turn.PerformNextAction();
                    Console.WriteLine($"{action.Origin} deals {action.Damages} to {string.Join(", ", action.Targets)}");
                }
            }

            Console.WriteLine($"Winners : {string.Join(", ", Fight.WinningTeam())}");
        }
    }
}
