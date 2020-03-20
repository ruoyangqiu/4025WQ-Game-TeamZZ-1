using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public class CharacterModelTests
    {
        [TearDown]
        public void TearDown()
        {
            // For Tear down delete the Item Dataset.
            // Test that need the Item Dataset should set it
            ItemIndexViewModel.Instance.Dataset.Clear();
        }

        [Test]
        public void CharacterModel_Constructor_Default_Should_Pass()
        {
            // ArDefense

            // Act
            var result = new CharacterModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void CharacterModel_Constructor_New_Item_Should_Copy()
        {
            // ArDefense
            var dataNew = new CharacterModel();
            dataNew.Attack = 2;
            dataNew.Id = "oldID";

            // Act
            var result = new CharacterModel(dataNew);

            // Reset

            // Assert 
            Assert.AreNotEqual("oldID", result.Id);
        }

        [Test]
        public void CharacterModel_Get_Default_Should_Pass()
        {
            // ArDefense

            // Act
            var result = new CharacterModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result.Attack);
            Assert.IsNotNull(result.Defense);
            Assert.IsNotNull(result.Speed);
        }

        [Test]
        public void CharacterModel_Set_Default_Should_Pass()
        {
            // ArDefense

            // Act
            var result = new CharacterModel();
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

            Assert.AreEqual(PlayerTypeEnum.Character, result.PlayerType);

            Assert.AreEqual(true, result.Alive);
            Assert.AreEqual(0, result.Order);
            Assert.AreEqual(0, result.ListOrder);
            Assert.AreEqual(1, result.Level);
            Assert.AreEqual(0, result.ExperienceRemaining);
            Assert.AreEqual(5, result.CurrentHealth);
            Assert.AreEqual(5, result.MaxHealth);
            Assert.AreEqual(0, result.Experience);

            Assert.AreEqual(null, result.Head);
            Assert.AreEqual(null, result.Feet);
            Assert.AreEqual(null, result.Necklace);
            Assert.AreEqual(null, result.PrimaryHand);
            Assert.AreEqual(null, result.OffHand);
            Assert.AreEqual(null, result.RightFinger);
            Assert.AreEqual(null, result.LeftFinger);

            Assert.AreEqual(DifficultyLevelEnum.Unknown, result.DifficultyLevel);

            Assert.AreEqual(CharacterClassEnum.Unknown, result.CharacterClass);
        }
    }
}
