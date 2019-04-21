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
        private static Player Player;
        private static Dummy Dummy;
        private static Fight Fight;

        static void Main(string[] args)
        {
            var menuChoice = MainMenu();

            switch (menuChoice.Value)
            {
                case 1:
                    NameChoice();
                    CharacsChoice();
                    StartFight();
                    break;
                case 2:
                    Environment.Exit(0);
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
            stringBuilder.AppendLine($"{nameof(entity.MaxHealth)} : {entity.MaxHealth}");
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

        static void NameChoice()
        {
            Console.WriteLine("Choose your name :");
            var name = string.Empty;
            while(!Entity.NameIsValid(name))
            {
                name = Console.ReadLine();
            }
            Player = new Player(name, 100);
        }

        static void CharacsChoice()
        {
            Console.WriteLine("Choose your hero characteristics :");

            var availablePoints = 30;

            Console.WriteLine($"Available points : {availablePoints}");

            while (availablePoints > 0)
            {
                // Max health
                Console.WriteLine(EntityCharacs(Player));
                Console.WriteLine("Max health (+2 by point) ?");
                var input = int.Parse(Console.ReadLine());
                availablePoints -= input;
                Player.AddMaxHealth(input * 2);
                Console.WriteLine($"Available points : {availablePoints}");


                // Strength
                Console.WriteLine(EntityCharacs(Player));
                Console.WriteLine($"{nameof(Player.Strength)} ?");
                input = int.Parse(Console.ReadLine());
                availablePoints -= input;
                Player.Strength += input;
                Console.WriteLine($"Available points : {availablePoints}");


                // Intelligence
                Console.WriteLine(EntityCharacs(Player));
                Console.WriteLine($"{nameof(Player.Intelligence)} ?");
                input = int.Parse(Console.ReadLine());
                availablePoints -= input;
                Player.Intelligence += input;
                Console.WriteLine($"Available points : {availablePoints}");


                // Dexterity
                Console.WriteLine(EntityCharacs(Player));
                Console.WriteLine($"{nameof(Player.Dexterity)} ?");
                input = int.Parse(Console.ReadLine());
                availablePoints -= input;
                Player.Dexterity += input;
                Console.WriteLine($"Available points : {availablePoints}");


                // Endurance
                Console.WriteLine($"{nameof(Player.Endurance)} : {availablePoints}");
                Player.Endurance += availablePoints;
                availablePoints -= availablePoints;
            }
        }

        static void StartFight()
        {
            Player.AddMultiplier(new Multiplier(EffectType.Blunt, 2));
            Dummy = new Dummy("Dummy", 100, 0);
            var sword = new Weapon("Wood sword", 8, 11, EffectType.Blunt);
            Player.Equip(sword);

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
