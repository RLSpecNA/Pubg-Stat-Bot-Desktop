using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public static class StatsCalculation
    {
        public static double GetFraggerRating(double adr, double headshotratio, double survivedtime, double winratepercent)
        {
            double numerator = (adr * (1 + headshotratio));
            double denominator = 1 - winratepercent;
            denominator = denominator * survivedtime;

            return numerator / denominator;
        }

        public static double GetAdr(float damagedealt, float  roundsplayed)
        { 
            return damagedealt / roundsplayed;
        }
        
        public static double GetHeadshotRatio(float headshotkills, float totalkills)
        {
            return (headshotkills / totalkills) * 100;
        }

        public static double GetHeadshotRatioBelowOne(float headshotkills, float totalkills)
        {
            return (headshotkills / totalkills);
        }

        public static double GetAverageSurvivedTime(float timesurvived, float roundsplayed)
        {
            double minsSurvived = timesurvived / 60;
            double averageMinsSurvived = minsSurvived / roundsplayed;

            double fraction = averageMinsSurvived - Math.Truncate(averageMinsSurvived);
            fraction = fraction * 60;
            fraction = fraction / 100;

            double wholeNumber = Math.Truncate(averageMinsSurvived);

            return fraction + wholeNumber;
        }

        public static double GetAverageSurvivedTimeBase10(float timesurvived, float roundsplayed)
        {
            double minsSurvived = timesurvived / 60;
            double averageMinsSurvived = minsSurvived / roundsplayed;


            return averageMinsSurvived;
        }

        public static double GetWinRatio(float wins, float roundsplayed)
        {
            return (wins / roundsplayed) * 100;
        }

        public static double GetWinRatioBelowOne(float wins, float roundsplayed)
        {
            return (wins / roundsplayed);
        }
    }
}
