using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models.Enum
{
    public enum DifficultyLevelEnum
    {
        // The easiest level of Monster, easy to kill, low damage to character
        Easy = 10,

        // Normal level Monster, will take a while to kill, medium damage 
        Medium = 12,

        // Usually the final boss of a round, hard to kill, high damage
        Hard = 14,
    }
}
