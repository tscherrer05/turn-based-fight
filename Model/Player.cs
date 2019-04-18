using System;
using System.Text.RegularExpressions;

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
    }
}