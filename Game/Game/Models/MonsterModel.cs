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
        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name;

            return myReturn.Trim();
        }
    }
}