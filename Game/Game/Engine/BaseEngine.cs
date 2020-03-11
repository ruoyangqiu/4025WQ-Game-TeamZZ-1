using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Engine
{
    /// <summary>
    /// Hold data structure for battle engine
    /// </summary>
    public class BaseEngine
    {
        #region Properties
        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessageModel BattleMessageModel = new BattleMessageModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // List of Monsters
        public List<EntityInfoModel> MonsterList = new List<EntityInfoModel>();

        // List of Characters
        public List<EntityInfoModel> CharacterList = new List<EntityInfoModel>();

        // Current Player who is the attacker
        public EntityInfoModel CurrentAttacker;

        // Current Player who is the Defender
        public EntityInfoModel CurrentDefender;

        //Current Action
        public ActionEnum CurrentAction;

        // Hold the list of players (MonsterModel, and character by guid), and order by speed
        public List<EntityInfoModel> PlayerList = new List<EntityInfoModel>();

        // Current Round State
        public RoundEnum RoundStateEnum = RoundEnum.Unknown;

        // Max Number of Characters
        public int MaxNumberPartyCharacters = 6;

        // Max Number of Monsters
        public int MaxNumberPartyMonsters = 6;

        // Max Number of Rounds for AutoBattle
        public int MaxRoundCount = 1000;

        // Max Number of Turns for AutoBattle
        public int MaxTurnCount = 10000;

        // MapModel
        public MapModel MapModel = new MapModel();

        // Switch to enable ConfusionRound
        public bool EnableConfusionRound = false;

        // Is the round confusion round. For Hackathon Scenario 14
        public bool IsConfusionRound = false;

        #endregion Properties
    }
}
