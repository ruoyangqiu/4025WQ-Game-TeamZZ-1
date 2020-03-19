using Game.Helpers;
using Game.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;

namespace Game.Models
{
    /// <summary>
    /// Entity model in the game. CharacterModel and MonsterModel are derived from this class. Defines attributes and methods that are common
    /// for both Character and Monster.
    /// </summary>
    public class EntityModel<T> : BaseModel<T>
    {
        #region Attributes

        #region Game Engine Attributes
        // The type of player, character comes before monster
        [Ignore]
        public PlayerTypeEnum PlayerType { get; set; } = PlayerTypeEnum.Unknown;

        [Ignore]
        // Check if the Entity is alive in the game
        public bool Alive { get; set; } = true;

        [Ignore]
        // Check if the Entity is asleep in the game
        public bool Asleep { get; set; } = false;

        // TurnOrder
        [Ignore]
        public int Order { get; set; } = 0;

        // Remember who was first into the list...
        [Ignore]
        public int ListOrder { get; set; } = 0;

        #endregion Game Engine Attributes

        #region Player Attributes
        // Value of attack attribute of the Entity
        public int Attack { get; set; } = 1;

        // Value of defence attribute of the Entity
        public int Defense { get; set; } = 1;

        // Value of health attribute of the Entity
        public int MaxHealth { get; set; } = 5;

        //Value of current health attribute of the Entity
        public int CurrentHealth { get; set; } = 5;

        // Value of Speed of the Entity
        public int Speed { get; set; } = 1;

        // Level of the monster
        public int Level { get; set; } = 1;

        // Total experience avaiable on this monster
        public int Experience { get; set; } = 0;

        // The Experience available to given up
        public int ExperienceRemaining { get; set; }

        // The Rate the item will drop
        public double DropRate { get; set; } = 1;

        // The Range of an Entity, Natral Range is 1
        public int Range { get; set; } = 1;

        [Ignore]
        // Character Class
        public CharacterClassEnum CharacterClass { get; set; } = CharacterClassEnum.Unknown;

        [Ignore]
        // Difficulty for this monster
        public DifficultyLevelEnum DifficultyLevel { get; set; } = DifficultyLevelEnum.Unknown;

        #endregion Player Attributes

        #endregion Attributes

        #region ItemIds

        // Item id for head location
        public string HeadId { get; set; }
        // Item for head
        [Ignore]
        public ItemModel Head { get; set; }


        // Item id for necklace
        public string NecklaceId { get; set; }

        // Item for necklace
        [Ignore]
        public ItemModel Necklace { get; set; }

        // Item id for primary hand
        public string PrimaryHandId { get; set; }
        // Item for primary hand
        [Ignore]
        public ItemModel PrimaryHand { get; set; }

        // Item id for off hand
        public string OffHandId { get; set; }

        // Item for off hand
        [Ignore]
        public ItemModel OffHand { get; set; }

        // Item id for Right Finger
        public string RightFingerId { get; set; }


        // Item for Right Finger
        [Ignore]
        public ItemModel RightFinger { get; set; }

        // Item id for Left Finger
        public string LeftFingerId { get; set; }

        // Item for Left Finger
        [Ignore]
        public ItemModel LeftFinger { get; set; }

        // Item id for feet
        public string FeetId { get; set; }

        // Item for feet
        [Ignore]
        public ItemModel Feet { get; set; }

        // The UniqueItem the Monster will drop
        public string UniqueItemId { get; set; }

        #endregion ItemIds

        #region Attributes Display

        #region Attack

        [Ignore]
        /// <summary>
        /// Get the bonus Attack from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemAttack { get { return GetItemBonus(AttributeEnum.Attack); } }

        [Ignore]
        /// <summary>
        /// Calculate Attack bonus based on level
        /// </summary>
        public int GetLevelBonusAttack { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Attack; } }

        [Ignore]
        /// <summary>
        /// Get the bonus Attack from Class
        /// </summary>
        /// <returns></returns>
        public int GetClassBonusAttack { get {
                var myreturn = 0;
                if(CharacterClass == CharacterClassEnum.Fighter)
                {
                    myreturn = 2 * Level;
                }
                return myreturn; 
            } }

        [Ignore]
        // Return the Total of All Attack
        public int GetAttackTotal { get { return GetAttack(); } }

        #endregion Attack

        #region Defense

        [Ignore]
        /// <summary>
        /// Get the bonus Defense from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemDefense { get { return GetItemBonus(AttributeEnum.Defense); } }

        [Ignore]
        /// <summary>
        /// Calculate Defense bonus based on level
        /// </summary>
        public int GetLevelBonusDefense { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Defense; } }

        [Ignore]
        /// <summary>
        /// Get the bonus Defense from Class
        /// </summary>
        /// <returns></returns>
        public int GetClassBonusDefense
        {
            get
            {
                var myreturn = 0;
                if (CharacterClass == CharacterClassEnum.Cleric)
                {
                    myreturn = 2 * Level;
                }
                return myreturn;
            }
        }

        [Ignore]
        // Return the Total of All Defense
        public int GetDefenseTotal { get { return GetDefense(); } }
        #endregion Defense

        #region MaxHealth

        [Ignore]
        /// <summary>
        /// Get the bonus MaxHealth from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemMaxHealth { get { return GetItemBonus(AttributeEnum.MaxHealth); } }

        [Ignore]
        /// <summary>
        /// Calculate MaxHealth bonus based on level
        /// </summary>
        public int GetLevelBonusMaxHealth
        {
            get
            {
                //var NewHealthAddition = DiceHelper.RollDice(Level - 1, 10);
                return 0;
            }
        }

        [Ignore]
        /// <summary>
        /// Get the bonus Attack from Class
        /// </summary>
        /// <returns></returns>
        public int GetClassBonusMaxHealth
        {
            get
            {
                var myreturn = 0;
                if (CharacterClass == CharacterClassEnum.Tank)
                {
                    myreturn = 2 * Level;
                }
                return myreturn;
            }
        }

        [Ignore]
        // Return the Total of All MaxHealth
        public int GetMaxHealthTotal { get { return GetMaxHealth(); } }
        #endregion MaxHealth

        #region CurrentHealth
        [Ignore]
        // Return the Total of All CurrentHealth
        public int GetCurrentHealthTotal { get { return GetCurrentHealth(); } }

        #endregion CurrentHealth

        #region Speed

        [Ignore]
        /// <summary>
        /// Get the bonus Attack from Item
        /// </summary>
        /// <returns></returns>
        public int GetItemSpeed { get { return GetItemBonus(AttributeEnum.Speed); } }

        [Ignore]
        /// <summary>
        /// Calculate Attack bonus based on level
        /// </summary>
        public int GetLevelBonusSpeed { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Speed; } }

        [Ignore]
        /// <summary>
        /// Get the bonus Speed from Class
        /// </summary>
        /// <returns></returns>
        public int GetClassBonusSpeed
        {
            get
            {
                var myreturn = 0;
                if (CharacterClass == CharacterClassEnum.Assasin)
                {
                    myreturn = 2 * Level;
                }
                return myreturn;
            }
        }

        [Ignore]
        // Return the Total of All Speed
        public int GetSpeedTotal { get { return GetSpeed(); } }
        #endregion Speed

        #region Damage
        [Ignore]
        // Return the Damage value, it is 25% of the Level rounded up
        public int GetDamageLevelBonus { get { return Convert.ToInt32(Math.Ceiling(Level * .25)); } }

        [Ignore]
        // Return the Damage with Item Bonus
        public int GetDamageItemBonus
        {
            get
            {
                var myItem = ItemIndexViewModel.Instance.GetItem(PrimaryHandId);
                if (myItem == null)
                {
                    return 0;
                }
                return myItem.Damage;
            }
        }

        [Ignore]
        // Return the Damage Dice if there is one
        public string GetDamageItemBonusString
        {
            get
            {
                var data = GetDamageItemBonus;
                if (data == 0)
                {
                    return "-";
                }

                return string.Format("1D {0}", data);
            }
        }

        [Ignore]
        // Return the Total of All Damage
        public string GetDamageTotalString
        {
            get
            {

                if (GetDamageItemBonusString.Equals("-"))
                {
                    return GetDamageLevelBonus.ToString();
                }

                return GetDamageLevelBonus.ToString() + " + " + GetDamageItemBonusString;

            }
        }

        [Ignore]
        public int GetDamageTotal { get { return GetDamage(); } }
        #endregion Damage

        #endregion Attributes Display

        #region Basic Methods
        public virtual string FormatOutput() { return ""; }

        public int GetRange()
        {
            var myReturn = Range;

            myReturn += GetItemRange();

            return myReturn;
        }

        // Get attack value
        public int GetAttack()
        {

            var myReturn = Attack;

            myReturn += GetItemAttack;

            myReturn += GetLevelBonusAttack;

            myReturn += GetClassBonusAttack;

            return myReturn;
        }

        // Get defense value
        public int GetDefense()
        {
            var myReturn = Defense;

            myReturn += GetLevelBonusDefense;

            myReturn += GetItemDefense;

            myReturn += GetClassBonusDefense;

            return myReturn;
        }

        // Get maxhealth value
        public int GetMaxHealth()
        {
            var myReturn = MaxHealth;

            myReturn += GetItemMaxHealth;

            myReturn += GetLevelBonusMaxHealth;

            myReturn += GetClassBonusMaxHealth;

            return myReturn;
        }

        // Get currenthealth value
        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        // Get Speed value
        public int GetSpeed()
        {
            var myReturn = Speed;

            myReturn += GetItemSpeed;

            myReturn += GetLevelBonusSpeed;

            myReturn += GetClassBonusSpeed;

            return myReturn;
        }

        public int GetDamage()
        {
            var myReturn = GetDamageItemBonus;

            myReturn += GetDamageLevelBonus;
            return myReturn;
        }

        #endregion Basic Methods

        #region Items

        /// <summary>
        /// Get the Range value for the equipped primary weapon
        /// 
        /// If it has a positive value, return that
        /// 
        /// Else return 0
        /// </summary>
        /// <returns></returns>
        public int GetItemRange()
        {
            var weapon = GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if (weapon == null)
            {
                return 0;
            }

            if (weapon.Range < 0)
            {
                return 0;
            }

            return weapon.Range;
        }

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

        #region Level Method

        /// <summary>
        /// Add Experience
        /// </summary>
        /// <param name="newExperience"></param>
        /// <returns></returns>
        public bool AddExperience(int newExperience)
        {
            // Don't allow going lower in experience
            if (newExperience < 0)
            {
                return false;
            }

            // Increment the Experience
            Experience += newExperience;

            // Can't level UP if at max.
            if (Level >= LevelTableHelper.MaxLevel)
            {
                return false;
            }

            // Then check for Level UP
            // If experience is higher than the experience at the next level, level up is OK.
            if (Experience >= LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience)
            {
                return LevelUp();
            }
            return false;
        }

        /// <summary>
        /// Calculate The amount of Experience to give
        /// Reduce the remaining by what was given
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int CalculateExperienceEarned(int damage) 
        {
            if (damage < 1)
            {
                return 0;
            }

            int remainingHealth = Math.Max(CurrentHealth - damage, 0); 
            double rawPercent = (double)remainingHealth / (double)CurrentHealth;
            double deltaPercent = 1 - rawPercent;
            var pointsAllocate = (int)Math.Floor(ExperienceRemaining * deltaPercent);

            // Catch rounding of low values, and force to 1.
            if (pointsAllocate < 1)
            {
                pointsAllocate = 1;
            }

            // Take away the points from remaining experience
            ExperienceRemaining -= pointsAllocate;
            if (ExperienceRemaining < 0)
            {
                pointsAllocate = 0;
            }

            return pointsAllocate;
        }

        /// <summary>
        /// Level Up
        /// </summary>
        /// <returns></returns>
        public bool LevelUp()
        {
            for (var i = LevelTableHelper.Instance.LevelDetailsList.Count - 1; i > 0; i--)
            {
                // Check the Level
                // If the Level is > Experience for the Index, increment the Level.
                if (LevelTableHelper.Instance.LevelDetailsList[i].Experience <= Experience)
                {
                    var NewLevel = LevelTableHelper.Instance.LevelDetailsList[i].Level;

                    // When leveling up, the current health is adjusted up by an offset of the MaxHealth, rather than full restore
                    var OldCurrentHealth = CurrentHealth;
                    var OldMaxHealth = MaxHealth;

                    // Set new Health
                    // New health, is d10 of the new level.  So leveling up 1 level is 1 d10, leveling up 2 levels is 2 d10.
                    var NewHealthAddition = DiceHelper.RollDice(NewLevel - Level, 10);

                    // Increment the Max health
                    MaxHealth += NewHealthAddition;

                    // Calculate new current health
                    // old max was 10, current health 8, new max is 15 so (15-(10-8)) = current health
                    CurrentHealth = (MaxHealth - (OldMaxHealth - OldCurrentHealth));

                    // Set the new level
                    Level = NewLevel;

                    // Done, exit
                    return true;
                }
            }

           return false;
        }

        // Level up to a number, say Level 3
        public int LevelUpToValue(int Value)
        {
            // Adjust the experience to the min for that level.
            // That will trigger level up to happen

            if (Value < 0)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value <= Level)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value > LevelTableHelper.MaxLevel)
            {
                return Level;
            }

            AddExperience(LevelTableHelper.Instance.LevelDetailsList[Value].Experience + 1);

            return Level;
        }

        #endregion Level Method

        public void FallAsleep()
        {
            Asleep = true;
        }

        public void WakeUp()
        {
            Asleep = false;
     
        }

        public bool isAsleep()
        {
            return Asleep;
        }
    }
}