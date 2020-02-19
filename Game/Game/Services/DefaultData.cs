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
                    Name = "Gold Sword",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Strong Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Bunny Hat",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

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
                    Attack = 5,
                    Defense = 3,
                    Speed = 4,
                    MaxHealth = 40,
                    DifficultyLevel = Models.Enum.DifficultyLevelEnum.Hard,
                    Experience = 1000,
                },

                new MonsterModel {
                    Name = "Horseman",
                    ImageURI = "https://i.ibb.co/SrMKM5r/01143.png",
                    Attack = 2,
                    Defense = 1,
                    Speed = 2,
                    MaxHealth = 20,
                    DifficultyLevel = Models.Enum.DifficultyLevelEnum.Easy,
                    Experience = 500,},

                new MonsterModel {
                    Name = "Jiangshi Bride",
                    ImageURI = "https://i.ibb.co/GMdPYqD/01118.png",
                    Attack = 3,
                    Defense = 2,
                    Speed = 2,
                    MaxHealth = 30,
                    DifficultyLevel = Models.Enum.DifficultyLevelEnum.Medium,
                    Experience = 800,},
            };

            return datalist;
        }
    }
}