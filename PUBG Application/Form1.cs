using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using PUBG_Application.Forms;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Threading;
using JSONLibrary;
using PUBG_Application;
using System.IO;
using System.Collections.Specialized;
using Newtonsoft.Json;
using JSONLibrary.Json_Objects.Ranked_Objects;
using JSONLibrary.Json_Objects.Regular_Objects;
using JSONLibrary.Json_Objects.AccountID;
using JSONLibrary.Json_Objects;
using System.Runtime.CompilerServices;
using JSONLibrary.Json_Objects.Match;

namespace PUBG_Application
{
    public partial class MainWindow : Form
    {
        // Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private Dictionary<Values.Modes, ModeStats> stats = new Dictionary<Values.Modes, ModeStats>();

        private bool firstLoad = true;

        private Dictionary<string, RootAccountIDObject> nameAndIdDict = new Dictionary<string, RootAccountIDObject>();

        private LeftPlayer leftPlayer = null;
        private RightPlayer rightPlayer = null;
        private Dictionary<string, PanelPlayer> familiarNames = new Dictionary<string, PanelPlayer>();

        public TextBox leftTextBox;
        public TextBox rightTextBox;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;


            this.PopulateComboBox();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            this.circularProgressBarLarge.Value = 0;
            this.circularProgressBarLarge.Update();

            this.leftTextBox = this.txtLeftPlayerName;
            this.rightTextBox = this.txtRightPlayerName;

            //this.circularProgressBarTop.Value = 0;
            //this.circularProgressBarTop.Update();

            //this.txtRightPlayerName.Enabled = false;

        }

        public void SetLeftPlayerNull()
        {
            this.leftPlayer = null;
        }

        public void SetRightPlayerNull()
        {
            this.rightPlayer = null;
        }
        private struct RGBColors
        {
            // orange color
            public static Color orange = Color.FromArgb(242, 169, 0);

            // white color
            public static Color white = Color.FromArgb(255, 255, 255);

            // dark grey color
            public static Color darkgrey = Color.FromArgb(22, 22, 22);

            // light grey color
            public static Color lightgrey = Color.FromArgb(60, 60, 60);

            //
            public static Color lightorange = Color.FromArgb(255, 218, 74);

            public static Color panelTitleGrey = Color.FromArgb(27, 27, 27);

            public static Color redBrown = Color.FromArgb(156, 65, 0);

        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                this.DisableButton();

                currentBtn = (IconButton)senderBtn;

                //grey color
                currentBtn.BackColor = RGBColors.lightgrey;

                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Icon Current Child Form
                //iconCurrentChildForm.IconChar = currentBtn.IconChar;
                //iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                // dark grey color
                currentBtn.BackColor = RGBColors.darkgrey;

                // orange color
                currentBtn.ForeColor = RGBColors.orange;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;

                // orange color
                currentBtn.IconColor = RGBColors.orange;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open only form
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //label1.Text = childForm.Text;
        }


        private void soloButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Solo, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.Solo));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.Solo));
                }
            }

        }

        private void duoButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Duo, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.Duo));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.Duo));
                }
            }

        }

        private void squadButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Squad, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.Squad));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.Squad));
                }
            }
        }

        private void soloFPPButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.SoloFPP, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.SoloFPP));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.SoloFPP));
                }
            }

        }

        private void duoFPPButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.DuoFPP, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.DuoFPP));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.DuoFPP));
                }
            }

        }

        private void squadFPPButton_clicked(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new NormalDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.SquadFPP, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, Values.StatType.SquadFPP));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new NormalSinglePlayer(this.rightPlayer, Values.StatType.SquadFPP));
                }
            }


        }

        private void rankTPPButton_Click(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new RankedDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedTPP, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedTPP));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedTPP));
                }
                
            }

        }

        private void rankFPPButton_Click(object sender, EventArgs e)
        {
            if (this.BothPlayersNotNull())
            {
                this.ActivateButton(sender, RGBColors.white);
                this.OpenChildForm(new RankedDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedFPP, this));
            }
            else
            {
                if (this.leftPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));
                }
                else if (this.rightPlayer != null)
                {
                    this.ActivateButton(sender, RGBColors.white);
                    this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));
                }
            }
        }

        private void Reset()
        {
            DisableButton();
            //iconCurrentChildForm.IconChar = IconChar.Home;
            // iconCurrentChildForm.IconColor = RGBColors.lightorange;

            //label1.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void panelTitleBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void LabelEffect1_Clicked(object sender, EventArgs e)
        {
            var lbl = sender as Label;

            if (lbl.Location.X == 16)
            {
                lbl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                lbl.Cursor = Cursors.Arrow;
                lbl.Location = new Point(lbl.Location.X - 3, lbl.Location.Y - 25);

                foreach (Control txt in panelTitleBar.Controls)
                {
                    if (txt.GetType() == typeof(TextBox) && txt.Name == "txtLeftPlayerName")
                    {
                        txt.Focus();
                    }
                    if (txt.GetType() == typeof(Panel) && txt.Name == "pnlPlayerName1")
                    {
                        txt.BackColor = RGBColors.orange;
                    }
                }
            }
        }

        private void LabelEffect2_Clicked(object sender, EventArgs e)
        {
            var lbl = sender as Label;


            if (lbl.Location.X == 403)
            {
                lbl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                lbl.Cursor = Cursors.Arrow;
                lbl.Location = new Point(lbl.Location.X - 3, lbl.Location.Y - 25);

                foreach (Control txt in panelTitleBar.Controls)
                {
                    if (txt.GetType() == typeof(TextBox) && txt.Name == "txtRightPlayerName")
                    {
                        txt.Focus();
                    }
                    if (txt.GetType() == typeof(Panel) && txt.Name == "pnlPlayerName2")
                    {
                        txt.BackColor = RGBColors.orange;
                    }
                }
            }
        }


        private void TextBox1_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            foreach (Control ctrl in panelTitleBar.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnlPlayerName1")
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lblPlayerName1")
                {
                    ctrl.ForeColor = RGBColors.orange;
                    ctrl.BackColor = RGBColors.panelTitleGrey;

                    if (ctrl.Location.X != 13)
                    {
                        ctrl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                        ctrl.Cursor = Cursors.Arrow;
                        ctrl.Location = new Point(ctrl.Location.X - 3, ctrl.Location.Y - 25);
                    }
                }
            }
        }

        private void TextBox2_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            foreach (Control ctrl in panelTitleBar.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnlPlayerName2")
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lblPlayerName2")
                {
                    ctrl.ForeColor = RGBColors.orange;
                    ctrl.BackColor = RGBColors.panelTitleGrey;

                    if (ctrl.Location.X != 400)
                    {
                        ctrl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                        ctrl.Cursor = Cursors.Arrow;
                        ctrl.Location = new Point(ctrl.Location.X - 3, ctrl.Location.Y - 25);
                    }
                }
            }
        }




        private void TextBox1_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            foreach (Control ctrl in panelTitleBar.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnlPlayerName1")
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lblPlayerName1")
                {
                    ctrl.ForeColor = RGBColors.orange;

                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        txt.Clear();
                        ctrl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                        ctrl.Cursor = Cursors.IBeam;
                        ctrl.Location = new Point(ctrl.Location.X + 3, ctrl.Location.Y + 25);

                    }
                }
            }
        }

        private void TextBox2_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            foreach (Control ctrl in panelTitleBar.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnlPlayerName2")
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lblPlayerName2")
                {
                    ctrl.ForeColor = RGBColors.orange;

                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        txt.Clear();
                        ctrl.Font = new Font("Century Gothic", 12, FontStyle.Italic);
                        ctrl.Cursor = Cursors.IBeam;
                        ctrl.Location = new Point(ctrl.Location.X + 3, ctrl.Location.Y + 25);

                    }
                }
            }
        }

        private void txtPlayerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadProgress(CircularProgressBar.CircularProgressBar bar)
        {
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(1);
                bar.Value = i;
                bar.Update();
            }
        }

        
        private async void txtPlayerName1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                

                string leftname = txtLeftPlayerName.Text.ToString();
                string rightname = txtRightPlayerName.Text.ToString();

                string season1 = this.comboBox1.SelectedItem.ToString();
                string season2 = this.comboBox2.SelectedItem.ToString();


                APIResponse response;

                if ((leftname != string.Empty || leftname != "") && (rightname != string.Empty || rightname != ""))
                {
                    this.txtLeftPlayerName.Enabled = false;
                    this.txtRightPlayerName.Enabled = false;

                    //TODO
                    // make same names in 1 if/else so a double form can open and not single when 2 names are already in the dictionary
                    if (this.familiarNames.ContainsKey(leftname))
                    {
                        PanelPlayer player = this.familiarNames[leftname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {

                            LeftPlayer leftPlayer = (LeftPlayer)player;
                            leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));
                            
                            this.leftPlayer = leftPlayer;
                            this.leftPlayer.Season = season1;
                        }
                    }
                    else
                    {
                        // fetch account informaiton from api
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(leftname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp()); this.leftPlayer = null;
                            }
                            else
                            {
                                LeftPlayer leftPlayer = (LeftPlayer)player;
                                leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));
                                
                                this.leftPlayer = leftPlayer;
                                this.leftPlayer.Season = season1;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, leftPlayer);



                                


                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }

                       
                    }

                    if (this.familiarNames.ContainsKey(rightname))
                    {
                        PanelPlayer player = this.familiarNames[rightname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            RightPlayer rightPlayer = (RightPlayer)player;
                            rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                            this.rightPlayer = rightPlayer;
                            this.rightPlayer.Season = season2;



                        }
                    }
                    else
                    {
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(rightname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.rightPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                RightPlayer rightPlayer = (RightPlayer)player;
                                rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                                this.rightPlayer = rightPlayer;
                                this.rightPlayer.Season = season2;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, rightPlayer);



                                
                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                    }

                    this.ActivateButton(this.iconButton8, RGBColors.white);
                    if (this.leftPlayer != null && this.rightPlayer != null)
                    {
                        this.OpenChildForm(new RankedDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedFPP, this));

                    }
                    else if (this.leftPlayer == null && this.rightPlayer != null)
                    {
                        this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));

                    }

                    else if (this.leftPlayer != null && this.rightPlayer == null)
                    {
                        this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));

                    }
                    else
                    {
                        MessageBox.Show("Both account names are not valid.");
                    }

                    this.txtLeftPlayerName.Enabled = true;
                    this.txtRightPlayerName.Enabled = true;

                }

                else if (leftname != "" || leftname != string.Empty)
                {
                    this.txtLeftPlayerName.Enabled = false;
                    if (this.familiarNames.ContainsKey(leftname))
                    {
                        PanelPlayer player = this.familiarNames[leftname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            LeftPlayer leftPlayer = (LeftPlayer)player;
                            leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));
                            
                            this.leftPlayer = leftPlayer;
                            this.leftPlayer.Season = season1;


                            this.ActivateButton(this.iconButton8, RGBColors.white);
                            this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));


                        }
                    }
                    else
                    {
                        // fetch account informaiton from api
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(leftname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.leftPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                LeftPlayer leftPlayer = (LeftPlayer)player;
                                leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));
                                leftPlayer.Matches20SquadFpp = await Task.Run(() => UIMethods.FilterUnrankedMatches(leftPlayer.Matches, Values.StatType.SquadFPP, 20));
                                this.leftPlayer = leftPlayer;
                                this.leftPlayer.Season = season1;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, leftPlayer);



                                this.ActivateButton(this.iconButton8, RGBColors.white);
                                this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));


                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                        
                    }

                    
                    this.txtLeftPlayerName.Enabled = true;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                else if (rightname != string.Empty || rightname != "")
                {

                    this.txtRightPlayerName.Enabled = false;

                    if (this.familiarNames.ContainsKey(rightname))
                    {
                        PanelPlayer player = this.familiarNames[rightname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            RightPlayer rightPlayer = (RightPlayer)player;
                            rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                            this.rightPlayer = rightPlayer;
                            this.rightPlayer.Season = season2;

                            this.ActivateButton(this.iconButton8, RGBColors.white);
                            this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));


                        }
                    }
                    else
                    {
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(rightname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.rightPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                RightPlayer rightPlayer = (RightPlayer)player;
                                rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                                this.rightPlayer = rightPlayer;
                                this.rightPlayer.Season = season2;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, rightPlayer);



                                this.ActivateButton(this.iconButton8, RGBColors.white);
                                this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));
                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                    }

                    this.txtRightPlayerName.Enabled = true;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                e.Handled = true;
                e.SuppressKeyPress = true;

                return;
            }
        }

        
       


        private async void txtPlayerName2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                

                string leftname = txtLeftPlayerName.Text.ToString();
                string rightname = txtRightPlayerName.Text.ToString();

                string season1 = this.comboBox1.SelectedItem.ToString();
                string season2 = this.comboBox2.SelectedItem.ToString();


                APIResponse response;

                if ((leftname != string.Empty || leftname != "") && (rightname != string.Empty || rightname != ""))
                {

                    this.txtLeftPlayerName.Enabled = false;
                    this.txtRightPlayerName.Enabled = false;

                    if (this.familiarNames.ContainsKey(leftname))
                    {
                        PanelPlayer player = this.familiarNames[leftname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            LeftPlayer leftPlayer = (LeftPlayer)player;
                            leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));

                            this.leftPlayer = leftPlayer;
                            this.leftPlayer.Season = season1;

                        }
                    }
                    else
                    {
                        // fetch account informaiton from api
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(leftname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.leftPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                LeftPlayer leftPlayer = (LeftPlayer)player;
                                leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));

                                this.leftPlayer = leftPlayer;
                                this.leftPlayer.Season = season1;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, leftPlayer);
                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                    }

                    if (this.familiarNames.ContainsKey(rightname))
                    {
                        PanelPlayer player = this.familiarNames[rightname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            RightPlayer rightPlayer = (RightPlayer)player;
                            rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                            this.rightPlayer = rightPlayer;
                            this.rightPlayer.Season = season2;

                        }
                    }
                    else
                    {
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(rightname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.rightPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                RightPlayer rightPlayer = (RightPlayer)player;
                                rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                                this.rightPlayer = rightPlayer;
                                this.rightPlayer.Season = season2;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, rightPlayer);



                               
                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                    }

                    this.ActivateButton(this.iconButton8, RGBColors.white);
                    if (this.leftPlayer != null && this.rightPlayer != null)
                    {
                        this.OpenChildForm(new RankedDoublePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedFPP, this));

                    }
                    else if (this.leftPlayer == null && this.rightPlayer != null)
                    {
                        this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));

                    }

                    else if (this.leftPlayer != null && this.rightPlayer == null)
                    {
                        this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));

                    }
                    else
                    {
                        MessageBox.Show("Both account names are not valid.");
                    }

                    this.txtLeftPlayerName.Enabled = true;
                    this.txtRightPlayerName.Enabled = true;
                }

                else if (leftname != "" || leftname != string.Empty)
                {

                    if (this.familiarNames.ContainsKey(leftname))
                    {

                        this.txtLeftPlayerName.Enabled = false;

                        PanelPlayer player = this.familiarNames[leftname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            LeftPlayer leftPlayer = (LeftPlayer)player;
                            leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));

                            this.leftPlayer = leftPlayer;
                            this.leftPlayer.Season = season1;

                            this.ActivateButton(this.iconButton8, RGBColors.white);
                            this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));


                        }
                    }
                    else
                    {
                        // fetch account informaiton from api
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(leftname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season1, "left"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.leftPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                                   Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                            }
                            else
                            {
                                LeftPlayer leftPlayer = (LeftPlayer)player;
                                leftPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(leftPlayer.AccountObj.data[0].relationships.matches.data));

                                this.leftPlayer = leftPlayer;
                                this.leftPlayer.Season = season1;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, leftPlayer);



                                this.ActivateButton(this.iconButton8, RGBColors.white);
                                this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, Values.StatType.RankedFPP));


                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }

                    }

                    this.txtLeftPlayerName.Enabled = true;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                else if (rightname != string.Empty || rightname != "")
                {
                    this.txtRightPlayerName.Enabled = false;

                    if (this.familiarNames.ContainsKey(rightname))
                    {
                        PanelPlayer player = this.familiarNames[rightname];
                        //fetch stats...from api

                        player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                        if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                        {
                            MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() +
                                                               Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());
                        }
                        else
                        {
                            RightPlayer rightPlayer = (RightPlayer)player;
                            rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                            this.rightPlayer = rightPlayer;
                            this.rightPlayer.Season = season2;

                            this.ActivateButton(this.iconButton8, RGBColors.white);
                            this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));


                        }
                    }
                    else
                    {
                        Tuple<RootAccountIDObject, int> pair = await Task.Run(() => this.GetAccountObjAsync(rightname));
                        RootAccountIDObject accObj = pair.Item1;
                        int errorCode = pair.Item2;

                        if (errorCode == 200)
                        {
                            // build player object
                            PanelPlayer player = this.BuildPlayer(accObj);

                            //fetch stats...from api
                            player = await Task.Run(() => this.BuildPlayerWithStatsAsync(player, season2, "right"));

                            if (player.NormalErrorCode != 200 || player.RankedErrorCode != 200)
                            {
                                this.rightPlayer = null;

                                MessageBox.Show(new APIResponse(player.NormalErrorCode).GetFormattedResponseStatLookUp() + 
                                    Environment.NewLine + new APIResponse(player.RankedErrorCode).GetFormattedResponseRankedLookUp());


                            }
                            else
                            {
                                RightPlayer rightPlayer = (RightPlayer)player;
                                rightPlayer.Matches = await Task.Run(() => UIMethods.GetMatchesParsed(rightPlayer.AccountObj.data[0].relationships.matches.data));

                                this.rightPlayer = rightPlayer;
                                this.rightPlayer.Season = season2;

                                //add to dictionary
                                this.familiarNames.Add(player.Name, rightPlayer);



                                this.ActivateButton(this.iconButton8, RGBColors.white);
                                this.OpenChildForm(new RankedSinglePlayer(this.rightPlayer, Values.StatType.RankedFPP));
                            }

                        }
                        else
                        {
                            MessageBox.Show(new APIResponse(errorCode).GetFormattedResponseAccounNameLookUp());
                        }
                    }

                    this.txtRightPlayerName.Enabled = true;
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                

                
                e.Handled = true;
                e.SuppressKeyPress = true;

                return;
            }
        }


        private bool BothPlayersNotNull()
        {
            if (this.leftPlayer != null && this.rightPlayer != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private string GetProperSeasonName(string season)
        {
            if (Enum.TryParse(season.Replace(" ", "_"), out Values.Seasons proper))
            {
                Values values = new Values();
                Dictionary<Values.Seasons, string> seasons = new Dictionary<Values.Seasons, string>();
                seasons = values.seasons;

                if (seasons.ContainsKey(proper))
                {
                    return seasons[proper];
                }
            }

            return null;
        }

        private void PopulateComboBox()
        {
            Values values = new Values();
            Dictionary<Values.Seasons, string> seasons = new Dictionary<Values.Seasons, string>();
            seasons = values.seasons;
            
           
            int i = 0;
            foreach(KeyValuePair<Values.Seasons, string> pair in seasons)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(pair.Key.ToString().Replace("_", " "));

                this.comboBox1.Items.Insert(i, builder.ToString());
                this.comboBox2.Items.Insert(i++, builder.ToString());
            }
        }


     

       

        

        private async Task<PanelPlayer> BuildPlayerWithStatsAsync(PanelPlayer player, string season, string side)
        {
            string seasonid = this.GetProperSeasonName(season);
            string name = player.Name;
            string accountID = player.AccountID;

            Tuple<StatsObject, int> statsNormal = await Task.Run(() => this.GetStatsAsync(accountID, seasonid, false));
            Tuple<StatsObject, int> statsRanked = await Task.Run(() => this.GetStatsAsync(accountID, seasonid, true));

            player.NormalStatsObj = (RootNormalStatsObject)statsNormal.Item1;
            player.RankedStatsObj = (RootRankedStatsObject)statsRanked.Item1;

            if (side == "left")
            {
                LeftPlayer leftplayer = new LeftPlayer(name, player.AccountID, player.AccountObj, player.NormalStatsObj, player.RankedStatsObj);
                leftplayer.NormalErrorCode = statsNormal.Item2;
                leftplayer.RankedErrorCode = statsRanked.Item2;
                return leftplayer;
            }
            else if (side == "right")
            {
                RightPlayer rightplayer = new RightPlayer(name, player.AccountID, player.AccountObj, player.NormalStatsObj, player.RankedStatsObj);
                rightplayer.NormalErrorCode = statsNormal.Item2;
                rightplayer.RankedErrorCode = statsRanked.Item2;
                return rightplayer;
            }
            else
            {
                return new PanelPlayer();
            }
        }

        private PanelPlayer BuildPlayer(RootAccountIDObject accountObj)
        {
            string name = accountObj.data[0].attributes.name;
            string accID = accountObj.data[0].id;

            PanelPlayer player = new PanelPlayer();
            player.Name = name;
            player.AccountID = accID;
            player.AccountObj = accountObj;

            return player;
        }

        
        private async Task<Tuple<RootAccountIDObject, int>> GetAccountObjAsync(string name)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor = new QueryExecutor(builder.GetAccountIDQuery(name));
            Tuple<string, int> account = await Task.Run(() => executor.ExecuteQueryAsync());

            Tuple<RootAccountIDObject, int> accountObjAndError = JsonParser.ParseAccountID(account);

            
            return accountObjAndError;
        }

        
        private async Task<Tuple<StatsObject, int>> GetStatsAsync(string accountid, string seasonid, bool ranked)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor;
            Tuple<string, int> statsJson;
            Tuple<StatsObject, int> statsObjAndError;

            if (!ranked)
            {
                executor = new QueryExecutor(builder.GetSeasonForPlayerQuery(accountid, seasonid));
                statsJson = await Task.Run(() => executor.ExecuteQueryAsync());
                Tuple<RootNormalStatsObject, int> pair = JsonParser.ParseNormalSeasonStats(statsJson);

                StatsObject statsObj = pair.Item1;
                int errorCode = pair.Item2;

                statsObjAndError = Tuple.Create(statsObj, errorCode);

            } 
            else
            {
                executor = new QueryExecutor(builder.GetRankedSeasonForPlayerQuery(accountid, seasonid));
                statsJson = await Task.Run(() => executor.ExecuteQueryAsync());
                Tuple<RootRankedStatsObject, int> pair = JsonParser.ParseRankedSeasonStats(statsJson);

                StatsObject statsObj = pair.Item1;
                int errorCode = pair.Item2;

                statsObjAndError = Tuple.Create(statsObj, errorCode);
            }

            return statsObjAndError;

        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.txtLeftPlayerName != null && this.txtLeftPlayerName.Text != "")
            {
                this.txtLeftPlayerName.Enabled = false;
                this.comboBox1.Enabled = false;




                if (this.firstLoad)
                {
                    this.LoadProgress(this.circularProgressBarLarge);
                    this.firstLoad = false;
                }
                else
                {
                   //this.LoadProgress(this.circularProgressBarTop);
                }

               

                this.txtLeftPlayerName.Enabled = true;
                this.comboBox1.Enabled = true;
            }

        }

        








        

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            

          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        /*
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = (FileStream)openFileDialog.OpenFile();

                Xml xml = new Xml();
                Dictionary<string, string> pairs = xml.LoadFromFile(stream);
                stream.Close();
                if (pairs != null || pairs.Count > 0)
                {
                    this.nameAndIdDict = pairs;

                    this.txtPlayerName.AutoCompleteMode = AutoCompleteMode.Append;
                    this.txtPlayerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    var autoComplete = new AutoCompleteStringCollection();

                    foreach (KeyValuePair<string, string> pair in this.nameAndIdDict)
                    {
                        autoComplete.Add(pair.Key.ToString());
                        Console.WriteLine("Name: " + pair.Key.ToString() + " ID: " + pair.Value.ToString());
                    }


                    


                    this.txtPlayerName.AutoCompleteCustomSource = autoComplete;
                }
                else
                {
                    Console.WriteLine("Error retreiving cached account id's from file...");
                }

                
            }*/

        /*
         if (this.nameAndIdDict.Count > 0)
         {
             SaveFileDialog saveFileDialog1 = new SaveFileDialog
             {
                 Filter = "XML|*.xml",
                 FilterIndex = 2,
                 RestoreDirectory = true,
             };

             if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 FileStream stream = (FileStream)saveFileDialog1.OpenFile();
                 Xml xml = new Xml();
                 xml.SaveToFile(stream, this.nameAndIdDict);
                 stream.Close();
             }


         }

         else
         {
             Console.WriteLine("Dictionary is empty...");
         }*/
    }
}
