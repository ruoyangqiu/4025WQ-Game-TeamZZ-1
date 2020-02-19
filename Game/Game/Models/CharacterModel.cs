using Game.Services;
using SQLite;
namespace Game.Models
{
    /// <summary>
    /// Character for the Game
    /// </summary>
    public class CharacterModel : EntityModel<CharacterModel>
    {

        // Level of the Character
        public int Level { get; set; } = 1;

        // Total Experience of Character
        public int Experience { get; set; } = 0;

        // Total Experience Needed to reach next level
        private int ExpToNextLevel { get; set; } = 0;

        // The Enum of Character Class. Every Character can only have one Class 
        [Ignore]
        public CharacterClassEnum CharacterClass { get; set; } = CharacterClassEnum.Unknown;

        /// <summary>
        /// Default CharacterModel
        /// Establish the Default Image Path
        /// </summary>
        public CharacterModel() {
            Name = "";
            var randomUriDescription = CharacterService.GetRandomURIDescription();
            ImageURI = randomUriDescription.Item1;
            Description = randomUriDescription.Item2;
        }

       

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
            CharacterClass = newData.CharacterClass;
            Level = newData.Level;
            ChangeAttributeByLevel();

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
        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description;

            return myReturn.Trim();
        }
        /// <summary>
        /// Scale the attribute wwhen Level up
        /// </summary>
        public void ScaleLevelUp()
        {
            Level = Level + 1;
            ChangeAttributeByLevel();
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// force Character level up to a target level
        /// </summary>
        /// <param name="newData">The target level</param>
        public int ForceUpToValue(int value)
        {
            if(value > 20)
            {
                return -1;
            }
            Level = value;
            ChangeAttributeByLevel();
            CurrentHealth = MaxHealth;
            return Level;
        }


        // Helper to change attrributes based on current level
        private void ChangeAttributeByLevel()
        {
            if(Level <= 20)
            {
                Attack = LevelTableHelper.Instance.LevelDetailsList[Level].Attack;
                Defense = LevelTableHelper.Instance.LevelDetailsList[Level].Defense;
                Speed = LevelTableHelper.Instance.LevelDetailsList[Level].Speed;
                if(Level < 20)
                {
                    ExpToNextLevel = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience;
                }
            }
            
        }
    }
}