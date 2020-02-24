using Game.Helpers;
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

        public const int MaxHealthPerLevel = 5;

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

            ChangeAttributeByClass();

            Level = newData.Level;
            

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

        // Check if the Character deaad
        public bool isAlive()
        {
            return CurrentHealth > 0;
        }

        // Get attack value
        public int GetAttack()
        {
            return Attack;
        }

        // Get defense value
        public int GetDefense()
        {
            return Defense;
        }

        // Get maxhealth value
        public int GetMaxHealth()
        {
            return MaxHealth;
        }

        // Get currenthealth value
        public int GetCurrrnetHealth()
        {
            return CurrentHealth;
        }

        // Get Speed value
        public int GetSpeed()
        {
            return Speed;
        }

        // get the Dice to roll for the weapon used 
        public int GetDamageDice()
        {
            return 0;
        }

        // get the calculated damage value this weapon rolled 
        int GetDamageRollValue()
        {
            return 0;
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
        /// if input value is greater than 20, it will 
        /// return -1 as a error notice
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

        /// <summary>
        /// Calculate MaxHealth bonus based on level
        /// </summary>
        public int LevelBonusMaxHealth()
        {
            return MaxHealthPerLevel * (Level - 1);
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

        /// <summary>
        /// Change the base Property of Character base on class
        /// </summary>
        public void ChangeAttributeByClass()
        {
            BasePropertyDetailsModel LittleCharacter = new BasePropertyDetailsModel(1, 1, 1, 5);
            if (CharacterClass == CharacterClassEnum.Fighter)
            {
                LittleCharacter = BasePropertyHelper.Instance.CharacterClassBase[CharacterClassEnum.Fighter];
            }

            if (CharacterClass == CharacterClassEnum.Cleric)
            {
                LittleCharacter = BasePropertyHelper.Instance.CharacterClassBase[CharacterClassEnum.Cleric];
            }
            Attack = LittleCharacter.Attack;
            Defense = LittleCharacter.Defense;
            Speed = LittleCharacter.Speed;
            MaxHealth = LittleCharacter.MaxHealth;
        }
    }
}