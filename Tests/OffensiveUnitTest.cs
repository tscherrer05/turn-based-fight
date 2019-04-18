using System;
using Game.Model;
using Game.Utils;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OffensiveUnitTest
    {
        [Test]
        public void RandomDamageIsNotUnderMinDamage()
        {
            var weapon = new Weapon("name", 10, 15, EffectType.Fire);
            Assert.GreaterOrEqual(weapon.RandomDamages(), 10);
            Assert.LessOrEqual(weapon.RandomDamages(), 15);
        }

        [Test]
        public void OwNoeICantCreateAnInvalidWeapon()
        {
            // MinPower > MaxPower
            Assert.Throws<ArgumentException>(() => new Weapon("sdjsdf", 10000, 1, EffectType.Fire));
        }
    }
}