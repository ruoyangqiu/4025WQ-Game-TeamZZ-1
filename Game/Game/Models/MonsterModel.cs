using Game.Helpers;
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

        

        // The Experience gain by a character
        public int GiveExperience()
        {
            return 0;
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

        #region CurrentHealth
        // Get currenthealth value
        public int GetCurrrnetHealth()
        {
            return CurrentHealth;
        }

        #endregion CurrentHealth

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


        [Ignore]
        // Return the Damage value, it is 25% of the Level rounded up
        public int GetDamageLevelBonus { get { return Convert.ToInt32(Math.Ceiling(Level * .25)); } }



        #region Items

        // Get the Item at a known string location (head, foot etc.)
        public ItemModel GetItem(string itemString)
        {
            return ItemIndexViewModel.Instance.GetItem(itemString);
        }

        // Drop All Items
        // Return a list of items for the pool of items
        public List<ItemModel> DropAllItems()
        {
            var myReturn = new List<ItemModel>();

            // Drop all Items
            ItemModel myItem;

            myItem = RemoveItem(ItemLocationEnum.Head);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Necklass);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.OffHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.RightFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.LeftFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Feet);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            return myReturn;
        }

        // Remove ItemModel from a set location
        // Does this by adding a new ItemModel of Null to the location
        // This will return the previous ItemModel, and put null in its place
        // Returns the ItemModel that was at the location
        // Nulls out the location
        public ItemModel RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Get the ItemModel at a known string location (head, foot etc.)
        public ItemModel GetItemByLocation(ItemLocationEnum itemLocation)
        {
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    return GetItem(HeadId);

                case ItemLocationEnum.Necklass:
                    return GetItem(NecklaceId);

                case ItemLocationEnum.PrimaryHand:
                    return GetItem(PrimaryHandId);

                case ItemLocationEnum.OffHand:
                    return GetItem(OffHandId);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFingerId);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFingerId);

                case ItemLocationEnum.Feet:
                    return GetItem(FeetId);
            }

            return null;
        }

        // Add ItemModel
        // Looks up the ItemModel
        // Puts the ItemModel ID as a string in the location slot
        // If ItemModel is null, then puts null in the slot
        // Returns the ItemModel that was in the location
        public ItemModel AddItem(ItemLocationEnum itemLocation, string itemID)
        {
            var myReturn = GetItemByLocation(itemLocation);

            switch (itemLocation)
            {
                case ItemLocationEnum.Feet:
                    FeetId = itemID;
                    break;

                case ItemLocationEnum.Head:
                    HeadId = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    NecklaceId = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    PrimaryHandId = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    OffHandId = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    RightFingerId = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    LeftFingerId = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }

        /// <summary>
        /// Get Bonus value from Item of given attribute
        /// </summary>
        /// <param name="attributeenum"></param>
        /// <returns></returns>
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            ItemModel myItem;

            myItem = GetItem(HeadId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(NecklaceId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(PrimaryHandId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(OffHandId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(RightFingerId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(LeftFingerId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(FeetId);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }
            return myReturn;
        }

        /// <summary>
        /// Get the Items the Character has
        /// </summary>
        /// <returns></returns>
        public string ItemSlotsFormatOutput()
        {
            var myReturn = "";

            var data = ItemIndexViewModel.Instance.GetItem(UniqueItemId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(HeadId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(NecklaceId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(PrimaryHandId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(OffHandId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(RightFingerId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(LeftFingerId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            data = ItemIndexViewModel.Instance.GetItem(FeetId);
            if (data != null)
            {
                myReturn += data.FormatOutput();
            }

            return myReturn.Trim();
        }


        #endregion Items

        #region BattleMethod

        // Get the calculated damage value this weapon rolled 
        public int GetDamageRollValue()
        {
            var myReturn = 0;

            var myItem = ItemIndexViewModel.Instance.GetItem(PrimaryHandId);
            if (myItem != null)
            {
                // Dice of the weapon.  So sword of Damage 10 is d10
                myReturn += DiceHelper.RollDice(1, myItem.Damage);
            }

            // Add in the Level as extra damage per game rules
            myReturn += GetDamageLevelBonus;

            return myReturn;
        }

        // The damage monster receive
        public bool TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return false;
            }

            CurrentHealth = CurrentHealth - damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                CauseDeath();
            }

            return true;
        }

        // Death
        // Alivce turn to false
        public bool CauseDeath()
        {
            Alive = false;
            return Alive;
        }

        #endregion BattleMethod


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