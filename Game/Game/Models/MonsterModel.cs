using Game.Models.Enum;
using Game.Services;

namespace Game.Models
{
    /// <summary>
    /// Monster for the Game
    /// </summary>
    public class MonsterModel : BaseModel<MonsterModel>
    {

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

        public DifficultyLevelEnum DifficultyLevel { get; set; }

        public int Experience { get; set; } = 0;

        // Item for head location
        public ItemModel Head { get; set; }

        // Item for necklace
        public ItemModel Necklace { get; set; }

        // Item for primary hand
        public ItemModel PrimaryHand { get; set; }

        // Item for off hand
        public ItemModel OffHand { get; set; }

        // Item for Right Finger
        public ItemModel RightFinger { get; set; }

        // Item for Left Finger
        public ItemModel LeftFinger { get; set; }

        // Item for feet
        public ItemModel Feet { get; set; }

        /// <summary>
        /// Default MonsterModel
        /// Establish the Default Image Path
        /// </summary>
        public MonsterModel() {
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
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            MaxHealth = newData.MaxHealth;
            CurrentHealth = newData.MaxHealth;
            DifficultyLevel = newData.DifficultyLevel;
            Experience = newData.Experience;
            Head = newData.Head;
            Necklace = newData.Necklace;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name;

            return myReturn.Trim();
        }
    }
}