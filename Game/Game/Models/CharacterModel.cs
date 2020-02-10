using Game.Services;

namespace Game.Models
{
    /// <summary>
    /// Character for the Game
    /// </summary>
    public class CharacterModel : BaseModel<CharacterModel>
    {
        // The Enum of Character Class. Every Character can only have one Class 
        public CharacterClassEnum CharacterClass { get; set; } = CharacterClassEnum.Unkown;

        /// <summary>
        /// Default CharacterModel
        /// Establish the Default Image Path
        /// </summary>
        public CharacterModel() {
            ImageURI = CharacterService.DefaultImageURI;
        }

        // Value of attack attribute of the Character
        public int Attack { get; set; } = 0;

        // Value of defence attribute of the Character
        public int Defence { get; set; } = 0;

        // Value of health attribute of the Character
        public int MaxHealth { get; set; } = 0;

        //Value of current health attribute of the Character
        public int CurrentHealth { get; set; } = 0;

        // Value of Speed of the Character
        public int Speed { get; set; } = 0;

        // Level of the Character
        public int Level { get; set; } = 1;

        // Total Experience of Character
        public int Experience { get; set; } = 0;

        // Check if the Character is alive in the game
        public bool Alive { get; set; } = true;

        // Item for head location
        public ItemModel Head { get; set; }

        // Item for necklace
        public ItemModel Necklace { get; set; }

        // Item for primary hand
        public ItemModel PrimaryHand { get; set; }


        /// <summary>
        /// Constructor to create a character based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public CharacterModel(CharacterModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;

        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description;

            return myReturn.Trim();
        }
    }
}