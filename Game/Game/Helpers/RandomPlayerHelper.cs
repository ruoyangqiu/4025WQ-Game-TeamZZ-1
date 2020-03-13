using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using Game.ViewModels;
using System.Linq;

namespace Game.Helpers
{
    public static class RandomPlayerHelper
    {
        /// <summary>
        /// Get Health
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetHealth(int level)
        {
            // Roll the Dice and reset the Health
            return DiceHelper.RollDice(level, 10);
        }
        /// <summary>
        /// Genrate am Item for Monster If they Don't have Unique Item
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterItem()
        {
            var result = ItemIndexViewModel.Instance.Dataset.ElementAt(DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count()) - 1).Id;

            return result;
        }
    }
}
