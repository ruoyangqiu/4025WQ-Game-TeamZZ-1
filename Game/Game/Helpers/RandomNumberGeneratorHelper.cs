using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    /// <summary>
    /// Helper for generating random numbers
    /// </summary>
    static class RandomNumberGeneratorHelper
    {
        private static Random rng = new Random();
        /// <summary>
        /// Returns a random integer in range. MinValue is inclusive, maxvalue is exclusive.
        /// For example, GetRandomNumberInRange(1, 7) would be a dice roll
        /// </summary>
        public static int GetRandomNumberInRange(int minValue, int maxValue)
        {
            return rng.Next(minValue, maxValue);
        }
    }
}
