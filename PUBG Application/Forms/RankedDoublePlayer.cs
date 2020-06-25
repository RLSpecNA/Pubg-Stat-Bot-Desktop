using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using JSONLibrary;
using JSONLibrary.Json_Objects.Ranked_Objects;
using LiveCharts;
using LiveCharts.Wpf;
using PUBG_Application.Properties;

namespace PUBG_Application.Forms
{
    public partial class RankedDoublePlayer : Form
    {
        private Form mainform;
        private LeftPlayer leftPlayer;
        private RightPlayer rightPlayer;
        private ModeStatsRanked statsLeft;
        private ModeStatsRanked statsRight;
        public RankedDoublePlayer()
        {
            InitializeComponent();

        }

        public RankedDoublePlayer(LeftPlayer leftPlayer, RightPlayer rightPlayer, Values.StatType type, Form mainform)
        {
            InitializeComponent();
            this.mainform = mainform;

            this.leftPlayer = leftPlayer;
            this.rightPlayer = rightPlayer;


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

            this.pictureBoxLeft.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxRight.SizeMode = PictureBoxSizeMode.Zoom;

            

            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;

            int xLeft = (panelLeft.Size.Width - this.labelPlayerNameLeft.Size.Width) / 2;
            this.labelPlayerNameLeft.Location = new Point(xLeft, this.labelPlayerNameLeft.Location.Y);

            int xRight = (panelRight.Size.Width - this.labelPlayerNameRight.Size.Width) / 2;
            this.labelPlayerNameRight.Location = new Point(xRight, this.labelPlayerNameRight.Location.Y);



            if (this.leftPlayer != null && this.rightPlayer != null)
            {
                if (type == Values.StatType.RankedFPP)
                {
                    this.statsLeft = this.leftPlayer.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp;
                    this.statsRight = this.rightPlayer.RankedStatsObj.data.attributes.rankedGameModeStats.squadFpp;

                    
                }
                else if (type == Values.StatType.RankedTPP)
                {
                    this.statsLeft = this.leftPlayer.RankedStatsObj.data.attributes.rankedGameModeStats.squad;
                    this.statsRight = this.rightPlayer.RankedStatsObj.data.attributes.rankedGameModeStats.squad;
                }


                this.leftPlayer.RankedUIStats = UIMethods.ComputeStats(this.statsLeft);
                this.rightPlayer.RankedUIStats = UIMethods.ComputeStats(this.statsRight);
                this.UpdateStatLabels();
                this.DisplayComparisonArrows();

            }




        }

        
       
        private void UpdateStatLabels()
        {
            RankedObject leftUI = this.leftPlayer.RankedUIStats;
            RankedObject rightUI = this.rightPlayer.RankedUIStats;

            this.pictureBoxLeft.Image = leftUI.Image;
            this.labelPlayerNameLeft.Text = this.leftPlayer.Name;
            this.labelGamesPlayedLeftValue.Text = leftUI.GamesPlayed.ToString();
            this.labelWinsLeftValue.Text = leftUI.Wins.ToString();
            this.labelWinPercentLeftValue.Text = leftUI.WinPercent.ToString();
            this.labelAVGRankLeftValue.Text = leftUI.AverageRank.ToString();
            this.labelTopTenRatioLeftValue.Text = leftUI.TopTenPercent.ToString();
            this.labelAdrLeftValue.Text = leftUI.Adr.ToString();
            this.labelKDLeftValue.Text = leftUI.Kd.ToString();
            this.labelKDALeftValue.Text = leftUI.Kda.ToString();
            this.labelAverageKnocksPerGameLeftValue.Text = leftUI.DbnosPerRound.ToString();

            
            this.pictureBoxRight.Image = rightUI.Image;
            this.labelPlayerNameRight.Text = this.rightPlayer.Name;
            this.labelGamesPlayedRightValue.Text = rightUI.GamesPlayed.ToString();
            this.labelWinsRightValue.Text = rightUI.Wins.ToString();
            this.labelWinPercentRightValue.Text = rightUI.WinPercent.ToString();
            this.labelAVGRankRightValue.Text = rightUI.AverageRank.ToString();
            this.labelTopTenRatioRightValue.Text = rightUI.TopTenPercent.ToString();
            this.labelAdrRightValue.Text = rightUI.Adr.ToString();
            this.labelKDRightValue.Text = rightUI.Kd.ToString();
            this.labelKDARightValue.Text = rightUI.Kda.ToString();
            this.labelAverageKnocksPerGameRightValue.Text = rightUI.DbnosPerRound.ToString();


        }
        
        private void DisplayComparisonArrows()
        {
            RankedObject leftUI = this.leftPlayer.RankedUIStats;
            RankedObject rightUI = this.rightPlayer.RankedUIStats;

            List<double> statsListLeft = new List<double>();
            List<double> statsListRight = new List<double>();
            List<IconPictureBox> leftComparisonArrowList = new List<IconPictureBox>();
            List<IconPictureBox> rightComparisonArrowList = new List<IconPictureBox>();

            
            statsListLeft.Add(leftUI.GamesPlayed);
            statsListLeft.Add(leftUI.Wins);
            statsListLeft.Add(leftUI.WinPercent);
            statsListLeft.Add(leftUI.AverageRank);
            statsListLeft.Add(leftUI.TopTenPercent);
            statsListLeft.Add(leftUI.Adr);
            statsListLeft.Add(leftUI.Kd);
            statsListLeft.Add(leftUI.Kda);
            statsListLeft.Add(leftUI.DbnosPerRound);

            statsListRight.Add(rightUI.GamesPlayed);
            statsListRight.Add(rightUI.Wins);
            statsListRight.Add(rightUI.WinPercent);
            statsListRight.Add(rightUI.AverageRank);
            statsListRight.Add(rightUI.TopTenPercent);
            statsListRight.Add(rightUI.Adr);
            statsListRight.Add(rightUI.Kd);
            statsListRight.Add(rightUI.Kda);
            statsListRight.Add(rightUI.DbnosPerRound);

            leftComparisonArrowList.Add(this.iconPictureBoxGamesPlayedLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinsLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxWinPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAVGRankLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxTopTenPercentLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxAdrLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxKdLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxKdaLeft);
            leftComparisonArrowList.Add(this.iconPictureBoxdBNOsLeft);

            rightComparisonArrowList.Add(this.iconPictureBoxGamesPlayedRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinsRight);
            rightComparisonArrowList.Add(this.iconPictureBoxWinPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAVGRankRight);
            rightComparisonArrowList.Add(this.iconPictureBoxTopTenPercentRight);
            rightComparisonArrowList.Add(this.iconPictureBoxAdrRight);
            rightComparisonArrowList.Add(this.iconPictureBoxKdRight);
            rightComparisonArrowList.Add(this.iconPictureBoxKdaRight);
            rightComparisonArrowList.Add(this.iconPictureBoxdBNOsRight);


            for (int i = 0; i < 9; i++)
            {

                if (i == 3)
                {

                    if (statsListLeft[i] < statsListRight[i])
                    {
                        leftComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretUp;
                        rightComparisonArrowList[i].IconChar = FontAwesome.Sharp.IconChar.CaretDown;

                        leftComparisonArrowList[i].IconColor = MyColors.RGBColors.green;
                        rightComparisonArrowList[i].IconColor = MyColors.RGBColors.redBrown;
                    }
                    else if (statsListLeft[i] > statsListRight[i])
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
                else if (statsListLeft[i] > statsListRight[i])
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

        private void switchToSingleViewButton_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = (MainWindow) this.mainform;
            mainWindow.rightTextBox.Text = "";
            mainWindow.SetRightPlayerNull();
            mainWindow.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));
        }

        private void iconPictureBox18_Click(object sender, EventArgs e)
        {

        }
    }
}
