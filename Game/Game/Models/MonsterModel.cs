using Game.Helpers;
using Game.Models.Enum;
using Game.Services;
using SQLite;

namespace Game.Models
{
    /// <summary>
    /// Monster for the Game
    /// </summary>
    public class MonsterModel : EntityModel<MonsterModel>
    {
        // Difficulty for this monster
        public DifficultyLevelEnum DifficultyLevel { get; set; } = DifficultyLevelEnum.Unknown;

        // Level of the monster
        public int Level { get; set; } = 1;

        // Total experience avaiable on this monster
        public int Experience { get; set; } = 0;

        // The UniqueItem the Monster will drop
        public string UniqueItemId { get; set; }

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
        public override void Update(MonsterModel newData)
        {
            if (newData == null)
            {
                return;
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
            
        }


        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name;

            return myReturn.Trim();
        }

        // The damage monster receive
        public int TakeDamage(int damage)
        {
            return damage;
        }

        // The Experience gain by a character
        public int GiveExperience()
        {
            return 0;
        }

        // Check if the Monster deaad
        public bool isMonsterAlive()
        {
            return CurrentHealth > 0;
        }

        #region Attack
        // Get attack value
        public int GetAttack()
        {

            var myReturn = Attack;

            myReturn += GetItemAttack();

            myReturn += GetLevelBonusAttack();

            return myReturn;
        }

        /// <summary>
        /// Get the bonus Attack from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemAttack()
        {
            return GetItemBonus(AttributeEnum.Attack);
        }

        /// <summary>
        /// Calculate Attack bonus based on level
        /// </summary>
        public int GetLevelBonusAttack()
        {
            return LevelTableHelper.Instance.LevelDetailsList[Level].Attack;
        }

        #endregion Attack

        #region Defense
        // Get defense value
        public int GetDefense()
        {
            var myReturn = Defense;

            myReturn += GetLevelBonusDefense();

            myReturn += GetItemDefense();

            return myReturn;
        }

        /// <summary>
        /// Get the bonus Defense from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemDefense()
        {
            return GetItemBonus(AttributeEnum.Defense);
        }

        /// <summary>
        /// Calculate Defense bonus based on level
        /// </summary>
        public int GetLevelBonusDefense()
        {
            return LevelTableHelper.Instance.LevelDetailsList[Level].Defense;
        }


        #endregion Defense

        #region MaxHealth

        // Get maxhealth value
        public int GetMaxHealth()
        {
            var myReturn = MaxHealth;

            myReturn += GetItemMaxHealth();

            myReturn += GetLevelBonusMaxHealth();

            return myReturn;
        }

        /// <summary>
        /// Get the bonus MaxHealth from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemMaxHealth()
        {
            return GetItemBonus(AttributeEnum.MaxHealth);
        }

        /// <summary>
        /// Calculate MaxHealth bonus based on level
        /// </summary>
        public int GetLevelBonusMaxHealth()
        {
            return MaxHealthPerLevel * (Level - 1);
        }
        #endregion MaxHealth

        // Get currenthealth value
        public int GetCurrrnetHealth()
        {
            return CurrentHealth;
        }

        #region Speed
        // Get Speed value
        public int GetSpeed()
        {
            var myReturn = Speed;

            myReturn += GetItemSpeed();

            myReturn += GetLevelBonusSpeed();

            return myReturn;
        }

        /// <summary>
        /// Get the bonus Attack from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemSpeed()
        {
            return GetItemBonus(AttributeEnum.Speed);
        }

        /// <summary>
        /// Calculate Attack bonus based on level
        /// </summary>
        public int GetLevelBonusSpeed()
        {
            return LevelTableHelper.Instance.LevelDetailsList[Level].Speed;
        }

        #endregion Speed

        // get the Dice to roll for the weapon used 
        public int GetDamageDice()
        {
            return 0;
        }

        // get the calculated damage value this weapon rolled 
        public int GetDamageRollValue()
        {
            return 0;
        }

        #region GetItemBonus

        /// <summary>
        /// Get Bonus value from Item of given attribute
        /// </summary>
        /// <param name="attributeenum"></param>
        /// <returns></returns>
        public int GetItemBonus(AttributeEnum attributeenum)
        {
            var myReturn = 0;
            ItemModel myItem;

            myItem = Head;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = Necklace;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = PrimaryHand;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = OffHand;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = LeftFinger;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = RightFinger;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = Feet;
            if (myItem != null)
            {
                if (myItem.Attribute == attributeenum)
                {
                    myReturn += myItem.Value;
                }
            }
            return myReturn;
        }

        #endregion GetItemBonus

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