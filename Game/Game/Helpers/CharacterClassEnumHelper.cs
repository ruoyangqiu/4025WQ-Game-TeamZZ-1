using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    static class CharacterClassEnumHelper
    {

        /// <summary>
        /// Returns a list of strings of the enum for Character Class
        /// Removes the unknown
        /// </summary>
        public static List<string> GetListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterClassEnum)).ToList().Where(m => m.ToString().Equals("Unknown") == false).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CharacterClassEnum ConvertStringToEnum(string value)
        {
            return (CharacterClassEnum)Enum.Parse(typeof(CharacterClassEnum), value);
        }
    }
}
}
