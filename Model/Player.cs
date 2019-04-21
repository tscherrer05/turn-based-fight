using System;

namespace Game.Model
{
    public class Player : Entity
    {
        public Player(string name, int maxHealth) : base(name, maxHealth)
        {

        }

        public Player(string name, int health, int strength) : base(name, health, strength)
        {
        }

        public Player(string name, int maxHealth, int strength, int dexterity, int endurance, int intelligence) : base(name, maxHealth, strength, dexterity, endurance, intelligence)
        {
        }

        public void AddMaxHealth(int points)
        {
            var minHealth = 10;
            if (MaxHealth + points > minHealth)
                MaxHealth += points;
            else
                throw new InvalidOperationException(
                    $"Invalid modification of max health. {nameof(MaxHealth)} : {MaxHealth}, {nameof(points)} : {points}");
        }
    }
}