using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine control the turn
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : BaseEngine
    {

        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(EntityInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.
            var result = Attack(Attacker);

            BattleScore.TurnCount ++;

            return result;
        }

        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(EntityInfoModel Attacker)
        {
            if(BattleScore.AutoBattle)
            {
                // For attack, choose who
                CurrentDefender = AttackChoice(Attacker);

                if(CurrentDefender == null)
                {
                    return false;
                }
            }

            TurnAsAttack(Attacker, CurrentDefender);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public EntityInfoModel AttackChoice(EntityInfoModel data)
        {
            return data;
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel SelectCharacterToAttack()
        {
            return null;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel SelectMonsterToAttack()
        {
            return null;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public bool TurnAsAttack(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            return true;
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        /// <param name="Target"></param>
        private void ApplyDamage(EntityInfoModel Target)
        {

        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public HitStatusEnum CalculateAttackStatus(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            return true;
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(EntityInfoModel Target)
        {
            return true;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargetDied(EntityInfoModel Target)
        {
            return true;
        }

        /// <summary>
        /// Drop Item
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public int DropItems(EntityInfoModel Target)
        {
            return 0;
        }

        /// <summary>
        /// Roll to hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            return new List<ItemModel>();
        }
    }
}
