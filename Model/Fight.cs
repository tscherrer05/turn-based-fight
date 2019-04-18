using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Utils;

namespace Game.Model
{
    public class Fight
    {
        private Turn _currentTurn = null;

        public Fight(List<Entity> playerTeam, List<Entity> opponentTeam)
        {
            PlayerTeam = playerTeam.AsReadOnly();
            OpponentTeam = opponentTeam.AsReadOnly();
        }

        public ReadOnlyCollection<Entity> PlayerTeam;
        public ReadOnlyCollection<Entity> OpponentTeam;

        public bool IsOver => WinningTeam() != null;

        public ReadOnlyCollection<Entity> WinningTeam()
        {
            if(PlayerTeam.All(e => e.Defeated))
                return PlayerTeam;
            if(OpponentTeam.All(e => e.Defeated))
                return OpponentTeam;
            return null;
        }

        public Turn NextTurn(OffensiveAction playerAction)
        {
            var actions = new List<OffensiveAction> {playerAction};
            foreach(var entity in PlayerTeam.Where(e => !(e is Player)))
                actions.Add(new OffensiveAction(entity, OpponentTeam.ToList(), new Weapon("truc", 10, 20, EffectType.Blunt)));
            
            foreach(var entity in OpponentTeam)
                actions.Add(new OffensiveAction(entity, PlayerTeam.ToList(), new Weapon("truc", 10, 20, EffectType.Blunt)));
            
            var turn = new Turn(actions);
            _currentTurn = turn;
            return turn;
        }

        public bool CurrentTurnIsOver()
        {
            return _currentTurn.IsOver;
        }
    }
}