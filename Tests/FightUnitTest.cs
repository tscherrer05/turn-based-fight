using System.Collections.Generic;
using Game.Model;
using Game.Utils;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class FightUnitTest
    {

        [Test]
        public void Fight_ShouldEndWhenATeamIsDead()
        {
            var team1 = new List<Entity> { new Player("name", 100) };
            var team2 = new List<Entity> { new Dummy("name", 100) };
            var fight = new Fight(team1, team2);
            Assert.False(fight.IsOver);
            team2.ForEach(e => e.Die());
            Assert.True(fight.IsOver);
        }


        [Test]
        public void Fight_NextTurn_ShouldReturnATurnWithActions()
        {
            // Arrange
            var player = new Player("name", 100);
            var dummy = new Dummy("name", 100);
            var weapon = new Weapon("weapon", 10, 20, EffectType.Blunt);
            var team1 = new List<Entity>() { player };
            var team2 = new List<Entity>() { dummy };
            var fight = new Fight(team1, team2);
            var playerAction = new OffensiveAction(player, team2, weapon); // Player choice (target(s) and actionable)

            // Act
            var turn = fight.NextTurn(playerAction); // Just call next turn

            // Assert
            Assert.NotNull(turn, "Turn should not be null");
            Assert.True(turn.Actions.Contains(playerAction), "Turn actions should contain player action");
            Assert.AreEqual(2, turn.Actions.Count, "There should be 2 actions");
        }

        [Test]
        public void Fight_WhenActionsAreNotPerformed_CurrentTurnIsNotOver()
        {
            // Arrange
            var player = new Player("name", 100);
            var dummy = new Dummy("name", 100);
            var weapon = new Weapon("weapon", 10, 20, EffectType.Blunt);
            var team1 = new List<Entity>() { player };
            var team2 = new List<Entity>() { dummy };
            var fight = new Fight(team1, team2);
            var playerAction = new OffensiveAction(player, team2, weapon); // Player choice (target(s) and actionable)

            // Act
            var turn = fight.NextTurn(playerAction);

            // Assert
            Assert.False(fight.CurrentTurnIsOver());
        }

        [Test]
        public void Fight_WhenAllActionsArePerformed_CurrentTurnIsOver()
        {
            // Arrange
            var player = new Player("name", 100);
            var dummy = new Dummy("name", 100);
            var weapon = new Weapon("weapon", 10, 20, EffectType.Blunt);
            var team1 = new List<Entity>() { player };
            var team2 = new List<Entity>() { dummy };
            var fight = new Fight(team1, team2);
            var playerAction = new OffensiveAction(player, team2, weapon); // Player choice (target(s) and actionable)

            // Act
            var turn = fight.NextTurn(playerAction);
            turn.PerformNextAction();
            turn.PerformNextAction();

            // Assert
            Assert.True(fight.CurrentTurnIsOver(), "Current turn should be over");
        }

        [Test]
        public void NextAction_ShouldPerformAction()
        {
            // Arrange
            var player = new Player("name", 100);
            var dummy = new Dummy("name", 100);
            var weapon = new Weapon("weapon", 10, 20, EffectType.Blunt);
            var team1 = new List<Entity> { player };
            var team2 = new List<Entity> { dummy };
            var fight = new Fight(team1, team2);
            var playerAction = new OffensiveAction(player, team2, weapon); // Player choice (target(s) and actionable)

            // Act
            var turn = fight.NextTurn(playerAction);
            turn.PerformNextAction();
            turn.PerformNextAction();

            // Assert
            Assert.Greater(playerAction.Damages, weapon.MinPower);
            Assert.Less(player.Health, player.MaxHealth);
            Assert.Less(dummy.Health, dummy.MaxHealth);
        }
    }
}