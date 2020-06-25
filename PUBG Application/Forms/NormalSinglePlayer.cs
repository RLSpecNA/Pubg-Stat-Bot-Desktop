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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Media;
using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace PUBG_Application.Forms
{
    public partial class NormalSinglePlayer : Form
    {
        private Tuple<RootAccountIDObject, RootNormalStatsObject> pair;
        private PanelPlayer player;
        private ModeStats stats;
        private Values.StatType type;

        public NormalSinglePlayer(PanelPlayer player, Values.StatType type)
        {
            InitializeComponent();
            this.player = player;
            this.type = type;
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

            Dictionary<string, int> mapsCount = this.CalculateRecent20Maps();

            Func<ChartPoint, string> label = chartpoint => string.Format("{0} ({1:P)", chartpoint.Y, chartpoint.Participation);

            SeriesCollection series = new SeriesCollection();


            foreach(KeyValuePair<string, int> pair in mapsCount)
            {
                System.Windows.Media.SolidColorBrush Fill = System.Windows.Media.Brushes.SandyBrown;

                if (pair.Key == "Desert_Main")
                {
                    Fill = System.Windows.Media.Brushes.SandyBrown;
                }
                else if (pair.Key == "DihorOtok_Main")
                {
                    Fill = System.Windows.Media.Brushes.MediumPurple;
                }
                
                else if (pair.Key == "Baltic_Main")
                {
                    Fill = System.Windows.Media.Brushes.ForestGreen;
                }
                
                else if (pair.Key == "Savage_Main")
                {
                    Fill = System.Windows.Media.Brushes.GreenYellow;
                }
                else if (pair.Key == "Summerland_Main")
                {
                    Fill = System.Windows.Media.Brushes.SaddleBrown;
                }

                if (pair.Value != 0)
                {
                    series.Add(new PieSeries()
                    {
                        Title = Values.GetMapName(pair.Key),
                        Values = new ChartValues<int> { pair.Value },
                        DataLabels = true,
                        Fill = Fill
                    });
                }
                
            }

            

            

            this.pieChart.Series = series;

            fraggerRatingGauge.From = 0;
            fraggerRatingGauge.To = 100;
            
            fraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            fraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            fraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };

            int xRecent20 = (panelRecent20.Size.Width - this.labelPieChartTitle.Size.Width) / 2;
            this.labelPieChartTitle.Location = new System.Drawing.Point(xRecent20, this.labelPieChartTitle.Location.Y);

            int xFraggerRating = (panelFraggerRating.Size.Width - this.labelFraggerRating.Size.Width) / 2;
            this.labelFraggerRating.Location = new System.Drawing.Point(xFraggerRating, this.labelFraggerRating.Location.Y);


            if (this.player != null)
            {

                this.stats = UIMethods.GetProperNormalStatsObject(type, this.player);

                this.player.UnRankedUIStats = this.ComputeStats(this.stats);
                
                this.DisplayStats();
                this.labelPlayerName.Text = this.player.Name.ToString();
                this.labelSeasonName.Text = this.player.Season;
                this.labelModeType.Text = type.ToString(); ;


            }

        }

        
        private Dictionary<string, int> CalculateRecent20Maps()
        {
            Dictionary<string, int> mapsCount = new Dictionary<string, int>();
            mapsCount.Add("Desert_Main", 0);
            mapsCount.Add("DihorOtok_Main", 0);
            mapsCount.Add("Erangel_Main", 0);
            mapsCount.Add("Baltic_Main", 0);
            mapsCount.Add("Range_Main", 0);
            mapsCount.Add("Savage_Main", 0);
            mapsCount.Add("Summerland_Main", 0);

            for (int i = 0; i < this.player.Matches20SquadFpp.Count; i++)
            {
                string mapname = this.player.Matches20SquadFpp[i].data.attributes.mapName;

                if (mapname == "Desert_Main")
                {
                    mapsCount["Desert_Main"] += 1;
                }
                else if (mapname == "DihorOtok_Main")
                {
                    mapsCount["DihorOtok_Main"] += 1;
                }
                else if (mapname == "Erangel_Main")
                {
                    mapsCount["Erangel_Main"] += 1;
                }
                else if (mapname == "Baltic_Main")
                {
                    mapsCount["Baltic_Main"] += 1;
                }
                else if (mapname == "Range_Main")
                {
                    mapsCount["Range_Main"] += 1;
                }
                else if (mapname == "Savage_Main")
                {
                    mapsCount["Savage_Main"] += 1;
                }
                else if (mapname == "Summerland_Main")
                {
                    mapsCount["Summerland_Main"] += 1;
                }
            }

            return mapsCount;
        }

        private UnRankedObject ComputeStats(ModeStats stats)
        {
            if (stats != null)
            {
                return new UnRankedObject()
                {
                    GamesPlayed = (int)stats.RoundsPlayed,
                    Wins = (int)stats.Wins,
                    WinPercent = Math.Round(StatsCalculation.GetWinRatio(stats.Wins, stats.RoundsPlayed), 2),
                    AvgSurvivalTime = Math.Round(StatsCalculation.GetAverageSurvivedTime(stats.TimeSurvived, stats.RoundsPlayed), 2),
                    Adr = (int)StatsCalculation.GetAdr(stats.DamageDealt, stats.RoundsPlayed),
                    HeadshotRatio = Math.Round(StatsCalculation.GetHeadshotRatio(stats.HeadshotKills, stats.Kills), 2),
                    MaxKills = (int)stats.RoundMostKills,
                    LongestKill = Math.Round(stats.LongestKill, 2),
                    DbnosPerRound = Math.Round(StatsCalculation.GetKnocksPerRound(stats.DBNOS, stats.RoundsPlayed), 2),

                    FraggerRating = new Random().Next(30, 100)
                };
            }
            else
            {
                return new UnRankedObject()
                {
                    GamesPlayed = 0,
                    Wins = 0,
                    WinPercent = 0,
                    AvgSurvivalTime = 0,
                    Adr = 0,
                    HeadshotRatio = 0,
                    MaxKills = 0,
                    LongestKill = 0,
                    DbnosPerRound = 0,
                    FraggerRating = 0

                };
            }
        }

        private void DisplayStats()
        {
            UnRankedObject unranked = this.player.UnRankedUIStats;


            this.labelGamesPlayedValue.Text = unranked.GamesPlayed.ToString();
            this.labelWinsValue.Text = unranked.Wins.ToString();
            this.labelWinPercentValue.Text = unranked.WinPercent.ToString();
            this.labelAverageSurvivedTimeValue.Text = unranked.AvgSurvivalTime.ToString();
            this.labelAdrValue.Text = unranked.Adr.ToString();
            this.labelHeadshotPercentValue.Text = unranked.HeadshotRatio.ToString();
            this.labelMaxKillsValue.Text = unranked.MaxKills.ToString();
            this.labelLongestKillValue.Text = unranked.LongestKill.ToString();
            this.labelDbnosPerRoundValue.Text = unranked.DbnosPerRound.ToString();
            this.fraggerRatingGauge.Value = unranked.FraggerRating;

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
