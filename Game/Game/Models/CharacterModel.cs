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
        public override bool Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            CharacterClass = newData.CharacterClass;

            //ChangeAttributeByClass();

            Level = newData.Level;

            MaxHealth = newData.MaxHealth;
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

            return true;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description;

            return myReturn.Trim();
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