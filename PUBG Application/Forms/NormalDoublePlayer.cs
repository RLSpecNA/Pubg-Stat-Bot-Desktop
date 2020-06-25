using FontAwesome.Sharp;
using JSONLibrary;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace PUBG_Application.Forms
{
    public partial class NormalDoublePlayer : Form
    {

        private PanelPlayer leftPlayer;
        private PanelPlayer rightPlayer;
        private ModeStats statsLeft;
        private ModeStats statsRight;

        private Form mainForm;

        private Values.StatType type;
        public NormalDoublePlayer()
        {
            InitializeComponent();

        }

        public NormalDoublePlayer(LeftPlayer leftPlayer, RightPlayer rightPlayer, Values.StatType type, Form form)
        {
            InitializeComponent();

            this.graphPlotBindingSource.DataSource = new List<GraphPlot>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Recent 50 Games",
                Labels = new[] { "Jan", "Feb", "March", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" }
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Fragger Rating",
                LabelFormatter = value => value.ToString("C")
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;


            this.leftPlayerFraggerRatingGauge.From = 0;
            this.leftPlayerFraggerRatingGauge.To = 100;

            this.leftPlayerFraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            this.leftPlayerFraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            this.rightPlayerFraggerRatingGauge.From = 0;
            this.rightPlayerFraggerRatingGauge.To = 100;

            this.rightPlayerFraggerRatingGauge.Base.LabelsVisibility = Visibility.Hidden;
            this.rightPlayerFraggerRatingGauge.Base.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 169, 0));

            this.leftPlayerFraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };

            this.rightPlayerFraggerRatingGauge.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Yellow, 0),
                    new GradientStop(Colors.Orange, .5),
                    new GradientStop(Colors.Red, 1)
                }
            };


            if (form != null)
            {
                this.mainForm = form;
            }
            if (leftPlayer != null && rightPlayer != null)
            {
               
                this.leftPlayer = leftPlayer;
                this.rightPlayer = rightPlayer;
                this.type = type;
                
                this.statsLeft = UIMethods.GetProperNormalStatsObject(type, this.leftPlayer);
                this.statsRight = UIMethods.GetProperNormalStatsObject(type, this.rightPlayer);

                this.leftPlayer.UnRankedUIStats = this.ComputeStats(this.statsLeft);
                this.rightPlayer.UnRankedUIStats = this.ComputeStats(this.statsRight);

                this.UpdateStatLabels();
                this.DisplayComparisonArrows();


            }
        }

        private void UpdateStatLabels()
        {
            UnRankedObject unrankedLeft = this.leftPlayer.UnRankedUIStats;
            UnRankedObject unrankedRight = this.rightPlayer.UnRankedUIStats;



            this.labelGamesPlayedLeftValue.Text = unrankedLeft.GamesPlayed.ToString();
            this.labelWinsLeftValue.Text = unrankedLeft.Wins.ToString();
            this.labelWinPercentLeftValue.Text = unrankedLeft.WinPercent.ToString();
            this.labelAverageSurvivedTimeLeftValue.Text = unrankedLeft.AvgSurvivalTime.ToString();
            this.labelAdrLeftValue.Text = unrankedLeft.Adr.ToString();
            this.labelHeadshotPercentLeftValue.Text = unrankedLeft.HeadshotRatio.ToString();
            this.labelMaxKillsLeftValue.Text = unrankedLeft.MaxKills.ToString();
            this.labelLongestKillLeftValue.Text = unrankedLeft.LongestKill.ToString();
            this.labelDbnosPerRoundLeftValue.Text = unrankedLeft.DbnosPerRound.ToString();
            this.leftPlayerFraggerRatingGauge.Value = unrankedLeft.FraggerRating;




            this.labelGamesPlayedRightValue.Text = unrankedRight.GamesPlayed.ToString();
            this.labelWinsRightValue.Text = unrankedRight.Wins.ToString();
            this.labelWinPercentRightValue.Text = unrankedRight.WinPercent.ToString();
            this.labelAverageSurvivedTimeRightValue.Text = unrankedRight.AvgSurvivalTime.ToString();
            this.labelAdrRightValue.Text = unrankedRight.Adr.ToString();
            this.labelHeadshotPercentRightValue.Text = unrankedRight.HeadshotRatio.ToString();
            this.labelMaxKillsRightValue.Text = unrankedRight.MaxKills.ToString();
            this.labelLongestKillRightValue.Text = unrankedRight.LongestKill.ToString();
            this.labelDbnosPerRoundRightValue.Text = unrankedRight.DbnosPerRound.ToString();
            this.rightPlayerFraggerRatingGauge.Value = unrankedRight.FraggerRating;


            this.labelLeftPlayerName.Text = this.leftPlayer.Name;
            this.labelRightPlayerName.Text = this.rightPlayer.Name;

            this.labelLeftPlayerSeasonName.Text = this.leftPlayer.Season;
            this.labelRightPlayerSeasonName.Text = this.rightPlayer.Season;



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

        private void DisplayComparisonArrows()
        {
            UnRankedObject leftUI = this.leftPlayer.UnRankedUIStats;
            UnRankedObject rightUI = this.rightPlayer.UnRankedUIStats;

            List<double> statsListLeft = new List<double>();
            List<double> statsListRight = new List<double>();
            List<IconPictureBox> leftComparisonArrowList = new List<IconPictureBox>();
            List<IconPictureBox> rightComparisonArrowList = new List<IconPictureBox>();


            statsListLeft.Add(leftUI.GamesPlayed);
            statsListLeft.Add(leftUI.Wins);
            statsListLeft.Add(leftUI.WinPercent);
            statsListLeft.Add(leftUI.AvgSurvivalTime);
            statsListLeft.Add(leftUI.HeadshotRatio);
            statsListLeft.Add(leftUI.Adr);
            statsListLeft.Add(leftUI.MaxKills);
            statsListLeft.Add(leftUI.LongestKill);
            statsListLeft.Add(leftUI.DbnosPerRound);

            statsListRight.Add(rightUI.GamesPlayed);
            statsListRight.Add(rightUI.Wins);
            statsListRight.Add(rightUI.WinPercent);
            statsListRight.Add(rightUI.AvgSurvivalTime);
            statsListRight.Add(rightUI.HeadshotRatio);
            statsListRight.Add(rightUI.Adr);
            statsListRight.Add(rightUI.MaxKills);
            statsListRight.Add(rightUI.LongestKill);
            statsListRight.Add(rightUI.DbnosPerRound);

            leftComparisonArrowList.Add(this.iconPictureBoxGamesPlayedLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAvgSurvivalTimeLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxHeadshotPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAdrLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxMaxKillsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxLongestKillLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxDbnosPerRoundLeft);

            rightComparisonArrowList.Add(this.iconPictureBoxGamesPlayedRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAvgSurvivalTimeRight);
            rightComparisonArrowList.Add(this.iconPictureBoxHeadShotPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAdrRight);
            rightComparisonArrowList.Add(this.iconPictureBoxMaxKillsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxLongestKillRight);
            rightComparisonArrowList.Add(this.iconPictureBoxDbnosPerRoundRight);


            for (int i = 0; i < 9; i++)
            {

               
                if (statsListLeft[i] > statsListRight[i])
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                }
                else if (statsListLeft[i] < statsListRight[i])
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                }
                else
                {
                    leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretRight;
                    rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretLeft;

                    leftComparisonArrowList[i].IconColor = MyColors.RGBColors.lightBlue;
                    rightComparisonArrowList[i].IconColor = MyColors.RGBColors.lightBlue;
                }
            }




        }


        private void button1_Click(object sender, EventArgs e)
        {
            //init data
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var years = (from o in graphPlotBindingSource.DataSource as List<GraphPlot>
                         select new { Year = o.Year }).Distinct();

            foreach (var year in years)
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

                series.Add(new LineSeries() { Title = year.Year.ToString(), Values = new ChartValues<double>(values) });
            }

            cartesianChart1.Series = series;
        }

        

        private void panelLeftPlayerStats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer);
        }

        private void panelRightPlayerStats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer);
        }

        private void labelLeftPlayerName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer);
        }

        private void labelLeftPlayerSeasonName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer);
        }

        private void labelLeftPlayerGameMode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.leftPlayer);
        }

        private void labelRightPlayerName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer);
        }

        private void labelRightPlayerSeasonName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer);
        }

        private void labelRightPlayerGameMode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switchToSingleView(sender, e, this.rightPlayer);
        }


        private void switchToSingleView(object sender, MouseEventArgs e, PanelPlayer player)
        {
            if (this.mainForm != null)
            {
                MainWindow mainWindow = (MainWindow)this.mainForm;
                mainWindow.rightTextBox.Text = "";
                mainWindow.leftTextBox.Text = "";

                mainWindow.SetRightPlayerNull();
                mainWindow.OpenChildForm(new NormalSinglePlayer(player, type));
            }
            
        }
    }
}
