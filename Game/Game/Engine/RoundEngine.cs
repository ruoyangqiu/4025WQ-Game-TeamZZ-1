using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;

            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
            List<MonsterModel> MonsterPool = new List<MonsterModel>(MonsterIndexViewModel.Instance.Dataset);
            for(int i = 0; i < MaxNumberPartyMonsters; i++)
            {
                var data = MonsterPool[i];

                MonsterList.Add(new EntityInfoModel(data));
            }

            return MonsterList.Count();
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

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
            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            if (BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                CurrentAttacker = GetNextPlayerTurn();
            }

            // Do the turn....
            TakeTurn(CurrentAttacker);

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public List<EntityInfoModel> RemoveDeadPlayersFromList()
        {
            PlayerList = PlayerList.Where(m => m.Alive == true).ToList();
            return PlayerList;
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
