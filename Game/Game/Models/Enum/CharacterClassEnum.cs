using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// The Type of Characters
    /// </summary>
    public enum CharacterClassEnum
    {
        Unknown = 0,

        // Fighte has highier Attack when created
        Fighter = 10,

        // Cleric has highier Defence when created
        Cleric = 12,
    }
}
