using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    public class MonsterModelTests
    {
        [TearDown]
        public void TearDown()
        {
            ItemIndexViewModel.Instance.Dataset.Clear();
        }

        [Test]
        public void MonsterModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new MonsterModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterModel_Constructor_New_Item_Should_Copy()
        {
            // Arrange
            var dataNew = new MonsterModel();
            dataNew.Attack = 2;
            dataNew.Id = "oldID";

            // Act
            var result = new MonsterModel(dataNew);

            // Reset

            // Assert 
            Assert.AreNotEqual("oldID", result.Id);
        }

        [Test]
        public void MonsterModel_Set_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new MonsterModel();
            result.Attack = 6;
            result.Defense = 7;
            result.Speed = 8;

            // Reset

            // Assert 
            Assert.AreEqual(6, result.Attack);
            Assert.AreEqual(7, result.Defense);
            Assert.AreEqual(8, result.Speed);

            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.Id, result.Guid);
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);

            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(1, result.Level);
            Assert.AreEqual(0, result.Experience);
            Assert.AreEqual(5, result.CurrentHealth);
            Assert.AreEqual(5, result.MaxHealth);

            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklace);
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);
        }
    }
}
