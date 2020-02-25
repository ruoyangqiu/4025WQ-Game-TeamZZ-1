using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Helpers
{
    /// <summary>
    /// Helper for DifficultyLevelEnum
    /// </summary>
    static class DifficultyLevelEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Difficulty level
        /// </summary>
        public static List<string> GetList
        {
            get
            {
                var myList = Enum.GetNames(typeof(DifficultyLevelEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Difficulty level
        /// Removes the unknown
        /// </summary>
        public static List<string> GetListDifficulty
        {
            get
            {
                var myList = Enum.GetNames(typeof(DifficultyLevelEnum)).ToList().Where(m => m.ToString().Equals("Unknown") == false).ToList();
                return myList;
            }
        }
        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DifficultyLevelEnum ConvertStringToEnum(string value)
        {
            return (DifficultyLevelEnum)Enum.Parse(typeof(DifficultyLevelEnum), value);
        }
    }
}

