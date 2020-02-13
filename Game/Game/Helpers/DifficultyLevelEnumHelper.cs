using Game.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Helpers
{
    static class DifficultyLevelEnumHelper
    {
        public static List<string> GetListDifficulty
        {
            get
            {
                var myList = Enum.GetNames(typeof(DifficultyLevelEnum)).ToList();
                return myList;
            }
        }

        public static DifficultyLevelEnum ConvertStringToEnum(string value)
        {
            return (DifficultyLevelEnum)Enum.Parse(typeof(DifficultyLevelEnum), value);
        }
    }
}

