using SQLite;
using System;

namespace Game.Models
{
    /// <summary>
    /// Entity model in the game. CharacterModel and MonsterModel are derived from this class. Defines attributes and methods that are common
    /// for both Character and Monster.
    /// </summary>
    public class EntityModel<T> : BaseModel<T>
    {
        #region Attributes

        // Value of attack attribute of the Monster
        public int Attack { get; set; } = 0;

        // Value of defence attribute of the Monster
        public int Defense { get; set; } = 0;

        // Value of health attribute of the Monster
        public int MaxHealth { get; set; } = 0;

        //Value of current health attribute of the Monster
        public int CurrentHealth { get; set; } = 0;

        // Value of Speed of the Monster
        public int Speed { get; set; } = 0;

        #endregion Attributes

        // Check if the Character is alive in the game
        public bool Alive { get; set; } = true;

        #region Items

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

        #endregion Items
    }
}