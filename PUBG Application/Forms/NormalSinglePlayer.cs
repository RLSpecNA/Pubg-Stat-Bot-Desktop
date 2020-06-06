using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Regular_Objects;
using LiveCharts;
using LiveCharts.Wpf;

namespace PUBG_Application.Forms
{
    public partial class NormalSinglePlayer : Form
    {
        private Tuple<RootAccountIDObject, RootNormalStatsObject> pair;

        public NormalSinglePlayer(LeftPlayer leftPlayer, RightPlayer rightPlayer, Values.StatType type)
        {
            InitializeComponent();
            /*
            this.stats = stats;
            

            this.labelGamesPlayedValue.Text = stats.RoundsPlayed.ToString();
            this.labelWinsValue.Text = stats.Wins.ToString();


            this.labelWinPercentValue.Text = string.Format("{0:.##}", StatsCalculation.GetWinRatio(stats.Wins, stats.RoundsPlayed));

            this.labelAverageSurvivedTimeValue.Text = string.Format("{0:.##}", StatsCalculation.GetAverageSurvivedTime(stats.TimeSurvived, stats.RoundsPlayed)).ToString();
            this.labelAdrValue.Text = string.Format("{0:.##}", StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed)).ToString();
            this.labelHeadshotPercentValue.Text = string.Format("{0:.##}", StatsCalculation.GetHeadshotRatio(stats.HeadshotKills, stats.Kills)).ToString();
            this.labelMaxKillsValue.Text = stats.RoundMostKills.ToString();
            this.labelLongestKillValue.Text = string.Format("{0:.##}", stats.LongestKill);



            double fragger = StatsCalculation.GetFraggerRating(StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed),
                StatsCalculation.GetHeadshotRatioBelowOne(stats.HeadshotKills, stats.Kills),
                StatsCalculation.GetAverageSurvivedTimeBase10(stats.TimeSurvived, stats.RoundsPlayed),
                StatsCalculation.GetWinRatioBelowOne(stats.Wins, stats.RoundsPlayed));
            this.labelFraggerRatingValue.Text = string.Format("{0:.##}", fragger);*/
            //this.richTextBox1.Text = (this.stats[Values.Modes.SquadFPP].DamageDealt / this.stats[Values.Modes.SquadFPP].RoundsPlayed).ToString();

            this.graphPlotBindingSource.DataSource = new List<GraphPlot>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Recent 50 Games",
                Labels = new[] {"Jan",  "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"}
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Fragger Rating",
                LabelFormatter = value => value.ToString("C")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }


        public NormalSinglePlayer()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //init data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var years = (from o in graphPlotBindingSource.DataSource as List<GraphPlot>
                         select new { Year = o.Year }).Distinct();

            foreach(var year in years)
            {
                List<double> values = new List<double>();
                for (int month = 1; month <= 12; month++)
                {
                    double value = 0;
                    var data = from o in graphPlotBindingSource.DataSource as List<GraphPlot>
                               where o.Year.Equals(year.Year) && o.Month.Equals(month)
                               orderby o.Month ascending
                               select new { o.Value, o.Month };

                    if (data.SingleOrDefault() != null)
                    {
                        value = data.SingleOrDefault().Value;
                    }

                    values.Add(value);
                }

                series.Add(new LineSeries() { Title = year.Year.ToString(), Values = new ChartValues<double>(values)});
            }

            cartesianChart1.Series = series;
        }
    }
}
