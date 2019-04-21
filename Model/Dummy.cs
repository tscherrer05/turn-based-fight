namespace Game.Model
{
    public class Dummy : Entity
    {
        public Dummy(string name, int maxHealth) : base(name, maxHealth)
        {
        }

        public Dummy(string name, int health, int strength) : base(name, health, strength)
        {
        }

        public Dummy(string name, int maxHealth, int strength, int dexterity, int endurance, int intelligence) : base(name, maxHealth, strength, dexterity, endurance, intelligence)
        {
        }
    }
}