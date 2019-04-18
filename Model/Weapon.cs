using System;
using System.Collections.Generic;
using Game.Utils;

namespace Game.Model
{
    public class Weapon : Offensive
    {
        public Weapon(string name, int minPower, int maxPower, EffectType effect) : base(name, minPower, maxPower, effect)
        {
        }

        public override int Apply(Entity source, List<Entity> targets)
        {
            var damages = 0;
            foreach(var t in targets)
            {
                damages += source.Attack(t, this);
            }
            return damages;
        }
    }
}