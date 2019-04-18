using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Model
{
    public class Turn
    {
        private readonly List<OffensiveAction> _actions;
        private readonly List<OffensiveAction> _performedActions;

        public Turn(List<OffensiveAction> actions)
        {
            Actions = actions.AsReadOnly();
            _actions = actions;
            _performedActions = new List<OffensiveAction>();
        }

        public ReadOnlyCollection<OffensiveAction> Actions {get;}

        public bool IsOver 
        { 
            get
            {
                return _performedActions.Count == _actions.Count;
            }
        }

        public OffensiveAction PerformNextAction()
        {
            var action = RandomAction();
            action.Perform();
            _performedActions.Add(action);
            return action;
        }

        private OffensiveAction RandomAction()
        {
            // TODO : select next action based on entities characteristics
            var rnd = new Random();
            var list = _actions.Where(a => !_performedActions.Contains(a)).ToList();
            var i = rnd.Next(list.Count);
            return list[i];
        }
    }
}