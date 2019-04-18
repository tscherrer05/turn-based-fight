using System;
using System.Collections.Generic;

namespace Game.Model
{
    public class OffensiveAction
    {
        public Entity Origin;
        public List<Entity> Targets;
        private IActionable Actionable;

        // TODO : dictionnary of damages target -> damage for this action
        public int Damages {get; private set;} = 0;

        public OffensiveAction(Entity origin, List<Entity> targets, IActionable actionable)
        {
            Origin = origin;
            Targets = targets;
            Actionable = actionable;
        }

        public void Perform()
        {
            Targets.ForEach(t => Damages += Actionable.Apply(Origin, Targets));
        }

    }
}