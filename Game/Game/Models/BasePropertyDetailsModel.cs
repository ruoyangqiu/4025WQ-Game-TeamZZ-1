using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Hold the details of base properties for each class of Character 
    /// and each difficulty of Monster
    /// </summary>
    class BasePropertyDetailsModel
    {
        // Base Attack 
        public int Attack;

        // Base Defense
        public int Defense;

        // Base Speed
        public int Speed;

        // Base MaxHealth
        public int MaxHealth;

        /// <summary>
        /// Create a new BasePeoperty with data passed in
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="speed"></param>
        /// <param name="maxhealth"></param>
        public BasePropertyDetailsModel(int attack, int defense, int speed, int maxhealth)
        {
            Attack = attack;
            Defense = defense;
            Speed = speed;
            MaxHealth = maxhealth;
        }
    }
}
