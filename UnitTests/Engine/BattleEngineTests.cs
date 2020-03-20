using NUnit.Framework;
using Game.Engine;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Engine
{
    [TestFixture]
    public class BattleEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
