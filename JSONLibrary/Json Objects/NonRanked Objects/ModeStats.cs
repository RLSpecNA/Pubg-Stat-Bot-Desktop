using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public abstract class ModeStats
    {
      
        public float Assists { get; set; }
        public float Boosts { get; set; }
        public float DBNOS { get; set; }
        public float DailyKills { get; set; }
        public float DailyWins { get; set; }
        public float DamageDealt { get; set; }
        public float Days { get; set; }
        public float HeadshotKills { get; set; }
        public float Heals { get; set; }
        public float KillPoints { get; set; }
        public float Kills { get; set; }
        public float LongestKill { get; set; }
        public float LongestTimeSurvived { get; set; }
        public float Losses { get; set; }
        public float MaxKillStreaks { get; set; }
        public float MostSurvivalTime { get; set; }
        public float RankPoints { get; set; }
        public string RankPointsTitle { get; set; }
        public float Revives { get; set; }
        public float RideDistance { get; set; }
        public float RoadKills { get; set; }
        public float RoundMostKills { get; set; }
        public float RoundsPlayed { get; set; }
        public float Suicides { get; set; }
        public float SwimDistance { get; set; }
        public float TeamKillsteamKills { get; set; }
        public float TimeSurvived { get; set; }
        public float Top10s { get; set; }
        public float VehicleDestroys { get; set; }
        public float WalkDistance { get; set; }
        public float WeaponsAcquired { get; set; }
        public float WeeklyKills { get; set; }
        public float WeeklyWins { get; set; }
        public float WinPoints { get; set; }
        public float Wins { get; set; }

    }
}
