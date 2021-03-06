﻿using Game.Helpers;
using Game.Models;
using Game.ViewModels;
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
            bool result = false;

            // Check for confusion round or not, if it is confusion round and rolled confusion, skip turn
            if (EnableConfusionRound && IsConfusionRound && DiceHelper.RollDice(1, 20) - Attacker.Level > 0)
            {
                BattleMessageModel.TurnMessage = Attacker.Name + " is confused.";

                Debug.WriteLine(BattleMessageModel.TurnMessage);

                return true;
            }

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                if (BattleScore.TurnCount >= PlayerList.Count)
                {
                    Awake = true;
                }
                /*
                 * Order of Priority
                 * If can attack Then Attack
                 * Next use Ability or Move
                 */

                // Assume Move if nothing else happens
                CurrentAction = ActionEnum.Move;
                Debug.WriteLine(BattleScore.TurnCount);
                if (!Awake)
                {
                    CurrentAction = ActionEnum.Sleep;
                }
                // See if Desired Target is within Range, and if so attack away
                else if (MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
                {
                    CurrentAction = ActionEnum.Attack;
                }
            }
            else
            {
                if (SLEEPINGTEST)
                {
                    int listNo = Attacker.ListOrder;

                    if ((listNo * 2 + 1) == BattleScore.RoundCount)
                    {
                        foreach (var player in PlayerList)
                        {
                            if (player.PlayerType == PlayerTypeEnum.Monster)
                            {
                                player.FallAsleep();
                            }
                        }
                        Awake = false;
                    }
                }

                if (false)
                {
                    if (BattleScore.AutoBattle)
                    {
                        /*
                         * Order of Priority
                         * If can attack Then Attack
                         * Next use Ability or Move
                         */

                        // Assume Move if nothing else happens
                        CurrentAction = ActionEnum.Move;

                        // See if Desired Target is within Range, and if so attack away
                        if (MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
                        {
                            CurrentAction = ActionEnum.Attack;
                        }

                        // Simple Logic is Roll to Try Ability. 50% says try
                        else if (DiceHelper.RollDice(1, 10) > 5)
                        {
                            CurrentAction = ActionEnum.Ability;
                        }
                    }
                }
            }
            switch (CurrentAction)
            {
                case ActionEnum.Unknown:
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;
                case ActionEnum.Sleep:
                    result = FallAsleep(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker, TargetLocation);
                    break;
            }

            BattleScore.TurnCount++;

            return result;

            //return result;
        }

        //Sleeping Turn for Hackthon Scenario 7
        public bool FallAsleep(EntityInfoModel Attacker)
        {
            Debug.WriteLine(string.Format(Attacker.Name, " is sleeping"));
            BattleMessageModel.ClearMessages();
            BattleMessageModel.TurnMessage = Attacker.Name + " is sleeping";

            return true;
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool MoveAsTurn(EntityInfoModel Attacker, MapModelLocation targetLocation = null)
        {

            /*
             * TODO: TEAMS Work out your own move logic if you are implementing move
             * 
             * Mike's Logic
             * The monster or charcter will move to a different square if one is open
             * Find the Desired Target
             * Jump to the closest space near the target that is open
             * 
             * If no open spaces, return false
             * 
             */

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                // For Attack, Choose Who
                CurrentDefender = AttackChoice(Attacker);

                if (CurrentDefender == null)
                {
                    return false;
                }

                // Get X, Y for Defender
                var locationDefender = MapModel.GetLocationForPlayer(CurrentDefender);
                if (locationDefender == null)
                {
                    return false;
                }

                var locationAttacker = MapModel.GetLocationForPlayer(Attacker);
                if (locationAttacker == null)
                {
                    return false;
                }

                // Find Location Nearest to Defender that is Open.

                // Get the Open Locations
                var openSquare = MapModel.ReturnClosestEmptyLocation(locationDefender);

                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));

                BattleMessageModel.TurnMessage = Attacker.Name + " moves closer to " + CurrentDefender.Name;

                return MapModel.MovePlayerOnMap(locationAttacker, openSquare);
            }
            else if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var locationAttacker = MapModel.GetLocationForPlayer(Attacker);
                if (locationAttacker == null)
                {
                    return false;
                }

                var openSquare = TargetLocation;

                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));

                BattleMessageModel.TurnMessage = Attacker.Name + " moves to row: " + openSquare.Row + 1 + " column: " + openSquare.Column;

                return MapModel.MovePlayerOnMap(locationAttacker, openSquare);
            }

            return true;
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool UseAbility(EntityInfoModel Attacker)
        {
            BattleMessageModel.ClearMessages();
            BattleMessageModel.TurnMessageSpecial = " Used Ability";
            BattleMessageModel.TurnMessage = Attacker.Name + BattleMessageModel.TurnMessageSpecial;
            Debug.WriteLine(BattleMessageModel.TurnMessage);
            return true;
        }

        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(EntityInfoModel Attacker)
        {
            if (BattleScore.AutoBattle)
            {
                // For attack, choose who
                CurrentDefender = AttackChoice(Attacker);

                if (CurrentDefender == null)
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
            switch (data.PlayerType)
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
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleMessageModel.ClearMessages();

            CalculateAttackStatus(Attacker, Target);

            // Hackathon
            // Hackathon Scenario 2, Bob alwasys misses
            if (Attacker.Name.Equals("Bob"))
            {
                BattleMessageModel.HitStatus = HitStatusEnum.Miss;
                BattleMessageModel.TurnMessage = "Bob always Misses";
                Debug.WriteLine(BattleMessageModel.TurnMessage);
                return true;
            }

            if (PrimeNumber)
            {
                if (Attacker.PlayerType == PlayerTypeEnum.Character && IsPrime(Attacker))
                {
                    BattleMessageModel.HitStatus = HitStatusEnum.Hit;
                    BattleMessageModel.TurnMessage = "Prime always Hit";
                    //Debug.WriteLine(Attacker.TestDamage);
                    BattleMessageModel.DamageAmount = Attacker.GetDamageTotal;
                    Debug.WriteLine(BattleMessageModel.TurnMessage);
                    return true;
                }
            }


            switch (BattleMessageModel.HitStatus)
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
            if(Target.PlayerType == PlayerTypeEnum.Character)
            {
                UpdatePlayerList(Target);
            }
            return true;
        }

        /// <summary>
        /// Update PlayerList after Character was attacked. Because it didn't update the Character stored in PlayerList
        /// </summary>
        /// <param name="Target"></param>
        private void UpdatePlayerList(EntityInfoModel Target)
        {
            for(int i = 0; i < PlayerList.Count; i++)
            {
                if(PlayerList[i].PlayerType == PlayerTypeEnum.Character)
                {
                    if(PlayerList[i].Guid == Target.Guid)
                    {
                        PlayerList[i].CurrentHealth = Target.CurrentHealth;
                        if(PlayerList[i].Level != Target.Level)
                        {
                            PlayerList[i].LevelUpToValue(Target.Level);
                        }
                    }
                }
            }
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
            //if(Attacker.PlayerType == PlayerTypeEnum.Monster)
            //{
            //    BattleMessageModel.HitStatus = HitStatusEnum.Hit;
            //    BattleMessageModel.AttackStatus = " Hit hit ";
            //    return BattleMessageModel.HitStatus;
            //}

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
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(BattleMessageModel.DamageAmount);
                BattleMessageModel.ExperienceEarned = " Earned " + experienceEarned + " points";

                var levelup = Attacker.AddExperience(experienceEarned);
                if (levelup)
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
            if (Target.Alive == false)
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
            bool found;

            // Mark Status in output
            BattleMessageModel.TurnMessageSpecial = " and causes death. ";

            // Removing the 
            MapModel.RemovePlayerFromMap(Target);

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = CharacterList.Remove(CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = MonsterList.Remove(MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

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
            // Depends on Target Player Type,
            // It will generate different Drop Methods
            var result = 0;
            if (Target.PlayerType == PlayerTypeEnum.Character)
            {
                result = CharacterDropItem(Target);
            }

            if (Target.PlayerType == PlayerTypeEnum.Monster)
            {
                result = MonsterDropItem(Target);
            }
            return result;

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
        /// Roll dice to determine if the unique Item Drop
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public bool IsUniqueDrop(EntityInfoModel Target)
        {
            if (Target.PlayerType == PlayerTypeEnum.Character)
            {
                return false;
            }
            int rate = (int)(10 * Target.DropRate);
            int dice = DiceHelper.RollDice(1, 10);
            if (dice > rate)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Monster Drop Item
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public int MonsterDropItem(EntityInfoModel Target)
        {
            // Monster will only drop unique Item
            // The unique item will drop based on droprate
            var DroppedMessage = "\nItems Dropped : \n";


            if (string.IsNullOrEmpty(Target.UniqueItemId))
            {
                var data = GetRandomMonsterItemDrops();
                BattleScore.ItemsDroppedList += data.FormatOutput() + "\n";
                DroppedMessage += data.Name + "\n";

                ItemPool.Add(data);


                BattleMessageModel.DroppedMessage = DroppedMessage;

                BattleScore.ItemModelDropList.Add(data);

                return 1;
            }

            if (!IsUniqueDrop(Target))
            {
                DroppedMessage = " Nothing dropped. ";
                BattleMessageModel.DroppedMessage = DroppedMessage;
                return 0;
            }

            var ItemModel = ItemIndexViewModel.Instance.GetItem(Target.UniqueItemId);

            BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
            DroppedMessage += ItemModel.Name + "\n";

            ItemPool.Add(ItemModel);


            BattleMessageModel.DroppedMessage = DroppedMessage;

            BattleScore.ItemModelDropList.Add(ItemModel);

            return 1;
        }

        public int CharacterDropItem(EntityInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();


            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            BattleMessageModel.DroppedMessage = DroppedMessage;

            BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public ItemModel GetRandomMonsterItemDrops()
        {


            var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterItem());
            return data;
        }

        private bool IsPrime(EntityInfoModel Attacker)
        {
            int number = Attacker.GetAttackTotal + Attacker.GetDefenseTotal + Attacker.GetMaxHealthTotal + Attacker.GetSpeedTotal;
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
