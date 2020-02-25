using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Helpers
{
    /// <summary>
    /// Helper to manage BaseProperrty data
    /// </summary>
    class BasePropertyHelper
    {
        #region Singleton

        private static BasePropertyHelper _instance;

        public static BasePropertyHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BasePropertyHelper();
                }
                return _instance;
            }
        }

        #endregion Singleton

        // Dictionary to store the Monster's base properties based on difficulty level
        public Dictionary<DifficultyLevelEnum, BasePropertyDetailsModel> MonsterDifficultyBase;

        // Dictionary to store the Character's base properties based on their class
        public Dictionary<CharacterClassEnum, BasePropertyDetailsModel> CharacterClassBase;

        /// <summary>
        /// Constructor calls to clear the data
        /// </summary>
        public BasePropertyHelper()
        {
            ClearAndLoadDataTable();
        }

        /// <summary>
        /// Clear the data and reload it
        /// </summary>
        public void ClearAndLoadDataTable()
        {
            MonsterDifficultyBase = new Dictionary<DifficultyLevelEnum, BasePropertyDetailsModel>();
            CharacterClassBase = new Dictionary<CharacterClassEnum, BasePropertyDetailsModel>();
            LoadBasePropertyData();
        }

        /// <summary>
        /// Load data for each difficulty and class
        /// </summary>
        public void LoadBasePropertyData()
        {
            MonsterDifficultyBase.Add(DifficultyLevelEnum.Easy, new BasePropertyDetailsModel(1, 1, 1, 5));
            MonsterDifficultyBase.Add(DifficultyLevelEnum.Medium, new BasePropertyDetailsModel(3, 3, 3, 10));
            MonsterDifficultyBase.Add(DifficultyLevelEnum.Hard, new BasePropertyDetailsModel(5, 5, 5, 15));

            CharacterClassBase.Add(CharacterClassEnum.Fighter, new BasePropertyDetailsModel(3, 1, 1, 5));
            CharacterClassBase.Add(CharacterClassEnum.Cleric, new BasePropertyDetailsModel(1, 3, 1, 5));
        }
    }
}
