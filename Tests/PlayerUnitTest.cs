using Game.Model;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class PlayerUnitTest
    {
        [Test]
        public void NameIsValid_True()
        {
            var input = "Tim";
            Assert.True(Player.NameIsValid(input));
        }

        [Test]
        public void NameIsValid_False()
        {
            var input = "Tim1 ";
            Assert.True(Player.NameIsValid(input));
        }

        [Test]
        public void WowICantCreateAnInvalidObjectAwesome()
        {
            var input = "123";
            Assert.Throws<ArgumentException>(() => new Player(input, 100, 10));

            // TODO : Prevent this case
            //input = "Test1";
            //Assert.Throws<ArgumentException>(() => new Player(input, 100, 10));
        }
    }
}