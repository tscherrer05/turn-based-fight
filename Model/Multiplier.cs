using Game.Utils;

namespace Game.Model
{
    public class Multiplier
    {
        public int Value {get;}
        public EffectType EffectType {get;}

        public Multiplier(EffectType effect, int value)
        {
            Value = value;
            EffectType = effect;
        }
    }
}