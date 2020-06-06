using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONLibrary
{
    public abstract class PanelPlayer
    {
        public string Name { get; set; }

        public string AccountID { get; set; }

        public RootAccountIDObject AccountObj { get; set; }

        public RootNormalStatsObject NormalStatsObj { get; set; }

        public RootRankedStatsObject RankedStatsObj { get; set; }


        protected PanelPlayer(string name, string account_id, RootAccountIDObject accountObj, 
            RootNormalStatsObject normalStatsObj, RootRankedStatsObject rankedStatsObj)
        {
            this.Name = name;
            this.AccountID = account_id;
            this.AccountObj = accountObj;
            this.NormalStatsObj = normalStatsObj;
            this.RankedStatsObj = rankedStatsObj;
        }

        protected PanelPlayer(RootAccountIDObject accountObj)
        {
            this.AccountObj = accountObj;

            this.Name = this.AccountObj.data[0].attributes.name.ToString();
            this.AccountID = this.AccountObj.data[0].id.ToString();
        }


    }
}
