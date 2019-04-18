

using System;
using System.Collections.Generic;
using Game.Utils;

namespace Game.Model
{
    public abstract class Offensive : IOffensive
    {
        public string Name {get;}

        public int MinPower {get;}

        public int MaxPower {get;}

        public EffectType Effect {get;}

        public int RandomDamages()
        {
            var rand = new Random();
            return (int)(rand.NextDouble() * (MaxPower - MinPower) + MinPower);
        }
                
        public override string ToString() => Name;
        
        public abstract int Apply(Entity origin, List<Entity> targets);

        public Offensive(string name, int minPower, int maxPower, EffectType effect)
        {
            if(minPower > maxPower)
                throw new ArgumentException(message: $"{nameof(minPower)} can't be greater than {nameof(maxPower)}");

            Name = name;
            MinPower = minPower;
            MaxPower = maxPower;  
            Effect = effect;
        }
    }
}