using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class QueryBuilder
    {
        private readonly string BASE_URL = "https://api.pubg.com/shards/steam/";

        public string GetAccountIDQuery(string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players?filter[playerNames]=" + name);

            return builder.ToString();
        }

        public string GetSeasonsListQuery()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "seasons");

            return builder.ToString();
        }

        public string GetSeasonForPlayerQuery(string account_id, string season_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players/" + account_id + "/seasons/" + season_id + "?filter[gamepad]=false");

            return builder.ToString();
        }

        public string GetRankedSeasonForPlayerQuery(string account_id, string season_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.BASE_URL + "players/" + account_id + "/seasons/" + season_id + "/ranked");
            return builder.ToString();
        }
    }
}
