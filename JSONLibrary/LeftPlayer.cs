﻿using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class LeftPlayer : PanelPlayer
    {
        public LeftPlayer(string name, string account_id, RootAccountIDObject accountObj,
            RootNormalStatsObject normalStatsObj, RootRankedStatsObject rankedStatsObj) : base(name,  account_id,  accountObj,
             normalStatsObj,  rankedStatsObj)
        {

        }
    }
}
