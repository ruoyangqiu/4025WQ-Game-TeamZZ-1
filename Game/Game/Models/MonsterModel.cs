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
        public string HeadId { get; set; }

        // Item for necklace
        public string NecklaceId { get; set; }

        // Item for primary hand
        public string PrimaryHandId { get; set; }

        // Item for off hand
        public string OffHandId { get; set; }

        // Item for Right Finger
        public string RightFingerId { get; set; }

        // Item for Left Finger
        public string LeftFingerId { get; set; }

        // Item for feet
        public string FeetId { get; set; }

        // The UniqueItem the Monster will drop
        public string UniqueItem { get; set; }

        // The Rate the item will drop
        public double DropRate { get; set; } = 1;

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
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            MaxHealth = newData.MaxHealth;
            CurrentHealth = newData.MaxHealth;
            DifficultyLevel = newData.DifficultyLevel;
            Experience = newData.Experience;
            HeadId = newData.HeadId;
            NecklaceId = newData.NecklaceId;
            PrimaryHandId = newData.PrimaryHandId;
            OffHandId = newData.OffHandId;
            RightFingerId = newData.RightFingerId;
            LeftFingerId = newData.LeftFingerId;
            FeetId = newData.FeetId;
            UniqueItem = newData.UniqueItem;
            DropRate = newData.DropRate;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the character as a string
        public string FormatOutput()
        {
            var myReturn = Name;

            return myReturn.Trim();
        }

        // The damage monster receive
        public int MonsterTakeDamage(int damage)
        {
            return damage;
        }

        // The Experience gain by a character
        public int GiveExperience()
        {
            return 0;
        }

        // Check if the Monster deaad
        bool isMonsterAlive()
        {
            return CurrentHealth > 0;
        }

        // Get attack value
        int GetAttack()
        {
            return Attack;
        }

        // Get defense value
        int GetDefense()
        {
            return Defense;
        }

        // Get maxhealth value
        int GetMaxHealth()
        {
            return MaxHealth;
        }

        // Get currenthealth value
        int GetCurrrnetHealth()
        {
            return CurrentHealth;
        }

        // Get Speed value
        int GetSpeed()
        {
            return Speed;
        }

        // get the Dice to roll for the weapon used 
        int GetMonsterDamageDice()
        {
            return 0;
        }

        // get the calculated damage value this weapon rolled 
        int GetMonsterDamageRollValue()
        {
            return 0;
        }

    }
}