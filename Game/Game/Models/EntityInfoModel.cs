using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class EntityInfoModel : EntityModel<EntityInfoModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public EntityInfoModel() { }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public EntityInfoModel(EntityInfoModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            //ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;

            MaxHealth = data.GetMaxHealthTotal;
            CurrentHealth = MaxHealth;
            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklace = data.Necklace;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            CharacterClass = data.CharacterClass;
            DifficultyLevel = data.DifficultyLevel;

            Attack = data.Attack;
            Defense = data.Defense;

        }

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public EntityInfoModel(CharacterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            Attack = data.Attack;
            Defense = data.Defense;
            //ExperienceRemaining = data.ExperienceRemaining;
            //Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            CharacterClass = data.CharacterClass;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            
            MaxHealth = data.MaxHealth;
            CurrentHealth = data.CurrentHealth;
            // Set the strings for the items
            HeadId = data.HeadId;
            PrimaryHandId = data.PrimaryHandId;
            OffHandId = data.OffHandId;
            NecklaceId = data.NecklaceId;
            RightFingerId = data.RightFingerId;
            LeftFingerId = data.LeftFingerId;
            Feet = data.Feet;
            if(data.Level > 1)
            {
                this.LevelUpToValue(data.Level);
                //this.LevelUp();
            }
        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public EntityInfoModel(MonsterModel data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            //ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            CurrentHealth = data.GetCurrentHealth();
            MaxHealth = data.GetMaxHealth();

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklace = data.Necklace;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            DifficultyLevel = data.DifficultyLevel;
        }

        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();

            if (PlayerType == PlayerTypeEnum.Character)
            {
                myReturn += " , Total Experience : " + Experience;
                myReturn += " , Damage : " + GetDamageTotalString;
                myReturn += " , Attack :" + GetAttackTotal;
                myReturn += " , Defense :" + GetDefenseTotal;
                myReturn += " , Speed :" + GetSpeedTotal;
            }

            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }
    }
}
