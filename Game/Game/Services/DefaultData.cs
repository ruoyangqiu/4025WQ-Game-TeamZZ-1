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
                    ImageURI = "https://i.ibb.co/KxFtsHr/01019.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Red Hron",
                    Description = "Gives bonus speed",
                    ImageURI = "https://i.ibb.co/hsqYC9S/01020.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Golden Hair pin",
                    Description = "Gives bonus damage",
                    ImageURI = "https://i.ibb.co/qd6Vcxs/01004.png",
                    Range = 0,
                    Damage = 15,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Emperor's Necklace",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "https://i.ibb.co/vQtJZVH/01021.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.MaxHealth},

                    new ItemModel
                    {
                        Name = "Phase boots",
                        Description = "Gives bonus speed",
                        ImageURI = "https://i.ibb.co/9WNvJ56/01015.png",
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
                    ImageURI = "https://i.ibb.co/txcMq2f/00949.png",
                    CharacterClass = CharacterClassEnum.Fighter,
                },

                new CharacterModel {
                    Name = "Zhu Bajie",
                    ImageURI = "https://i.ibb.co/8j9FGvt/00955.png", 
                    CharacterClass = CharacterClassEnum.Cleric,
                },

                new CharacterModel {
                    Name = "Yang Jian",
                    ImageURI = "https://i.ibb.co/31J4jLn/00950.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Hong Haier",
                    ImageURI = "https://i.ibb.co/QFmnbSQ/00951.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Jing Ke",
                    ImageURI = "https://i.ibb.co/PtbYrrB/00956.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Yang Guifei",
                    ImageURI = "https://i.ibb.co/Yfm0Fkx/00944.png",
                    CharacterClass = CharacterClassEnum.Cleric},

                new CharacterModel {
                    Name = "Xiao Long Nu",
                    ImageURI = "https://i.ibb.co/F7fngW9/00934.png",
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
                    ImageURI = "https://i.ibb.co/JdPLTBs/01148.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Hard,
                    Experience = 1000,
                },

                new MonsterModel {
                    Name = "Horseman",
                    ImageURI = "https://i.ibb.co/SrMKM5r/01143.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Easy,
                    Experience = 500,},

                new MonsterModel {
                    Name = "Jiangshi Bride",
                    ImageURI = "https://i.ibb.co/GMdPYqD/01118.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Berserker",
                    ImageURI = "https://i.ibb.co/R7mnV1j/01119.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Great Wolf",
                    ImageURI = "https://i.ibb.co/Kw9j2RY/01124.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Witch",
                    ImageURI = "https://i.ibb.co/dMJyQB1/01125.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},

                new MonsterModel {
                    Name = "Hell Pig",
                    ImageURI = "https://i.ibb.co/R9JbmK7/01138.png",
                    DifficultyLevel = Models.DifficultyLevelEnum.Medium,
                    Experience = 800,},
            };

            return datalist;
        }
    }
}