using Game.Helpers;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            switch(data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();
                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel SelectCharacterToAttack()
        {
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // Select the one with lowest currenthealth
            var Defender = CharacterList
                .Where(m => m.Alive)
                .OrderBy(m => m.CurrentHealth)
                .FirstOrDefault();
            
            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public EntityInfoModel SelectMonsterToAttack()
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            // Select the one with highest attack
            var Defender = MonsterList
                .Where(m => m.Alive)
                .OrderBy(m => m.Attack)
                .LastOrDefault();

            return Defender;
        }

        /// <summary>
        /// Attack Action Deatil
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public bool TurnAsAttack(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            if(Attacker == null)
            {
                return false;
            }

            if(Target == null)
            {
                return false;
            }

            BattleMessageModel.ClearMessages();

            CalculateAttackStatus(Attacker, Target);

            switch(BattleMessageModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss
                    
                    break;

                case HitStatusEnum.Hit:
                    // It's a Hit

                    BattleMessageModel.DamageAmount = Attacker.GetDamageRollValue();

                    ApplyDamage(Target);

                    BattleMessageModel.TurnMessageSpecial = BattleMessageModel.GetCurrentHealthMessage();

                    // Check if the target dead. If dead, remove it

                    RemoveIfDead(Target);

                    // If Attacker is a Character, it should gain experience from Monster

                    CalculateExperience(Attacker, Target);

                    break;
            }
            BattleMessageModel.TurnMessage = Attacker.Name + BattleMessageModel.AttackStatus + Target.Name + BattleMessageModel.TurnMessageSpecial + BattleMessageModel.ExperienceEarned;
            Debug.WriteLine(BattleMessageModel.TurnMessage);
            return true;
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        /// <param name="Target"></param>
        private void ApplyDamage(EntityInfoModel Target)
        {
            Target.TakeDamage(BattleMessageModel.DamageAmount);
            BattleMessageModel.CurrentHealth = Target.GetCurrentHealthTotal;
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public HitStatusEnum CalculateAttackStatus(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            // Remember Current Player
            BattleMessageModel.PlayerType = PlayerTypeEnum.Monster;

            BattleMessageModel.TargetName = Target.Name;
            BattleMessageModel.AttackerName = Attacker.Name;
            if(Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                BattleMessageModel.HitStatus = HitStatusEnum.Hit;
                BattleMessageModel.AttackStatus = " Hit hit ";
                return BattleMessageModel.HitStatus;
            }

            //Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttackTotal;
            var DefenseScore = Target.Level + Target.GetDefenseTotal;

            BattleMessageModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);
            //BattleMessageModel.HitStatus = HitStatusEnum.Miss;
            //BattleMessageModel.AttackStatus = "Miss";
            return BattleMessageModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(EntityInfoModel Attacker, EntityInfoModel Target)
        {
            if(Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(BattleMessageModel.DamageAmount);
                BattleMessageModel.ExperienceEarned = " Earned " + experienceEarned + " points";

                var levelup = Attacker.AddExperience(experienceEarned);
                if(levelup)
                {
                    BattleMessageModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(BattleMessageModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                BattleScore.ExperienceGainedTotal += experienceEarned;
            }
            return true;
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(EntityInfoModel Target)
        {
            //Check if dead
            if(Target.Alive == false)
            {
                TargetDied(Target);
                return true;
            }
            return false;
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
            // Mark Status in output
            BattleMessageModel.TurnMessageSpecial = " and causes death. ";

            // Remove target from list...

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the MonsterModel to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    CharacterList.Remove(Target);
                    PlayerList.Remove(Target);

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    MonsterList.Remove(Target);
                    PlayerList.Remove(Target);

                    return true;
            }
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
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                BattleMessageModel.AttackStatus = " rolls 1 to completly miss ";

                // Force Miss
                BattleMessageModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessageModel.HitStatus;
            }

            if (d20 == 20)
            {
                BattleMessageModel.AttackStatus = " rolls 20 for lucky hit ";

                // Force Hit
                BattleMessageModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessageModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessageModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                BattleMessageModel.HitStatus = HitStatusEnum.Miss;
                BattleMessageModel.DamageAmount = 0;
                return BattleMessageModel.HitStatus;
            }

            BattleMessageModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            BattleMessageModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessageModel.HitStatus;
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
