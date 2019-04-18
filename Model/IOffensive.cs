using Game.Utils;

namespace Game.Model
{
    public interface IOffensive : IActionable
    {
        string Name {get;}
        int MinPower {get;}
        int MaxPower {get;}
        EffectType Effect {get;}
        int RandomDamages();
    }
}