using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;
using System;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        ItemIndexViewModel ItemViewModel= ItemIndexViewModel.Instance;
        AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;

        [SetUp]
        public void Setup()
        {
            AutoBattleEngine = EngineViewModel.AutoBattleEngine;
            BattleEngine = EngineViewModel.Engine;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new EntityInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                Experience = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, AutoBattleEngine.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, AutoBattleEngine.BattleScore.RoundCount);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new EntityInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                Experience = 100,
                                Name = "Bob",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new EntityInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    Experience = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(19);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.BattleMessageModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new EntityInfoModel(
                            new CharacterModel
                            {
                                Speed = 200,
                                Level = 10,
                                CurrentHealth = 100,
                                Experience = 100,
                                Name = "Mike",
                            });

            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new EntityInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    Experience = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessageModel.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_47_Prime_Number_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *  47
             *  
             * Description: 
             *      Make a Character called Prime 
             *      Prime's sum of Attack, Defence, Speed and MaxHealth is a PrimeNumber
             *      Prime Always Hit
             *      Prime Always deal max damage
             *      Other Characters cannot deal max damage
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      
             *      Check if the attack's attributes' total value is prime
             *      Set BattleMessage.DamageAmount = TotalDamage and return hit
             *                 
             * Test Algrorithm:
             *  Create Character named Prime
             *  Set the relative attributes: Attack, MaxHealth, Defence and Speed
             *  So that the sum of them is a prime number.
             *  Give a primaryHand item to Characterr to test if
             *  Prime can do the max damage
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Prime
             *  Test with Character of NotPrime
             * 
             * Validation:
             *      Verify Enum is Hit for Prime
             *      
             *      Verify Prime's BattleDamageAmount is Prime's TotalDamage
             *      
             *      
             *  
             */

            //Arrange

            // Set Character Conditions
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(0);
            BattleEngine.MaxNumberPartyCharacters = 1;

            var TestSword = ItemViewModel.Dataset.Where(a => a.Name.Equals("Golden Hair pin")).FirstOrDefault();
            var CharacterPlayer = new EntityInfoModel(
                            new CharacterModel
                            {
                                Level = 10,
                                CurrentHealth = 200,
                                MaxHealth = 200,
                                Experience = 100,
                                Name = "Prime",
                            }) ;
            CharacterPlayer.Attack = 25;
            CharacterPlayer.Speed = 20;
            CharacterPlayer.Defense = 26;
            CharacterPlayer.AddItem(ItemLocationEnum.PrimaryHand, TestSword.Id);
            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new EntityInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    Experience = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(10);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessageModel.HitStatus);
           // Console.WriteLine(CharacterPlayer.TestDamage);
            Assert.AreEqual(CharacterPlayer.GetDamageTotal, BattleEngine.BattleMessageModel.DamageAmount);
        }

        [Test]
        public void HackathonScenario_Scenario_47_NotPrime_Should_Not_do_Full_Damage()
        {
            /* 
             * Scenario Number:  
             *  47
             *  
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Default Test
             *                 
             * Test Algrorithm:
             *  Create Character named NotPrime
             *  Set the Attributes of NotPrime to be a non Prime number
             *  Give Not Prime a weapon
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Prime
             *  Test with Character of NotPrime
             * 
             * Validation:
             *      Verify Enum is Hit for Prime
             *      
             *      Verify Prime's BattleDamageAmount is Prime's TotalDamage
             *      
             *      
             *  
             */

            //Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(0);
            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;
            var TestSword = ItemViewModel.Dataset.Where(a => a.Location == ItemLocationEnum.PrimaryHand).FirstOrDefault();
            var CharacterPlayer = new EntityInfoModel(
                            new CharacterModel
                            {
                                Level = 10,
                                CurrentHealth = 200,
                                MaxHealth = 200,
                                //TestDamage = 123,
                                Experience = 100,
                                Name = "NotPrime",
                            });
            CharacterPlayer.Attack = 25;
            CharacterPlayer.Speed = 20;
            CharacterPlayer.Defense = 25;
            CharacterPlayer.AddItem(ItemLocationEnum.PrimaryHand, TestSword.Id);
            BattleEngine.CharacterList.Add(CharacterPlayer);

            // Set Monster Conditions

            // Add a monster to attack
            BattleEngine.MaxNumberPartyCharacters = 1;

            var MonsterPlayer = new EntityInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    Experience = 1,
                    Name = "Monster",
                });

            BattleEngine.CharacterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(10);

            //Act
            var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
            //Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessageModel.HitStatus);
            // Console.WriteLine(CharacterPlayer.TestDamage);
            Assert.Greater(CharacterPlayer.GetDamageTotal, BattleEngine.BattleMessageModel.DamageAmount);
        }
    }
}