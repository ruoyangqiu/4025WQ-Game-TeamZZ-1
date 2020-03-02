using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine
{
    /// <summary>
    /// Manage the round functionality
    /// </summary>
    public class RoundEngine : TurnEngine
    {
        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        /// <returns></returns>
        private bool ClearLists()
        {
            ItemPool.Clear();
            MonsterList.Clear();
            return true;
        }

        // Call to make a new set of monsters...
        public bool NewRound()
        {
            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
            return 0;
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public void PickupItemsForAllCharacters()
        {

        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {
            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel GetNextPlayerTurn()
        {
            return null;
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public List<EntityInfoModel> RemoveDeadPlayersFromList()
        {
            return null;
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public List<EntityInfoModel> OrderPlayerListByTurnOrder()
        {
            return null;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public List<EntityInfoModel> MakePlayerList()
        {
            return null;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel GetNextPlayerInList()
        {
            return null;
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public bool PickupItemsFromPool(EntityInfoModel character)
        {
            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public bool GetItemFromPoolIfBetter(EntityInfoModel character, ItemLocationEnum setLocation)
        {
            return true;
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        private ItemModel SwapCharacterItem(EntityInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            return null;
        }
     }
}
