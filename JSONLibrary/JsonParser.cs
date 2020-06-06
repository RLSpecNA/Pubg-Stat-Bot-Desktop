using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public static class JsonParser
    {
        
        public static Tuple<RootAccountIDObject, int> ParseAccountID(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }
            else if (pair.Item1 == null)
            {
                Tuple<RootAccountIDObject, int> error = Tuple.Create<RootAccountIDObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootAccountIDObject obj = JsonConvert.DeserializeObject<RootAccountIDObject>(pair.Item1);
                Tuple<RootAccountIDObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }

        public static Tuple<RootRankedStatsObject, int> ParseRankedSeasonStats(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootRankedStatsObject, int> error = Tuple.Create<RootRankedStatsObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootRankedStatsObject obj = JsonConvert.DeserializeObject<RootRankedStatsObject>(pair.Item1);
                Tuple<RootRankedStatsObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }
        }
        public static Tuple<RootNormalStatsObject, int> ParseNormalSeasonStats(Tuple<string, int> pair)
        {
            if (pair == null)
            {
                return null;
            }

            else if (pair.Item1 == null)
            {
                Tuple<RootNormalStatsObject, int> error = Tuple.Create<RootNormalStatsObject, int>(null, pair.Item2);
                return error;
            }
            if (pair != null)
            {
                RootNormalStatsObject obj = JsonConvert.DeserializeObject<RootNormalStatsObject>(pair.Item1);
                Tuple<RootNormalStatsObject, int> response = Tuple.Create(obj, pair.Item2);

                return response;
            }
            else
            {
                return null;

            }

            /*
            if (json[0] != null || json[0].ToString() != string.Empty)
            {
                var jsonData = JObject.Parse(json[0].ToString()).Children();
                List<JToken> tokens = jsonData.Children().ToList(); // count 3

                if (tokens.Count == 3)
                {
                    var jsonData2 = JObject.Parse(tokens[0].ToString()).Children();
                    List<JToken> tokens2 = jsonData2.Children().ToList(); // count 3

                    if (tokens2.Count == 3)
                    {
                        var jsonData3 = JObject.Parse(tokens2[1].ToString()).Children();
                        List<JToken> tokens3 = jsonData3.Children().ToList(); // count 2

                        if (tokens3.Count == 2 && Double.TryParse(tokens3[0].ToString(), out double result0))
                        {
                            var jsonData4 = JObject.Parse(tokens3[1].ToString()).Children();
                            List<JToken> tokens4 = jsonData4.Children().ToList(); // count 6

                            if (tokens4.Count == 6)
                            {
                                Dictionary<Values.Modes, ModeStats> stats = new Dictionary<Values.Modes, ModeStats>();

                                stats.Add(Values.Modes.Duo, JsonConvert.DeserializeObject<DuoStats>(tokens4[0].ToString()));
                                stats.Add(Values.Modes.DuoFPP, JsonConvert.DeserializeObject<DuoFPPStats>(tokens4[1].ToString()));
                                stats.Add(Values.Modes.Solo, JsonConvert.DeserializeObject<SoloStats>(tokens4[2].ToString()));
                                stats.Add(Values.Modes.SoloFPP, JsonConvert.DeserializeObject<SoloFPPStats>(tokens4[3].ToString()));
                                stats.Add(Values.Modes.Squad, JsonConvert.DeserializeObject<SquadStats>(tokens4[4].ToString()));
                                stats.Add(Values.Modes.SquadFPP, JsonConvert.DeserializeObject<SquadFPPStats>(tokens4[5].ToString()));

                                object[] array = { stats, json[1] };
                                return array;
                            }
                        }
                        else if (tokens3.Count == 2 && Double.TryParse(tokens3[1].ToString(), out double result1))
                        {
                            var jsonData4 = JObject.Parse(tokens3[0].ToString()).Children();
                            List<JToken> tokens4 = jsonData4.Children().ToList(); // count 6

                            if (tokens4.Count == 6)
                            {
                                Dictionary<Values.Modes, ModeStats> stats = new Dictionary<Values.Modes, ModeStats>();

                                stats.Add(Values.Modes.Duo, JsonConvert.DeserializeObject<DuoStats>(tokens4[0].ToString()));
                                stats.Add(Values.Modes.DuoFPP, JsonConvert.DeserializeObject<DuoFPPStats>(tokens4[1].ToString()));
                                stats.Add(Values.Modes.Solo, JsonConvert.DeserializeObject<SoloStats>(tokens4[2].ToString()));
                                stats.Add(Values.Modes.SoloFPP, JsonConvert.DeserializeObject<SoloFPPStats>(tokens4[3].ToString()));
                                stats.Add(Values.Modes.Squad, JsonConvert.DeserializeObject<SquadStats>(tokens4[4].ToString()));
                                stats.Add(Values.Modes.SquadFPP, JsonConvert.DeserializeObject<SquadFPPStats>(tokens4[5].ToString()));

                                object[] array = { stats, json[1] };
                                return array;
                            }
                        }

                    }
                }
            }*/


            
        }
    }
}
