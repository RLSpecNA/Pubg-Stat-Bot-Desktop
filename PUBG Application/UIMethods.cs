using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using PUBG_Application.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Application
{
    public class UIMethods
    {

        public static async Task<List<RootMatch>> FilterUnrankedMatches(List<RootMatch> matches, Values.StatType type, int maxSize)
        {
            
            List<RootMatch> list = new List<RootMatch>();

            foreach(RootMatch match in matches)
            {
                string gameMode = match.data.attributes.gameMode;
                string matchType = match.data.attributes.matchType;

                if (gameMode == Values.GetEnumString(type).ToLower() && 
                    matchType == "official" && 
                    list.Count < maxSize)
                {
                    list.Add(match);  
                }

                if (list.Count == maxSize)
                {
                    break;
                }
            }

            return list;
        }
        public static async Task<List<RootMatch>> GetMatchesParsed(List<JSONLibrary.Json_Objects.AccountID.DataMatches> matches)
        {
            List<Task<RootMatch>> tasks = new List<Task<RootMatch>>();

            foreach(DataMatches match in matches)
            {
                tasks.Add(Task.Run(() => ParseMatch(match.id)));
            }

            var results = await Task.WhenAll(tasks);

            List<RootMatch> list = results.ToList();

            return list;
        }

        public static async Task<RootMatch> ParseMatch(string match_id)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor = new QueryExecutor(builder.GetMatchQuery(match_id));
            Tuple<string, int> tuple = await executor.ExecuteQueryAsync();

            RootMatch match = JsonParser.ParseMatchData(tuple).Item1;

            return match;
        }
        public static RankedObject GetRankedObject(int rankPoints)
        {


            // bronze
            if (rankPoints > 0 && rankPoints < 1500)
            {
                return new RankedObject(Resources.Bronze_5, Values.RankTitle.Silver, Values.RankLevel.V);
            }

            // silver
            else if (rankPoints >= 1500 && rankPoints < 2000)
            {
                if (rankPoints >= 1500 && rankPoints < 1600)
                {

                    return new RankedObject(Resources.Silver_5, Values.RankTitle.Silver, Values.RankLevel.V);
                }

                else if (rankPoints >= 1600 && rankPoints < 1700)
                {
                    new RankedObject(Resources.Silver_4, Values.RankTitle.Silver, Values.RankLevel.IV);
                }

                else if (rankPoints >= 1700 && rankPoints < 1800)
                {
                    return new RankedObject(Resources.Silver_3, Values.RankTitle.Silver, Values.RankLevel.III);
                }

                else if (rankPoints >= 1800 && rankPoints < 1900)
                {
                    return new RankedObject(Resources.Silver_2, Values.RankTitle.Silver, Values.RankLevel.II);
                }

                else if (rankPoints >= 1900 && rankPoints < 2000)
                {
                    return new RankedObject(Resources.Silver_1, Values.RankTitle.Silver, Values.RankLevel.I);
                }
            }

            // gold
            else if (rankPoints >= 2000 && rankPoints < 2500)
            {
                if (rankPoints >= 2000 && rankPoints < 2100)
                {
                    return new RankedObject(Resources.Gold_5, Values.RankTitle.Gold, Values.RankLevel.V);
                }

                else if (rankPoints >= 2100 && rankPoints < 2200)
                {
                    return new RankedObject(Resources.Gold_4, Values.RankTitle.Gold, Values.RankLevel.IV);
                }

                else if (rankPoints >= 2200 && rankPoints < 2300)
                {
                    return new RankedObject(Resources.Gold_3, Values.RankTitle.Gold, Values.RankLevel.III);
                }

                else if (rankPoints >= 2300 && rankPoints < 2400)
                {
                    return new RankedObject(Resources.Gold_2, Values.RankTitle.Gold, Values.RankLevel.II);
                }

                else if (rankPoints >= 2400 && rankPoints < 2500)
                {
                    return new RankedObject(Resources.Gold_1, Values.RankTitle.Gold, Values.RankLevel.I);
                }
            }

            // plat
            else if (rankPoints >= 2500 && rankPoints < 3000)
            {
                if (rankPoints >= 2500 && rankPoints < 2600)
                {
                    return new RankedObject(Resources.Platinum_5, Values.RankTitle.Platinum, Values.RankLevel.V);
                }

                else if (rankPoints >= 2600 && rankPoints < 2700)
                {
                    return new RankedObject(Resources.Platinum_4, Values.RankTitle.Platinum, Values.RankLevel.IV);
                }

                else if (rankPoints >= 2700 && rankPoints < 2800)
                {
                    return new RankedObject(Resources.Platinum_3, Values.RankTitle.Platinum, Values.RankLevel.III);
                }

                else if (rankPoints >= 2800 && rankPoints < 2900)
                {
                    return new RankedObject(Resources.Platinum_2, Values.RankTitle.Platinum, Values.RankLevel.II);
                }

                else if (rankPoints >= 2900 && rankPoints < 3000)
                {
                    return new RankedObject(Resources.Platinum_1, Values.RankTitle.Platinum, Values.RankLevel.I);
                }
            }

            // diamond
            else if (rankPoints >= 3000 && rankPoints < 3500)
            {
                if (rankPoints >= 3000 && rankPoints < 3100)
                {
                    return new RankedObject(Resources.Diamond_5, Values.RankTitle.Diamond, Values.RankLevel.V);

                }

                else if (rankPoints >= 3100 && rankPoints < 3200)
                {
                    return new RankedObject(Resources.Diamond_4, Values.RankTitle.Diamond, Values.RankLevel.IV);

                }

                else if (rankPoints >= 3200 && rankPoints < 3300)
                {
                    return new RankedObject(Resources.Diamond_3, Values.RankTitle.Diamond, Values.RankLevel.III);

                }

                else if (rankPoints >= 3300 && rankPoints < 3400)
                {
                    return new RankedObject(Resources.Diamond_2, Values.RankTitle.Diamond, Values.RankLevel.II);

                }

                else if (rankPoints >= 3400 && rankPoints < 3500)
                {
                    return new RankedObject(Resources.Diamond_1, Values.RankTitle.Diamond, Values.RankLevel.I);

                }
            }

            // master
            else if (rankPoints >= 3500)
            {
                return new RankedObject(Resources.Master, Values.RankTitle.Master, Values.RankLevel.Master);

            }

            // unranked
            else
            {
                return new RankedObject(Resources.Unranked, Values.RankTitle.Unranked, Values.RankLevel.Unranked);

            }

            return null;
        }

        public static ModeStats GetProperNormalStatsObject(Values.StatType statType, PanelPlayer player)
        {
            if (statType == Values.StatType.Solo)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.soloStats;
            }

            else if (statType == Values.StatType.Duo)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.duoStats;
            }

            else if (statType == Values.StatType.Squad)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.squadStats;
            }

            else if (statType == Values.StatType.SoloFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.soloFppStats;
            }

            else if (statType == Values.StatType.DuoFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.duoFppStats;
            }

            else if (statType == Values.StatType.SquadFPP)
            {
                return player.NormalStatsObj.data.attributes.gameModeStats.squadFPPStats;
            }

            else
            {
                return null;
            }
        }

        public static RankedObject ComputeStats(ModeStatsRanked stats)
        {
            if (stats != null)
            {
                RankedObject ranked = UIMethods.GetRankedObject((int)stats.currentRankPoint);


                ranked.GamesPlayed = (int)stats.RoundsPlayed;
                ranked.Wins = (int)stats.Wins;
                ranked.WinPercent = Math.Round(StatsCalculation.GetWinRatio(stats.Wins, stats.RoundsPlayed), 2);
                ranked.AverageRank = Math.Round(stats.AvgRank, 2);
                ranked.TopTenPercent = Math.Round(stats.Top10Ratio * 100, 2);
                ranked.Adr = (int)Math.Round(StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed), 0);
                ranked.Kd = Math.Round(StatsCalculation.GetKD(stats.Kills, stats.Deaths), 2);
                ranked.Kda = Math.Round(StatsCalculation.GetKDA(stats.Kills, stats.Assists, stats.Deaths), 2);
                ranked.DbnosPerRound = Math.Round(StatsCalculation.GetKnocksPerRound(stats.Dbnos, stats.RoundsPlayed), 2);

                return ranked;
            }
            else
            {
                RankedObject ranked = UIMethods.GetRankedObject(0);


                ranked.GamesPlayed = 0;
                ranked.Wins = 0;
                ranked.WinPercent = 0;
                ranked.AverageRank = 0;
                ranked.TopTenPercent = 0;
                ranked.Adr = 0;
                ranked.Kd = 0;
                ranked.Kda = 0;
                ranked.DbnosPerRound = 0;

                return ranked;
            }

        }
    }
}
