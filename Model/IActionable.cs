using System.Collections.Generic;

namespace Game.Model
{
    public interface IActionable
    {
        int Apply(Entity origin, List<Entity> targets);
    }
}