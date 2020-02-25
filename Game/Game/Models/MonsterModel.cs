﻿using Game.Helpers;
using Game.Models.Enum;
using Game.Services;
using Game.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Game.Models
{
    /// <summary>
    /// Monster for the Game
    /// </summary>
    public class MonsterModel : EntityModel<MonsterModel>
    {
        // Difficulty for this monster
        public DifficultyLevelEnum DifficultyLevel { get; set; } = DifficultyLevelEnum.Unknown;

        // The UniqueItem the Monster will drop
        [Ignore]
        public ItemModel UniqueItem { get; set; }

        // The Rate the item will drop
        public double DropRate { get; set; } = 1;

        // The scale to buff Monster based on Level
        private int MonsterScale { get; set; } = 1;

        public const int MaxHealthPerLevel = 5;

        /// <summary>
        /// Default MonsterModel
        /// Establish the Default Image Path
        /// </summary>
        public MonsterModel()
        {
            Name = "";
            ImageURI = CharacterService.DefaultImageURI;
        }

        /// <summary>
        /// Constructor to create a Monster based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public MonsterModel(MonsterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override bool Update(MonsterModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            ImageURI = newData.ImageURI;
            Description = newData.Description;
            DifficultyLevel = newData.DifficultyLevel;
            ChangeAttributeByDifficultyLevel();

            Head = newData.Head;
            HeadId = Head == null ? "" : Head.Id;

            Necklace = newData.Necklace;
            NecklaceId = Necklace == null ? "" : Necklace.Id;

            PrimaryHand = newData.PrimaryHand;
            PrimaryHandId = PrimaryHand == null ? "" : PrimaryHand.Id;

            OffHand = newData.OffHand;
            OffHandId = OffHand == null ? "" : OffHand.Id;

            RightFinger = newData.RightFinger;
            RightFingerId = RightFinger == null ? "" : RightFinger.Id;

            LeftFinger = newData.LeftFinger;
            LeftFingerId = LeftFinger == null ? "" : LeftFinger.Id;

            Feet = newData.Feet;
            FeetId = Feet == null ? "" : Feet.Id;

            UniqueItem = newData.UniqueItem;
            UniqueItemId = UniqueItem == null ? "" : UniqueItem.Id;

            DropRate = newData.DropRate;

            return true;
        }


        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name;

            return myReturn.Trim();
        }

   


        // Helper to change attrributes based on cDifficultylevel
        public void ChangeAttributeByDifficultyLevel()
        {
            BasePropertyDetailsModel LittleMonster = new BasePropertyDetailsModel(1, 1, 1, 5);
            if (DifficultyLevel == DifficultyLevelEnum.Easy)
            {
                LittleMonster = BasePropertyHelper.Instance.MonsterDifficultyBase[DifficultyLevelEnum.Easy];
            }

            if (DifficultyLevel == DifficultyLevelEnum.Medium)
            {
                LittleMonster = BasePropertyHelper.Instance.MonsterDifficultyBase[DifficultyLevelEnum.Medium];
            }

            if (DifficultyLevel == DifficultyLevelEnum.Hard)
            {
                LittleMonster = BasePropertyHelper.Instance.MonsterDifficultyBase[DifficultyLevelEnum.Hard];
            }

            

            Attack = LittleMonster.Attack;
            Defense = LittleMonster.Defense;
            Speed = LittleMonster.Speed;
            MaxHealth = LittleMonster.MaxHealth;
        }
    }
}