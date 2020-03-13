using Game.Models;
using System.Collections.Generic;

namespace Game.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {

                new ItemModel {
                    Name = "Blue Horn",
                    Description = "Gives bonus attack",
                    ImageURI = "blue_horn.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Red Horn",
                    Description = "Gives bonus speed",
                    ImageURI = "red_horn.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Golden Hair pin",
                    Description = "Gives bonus damage",
                    ImageURI = "golden_hair_pin.png",
                    Range = 0,
                    Damage = 15,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Emperor's Necklace",
                    Description = "Emperor's special item",
                    ImageURI = "emperor_necklace.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.MaxHealth},

                    new ItemModel
                    {
                        Name = "Phase boots",
                        Description = "Gives bonus speed",
                        ImageURI = "phase_boots.png",
                        Range = 0,
                        Damage = 0,
                        Value = 9,
                        Location = ItemLocationEnum.Feet,
                        Attribute = AttributeEnum.Speed,
                    },
            };

            return datalist;
        }

        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load the Default character data
        /// </summary>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Sun Wukong",
                    ImageURI = "character1.png",
                    CharacterClass = CharacterClassEnum.Fighter,
                    Level = 5
                },

                new CharacterModel {
                    Name = "Zhu Bajie",
                    ImageURI = "character7.png", 
                    CharacterClass = CharacterClassEnum.Cleric,
                },

                new CharacterModel {
                    Name = "Yang Jian",
                    ImageURI = "character2.png",
                    CharacterClass = CharacterClassEnum.Cleric,
                    Level = 3
                },

                new CharacterModel {
                    Name = "Hong Haier",
                    ImageURI = "character3.png",
                    CharacterClass = CharacterClassEnum.Cleric,
                    Level = 20
                },

                new CharacterModel {
                    Name = "Princess Tieshan",
                    ImageURI = "character4.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Yang Guifei",
                    ImageURI = "character5.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Xiao Long Nu",
                    ImageURI = "character6.png",
                    CharacterClass = CharacterClassEnum.Cleric},
            };

            return datalist;
        }

        /// <summary>
        /// Load the Default monster data
        /// </summary>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Tauren",
                    ImageURI = "monster1.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Hard,
                    Experience = 1000,
                },

                new MonsterModel {
                    Name = "Horseman",
                    ImageURI = "monster2.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Easy,
                    Experience = 500,},

                new MonsterModel {
                    Name = "Jiangshi Bride",
                    ImageURI = "monster3.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Berserker",
                    ImageURI = "monster4.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Great Wolf",
                    ImageURI = "monster5.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Witch",
                    ImageURI = "monster6.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Hell Pig",
                    ImageURI = "monster7.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},
            };

            return datalist;
        }
    }
}