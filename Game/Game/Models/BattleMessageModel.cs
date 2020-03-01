﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Manage the message formatting for ui to display
    /// </summary>
    public class BattleMessageModel
    {
        #region Message Properties
        // Is the player a character or a monster
        public PlayerTypeEnum PlayerType = PlayerTypeEnum.Unknown;

        // The Status of the action
        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        // Name of the Attacker
        public string AttackerName = string.Empty;

        // Name of who the target was
        public string TargetName = string.Empty;

        // The status of the Attack
        public string AttackStatus = string.Empty;

        // Turn Message
        public string TurnMessage = string.Empty;

        // Turn Special Message
        public string TurnMessageSpecial = string.Empty;

        // Turn Experience Earned Message
        public string ExperienceEarned = string.Empty;

        // Level Up Message
        public string LevelUpMessage = string.Empty;

        // Amount of Damage
        public int DamageAmount = 0;

        // The Remaining Health Mesage
        public int CurrentHealth = 0;

        // Beginning of the Html Block for html formatting
        public string htmlHead = @"<html><body bgcolor=""#E8D0B6""><p>";

        // Ending of the Html Block for Html formatting
        public string htmlTail = @"</p></body></html>";

        #endregion Message Properties
    }
}