using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary.Json_Objects.Ranked_Objects
{
    public abstract class ModeStatsRanked
    {
        public CurrentTier currentTier { get; set; }

        public float currentRankPoint { get; set; }
        public BestTier bestTier { get; set; }

        public float bestRankPoint { get; set; }
        public float RoundsPlayed { get; set; }
        public float AvgRank { get; set; }
        public float AvgSurvivalTime { get; set; }
        public float Top10Ratio { get; set; }
        public float WinRatio { get; set; }
        public float Assists { get; set; }
        public float Wins { get; set; }
        public float Kda { get; set; }
        public float Kdr { get; set; }
        public float Kills { get; set; }
        public float Deaths { get; set; }
        public float RoundMostKills { get; set; }
        public float LongestKill { get; set; }
        public float HeadShotKills { get; set; }
        public float HeadShotKillRatio { get; set; }
        public float DamageDealt { get; set; }
        public float Dbnos { get; set; }
        public float ReviveRatio { get; set; }
        public float Revives { get; set; }
        public float Heals { get; set; }
        public float Boosts { get; set; }
        public float WeaponsAcquired { get; set; }
        public float TeamKills { get; set; }
        public float PlayTime { get; set; }
        public float KillStreak { get; set; }
    }
}
