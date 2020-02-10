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
                    Attribute = AttributeEnum.Defense},

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
                    Description = "Sun Wukong is a skilled fighter, capable of " +
                    "defeating the best warriors of heaven. His hair possesses " +
                    "magical properties, capable of summoning clones of the Monkey King " +
                    "himself, and/or into various weapons, animals, and other objects.",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png" },

                new CharacterModel {
                    Name = "Zhu Bajie",
                    Description = "Zhu Bajie looks like a terrible monster, part human and part pig, " +
                    "who often gets himself and his companions into trouble through his laziness, " +
                    "gluttony, and propensity for lusting after pretty women. He is jealous of " +
                    "Sun Wukong and always tries to bring him down.",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",},

                new CharacterModel {
                    Name = "Tang Sanzang",
                    Description = "Tang Sanzang was the founder of the Idealistic School. " +
                    "He translated abundant Buddhist canons and possessed an important position " +
                    "in Buddhist history. Xuanzang became a monk at the age of 12 and became a monk " +
                    "at the age of 21.",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",},
            };

            return datalist;
        }

    }
}