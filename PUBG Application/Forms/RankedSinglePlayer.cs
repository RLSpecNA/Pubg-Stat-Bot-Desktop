using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using JSONLibrary;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects.Match;
using JSONLibrary.Json_Objects.Ranked_Objects;
using LiveCharts;
using LiveCharts.Wpf;
using PUBG_Application.Properties;

namespace PUBG_Application.Forms
{
    public partial class RankedSinglePlayer : Form
    {
        private PanelPlayer player;
        private ModeStatsRanked stats;
        private Values.StatType type;
        public RankedSinglePlayer(PanelPlayer player, Values.StatType type)
        {
           
            InitializeComponent();
            this.player = player;
            this.type = type;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            

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

            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;

            if (this.player != null)
            {
                if (type == Values.StatType.RankedTPP)
                {
                    this.stats = this.player.RankedStatsObj.data.attributes.rankedGameModeStats.squad;
                }
                else if (type == Values.StatType.RankedFPP) 
                {
                    

                    this.stats = this.player.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp;

                }


                this.player.RankedUIStats = UIMethods.ComputeStats(this.stats);
                this.UpdateStatLabels();
            }

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


            int x = (panel3.Size.Width - this.labelRankTitle.Size.Width) / 2;
            this.labelRankTitle.Location = new System.Drawing.Point(x, this.labelRankTitle.Location.Y);


            Func<ChartPoint, string> label = chartpoint => string.Format("{0} ({1:P)", chartpoint.Y, chartpoint.Participation);

            SeriesCollection series = new SeriesCollection();

            series.Add(new PieSeries()
            {
                Title = "Erangel",
                Values = new ChartValues<int> { 20 },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.ForestGreen,

            });

            series.Add(new PieSeries()
            {
                Title = "Miramar",
                Values = new ChartValues<int> { 25 },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.SandyBrown,


            });

            series.Add(new PieSeries()
            {
                Title = "Sanhok",
                Values = new ChartValues<int> { 4 },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.GreenYellow,
            });

            series.Add(new PieSeries()
            {
                Title = "Vikendi",
                Values = new ChartValues<int> { 10 },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.MediumPurple,
            });

            series.Add(new PieSeries()
            {
                Title = "Karakin",
                Values = new ChartValues<int> { 5 },
                DataLabels = true,
                Fill = System.Windows.Media.Brushes.SaddleBrown,
            });

            this.pieChart.Series = series;

        }

        private void UpdateStatLabels()
        {
            RankedObject rankedStats = this.player.RankedUIStats;

            string rankTitle = rankedStats.Title.ToString();
            this.labelRankTitle.Text = rankTitle;
            this.pictureBox1.Image = rankedStats.Image;

            this.labelGamesPlayedValue.Text = rankedStats.GamesPlayed.ToString();
            this.labelWinsValue.Text = rankedStats.Wins.ToString();
            this.labelWinPercentValue.Text = Math.Round(rankedStats.WinPercent, 2).ToString();
            this.labelAVGRankValue.Text = Math.Round(rankedStats.AverageRank, 2).ToString();
            this.labelTopTenRatioValue.Text = Math.Round(rankedStats.TopTenPercent, 2).ToString();
            this.labelAdrValue.Text = rankedStats.Adr.ToString();
            this.labelKDValue.Text = Math.Round(rankedStats.Kd, 2).ToString();
            this.labelKDAValue.Text = Math.Round(rankedStats.Kda, 2).ToString();
            this.labelAverageKnocksPerGameValue.Text = Math.Round(rankedStats.DbnosPerRound, 2).ToString();
            this.fraggerRatingGauge.Value = new Random().Next(0, 100);

            this.labelPlayerNameTop.Text = this.player.Name;
            this.labelSeasonNameLeft.Text = this.player.Season;
            this.labelModeTypeRight.Text = Values.GetEnumString(this.type);

        }

        


        public RankedSinglePlayer()
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

            
      
       


    }
}
