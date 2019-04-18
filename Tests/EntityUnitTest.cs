using Game.Model;
using Game.Utils;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EntityUnitTest
    {
        [Test]
        public void Entity_WithCharacteristics()
        {
            var player = new Player("name", 100, 10, 9, 8, 7);
            Assert.AreEqual(10, player.Strength);
            Assert.AreEqual(9, player.Dexterity);
            Assert.AreEqual(8, player.Endurance);
            Assert.AreEqual(7, player.Intelligence);
        }

        [Test]
        public void Entity_WithStrengthAndBluntWeapon_ShouldDealMoreDamage()
        {
            var player1 = new Player("Weak", 100, 10);
            var player2 = new Player("Strong", 100, 20);
            var dummy = new Dummy("Dummy", 100, 10);
            var sword = new Weapon("Sword", 10, 10, EffectType.Blunt);
            var dmg1 = player1.Attack(dummy, sword);
            var dmg2 = player2.Attack(dummy, sword);
            Assert.Greater(dmg2, dmg1);
        }
    }
}