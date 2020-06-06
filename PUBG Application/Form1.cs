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

namespace PUBG_Application
{
    public partial class MainWindow : Form
    {
        // Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private Dictionary<Values.Modes, ModeStats> stats = new Dictionary<Values.Modes, ModeStats>();

        private Tuple<RootAccountIDObject, Tuple<RootNormalStatsObject, RootRankedStatsObject>>[] accountPair =
            new Tuple<RootAccountIDObject, Tuple<RootNormalStatsObject, RootRankedStatsObject>>[2];

        private bool firstLoad = true;

        private Dictionary<string, RootAccountIDObject> nameAndIdDict = new Dictionary<string, RootAccountIDObject>();

        private LeftPlayer leftPlayer = null;
        private RightPlayer rightPlayer = null;
        private Dictionary<string, PanelPlayer> familiarNames = new Dictionary<string, PanelPlayer>();
        
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

            //this.circularProgressBarTop.Value = 0;
            //this.circularProgressBarTop.Update();

            this.txtRightPlayerName.Enabled = false;

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

        private void OpenChildForm(Form childForm)
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
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Solo));

            }
        }

        private void duoButton_clicked(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Duo));

            }
        }

        private void squadButton_clicked(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.Squad));

            }
        }

        private void soloFPPButton_clicked(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.SoloFPP));

            }
        }

        private void duoFPPButton_clicked(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.DuoFPP));

            }
        }

        private void squadFPPButton_clicked(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootNormalStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item1);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.SquadFPP));

            }


        }

        private void rankTPPButton_Click(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootRankedStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item2);

            if (this.accountPair[0] != null)
            {
                
                this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedTPP));

            }
        }

        private void rankFPPButton_Click(object sender, EventArgs e)
        {
            this.ActivateButton(sender, RGBColors.white);
            Tuple<RootAccountIDObject, RootRankedStatsObject> pair =
                Tuple.Create(this.accountPair[0].Item1, this.accountPair[0].Item2.Item2);

            if (this.accountPair[0] != null)
            {

                this.OpenChildForm(new RankedSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedFPP));

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
                    if (txt.GetType() == typeof(TextBox) && txt.Name == "txt" + lbl.Name.Remove(0, 3))
                    {
                        txt.Focus();
                    }
                    if (txt.GetType() == typeof(Panel) && txt.Name == "pnl" + txt.Name.Remove(0, 3))
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
                    if (txt.GetType() == typeof(TextBox) && txt.Name == "txt" + lbl.Name.Remove(0, 3))
                    {
                        txt.Focus();
                    }
                    if (txt.GetType() == typeof(Panel) && txt.Name == "pnl" + txt.Name.Remove(0, 3))
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
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
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
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
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
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
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
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = RGBColors.orange;
                }

                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
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


        private void txtPlayerName1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtLeftPlayerName.Enabled = false;
                this.comboBox1.Enabled = false;

                //single player mode
                // check if there is somethging in the left text box 
                // check if right text is disabled.
                if (this.txtRightPlayerName.Enabled == false && this.txtLeftPlayerName.Text != "")
                {
                    //check if name is in dictionary
                    if (this.familiarNames.ContainsKey(this.txtLeftPlayerName.Text.ToString()))
                    {
                        //if the name is present, skip to stat query

                    }
                    else
                    {
                        string name = this.txtLeftPlayerName.Text.ToString();
                        string side = "left";
                        Tuple<string, string> arguments = Tuple.Create(name, side);

                        // if name not present in dictionary, try to pull name.account from API
                        this.workerAccountID.RunWorkerAsync(arguments);
                    }
                }
                else if (this.txtRightPlayerName.Enabled && 
                    this.txtRightPlayerName.Text != "" && this.txtLeftPlayerName.Text != "")
                {

                }

                //this.workerAccountID.RunWorkerAsync(this.txtPlayerName1.Text.ToString());




                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtPlayerName2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                /*
                this.txtPlayerName2.Enabled = false;
                this.comboBox2.Enabled = false;




                if (this.firstLoad)
                {
                    this.LoadProgress(this.circularProgressBarLarge);
                    this.firstLoad = false;
                }
                else
                {
                    //this.LoadProgress(this.circularProgressBarTop);
                }

                if (!this.nameAndIdDict.ContainsKey(this.txtPlayerName2.Text.ToString()))
                {
                    this.workerAccountID.RunWorkerAsync(this.txtPlayerName2.Text.ToString());

                }


                else
                {
                    List<string> arguments = new List<string>();
                    //arguments.Add(this.nameAndIdDict[this.txtPlayerName.Text.ToString()]);
                    arguments.Add(this.GetProperSeasonName(this.comboBox2.SelectedItem.ToString()));

                    this.workerStats.RunWorkerAsync(arguments);
                }*/

                this.txtRightPlayerName.Enabled = true;
                this.comboBox2.Enabled = true;


                e.Handled = true;
                e.SuppressKeyPress = true;
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

        






        private Tuple<RootNormalStatsObject, int, RootRankedStatsObject, int> GetStats(string accountid, string seasonid)
        {
            QueryBuilder builderNormal = new QueryBuilder();
            QueryExecutor executorNormal = new QueryExecutor(builderNormal.GetSeasonForPlayerQuery(accountid, seasonid));
            Tuple<string, int> pairNormal = executorNormal.ExecuteQuery();
            Console.WriteLine("Starting task...getting stats for player");
            Tuple<RootNormalStatsObject, int> responseNormal = JsonParser.ParseNormalSeasonStats(pairNormal);


            QueryBuilder builderRanked = new QueryBuilder();
            QueryExecutor executorRanked = new QueryExecutor(builderRanked.GetRankedSeasonForPlayerQuery(accountid, seasonid));
            Tuple<string, int> pairRanked = executorRanked.ExecuteQuery();
            Console.WriteLine("Starting task...getting ranked stats for player");
            Tuple<RootRankedStatsObject, int> responseRanked = JsonParser.ParseRankedSeasonStats(pairRanked);


            Tuple<RootNormalStatsObject,int, RootRankedStatsObject, int> quadTuple = 
                Tuple.Create(responseNormal.Item1, responseNormal.Item2, responseRanked.Item1, responseRanked.Item2);

            return quadTuple;

        }

        private Tuple<RootAccountIDObject, int> GetAccountId(string name)
        {
            QueryBuilder builder = new QueryBuilder();
            QueryExecutor executor = new QueryExecutor(builder.GetAccountIDQuery(name));
            Console.WriteLine("Starting task...getting accountid");

            Tuple<string, int> pair = executor.ExecuteQuery();

            Tuple<RootAccountIDObject, int> reponse = JsonParser.ParseAccountID(pair);

            return reponse;
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

                if (!this.nameAndIdDict.ContainsKey(this.txtLeftPlayerName.Text.ToString()))
                {
                    this.workerAccountID.RunWorkerAsync(this.txtLeftPlayerName.Text.ToString());
                }
                else
                {
                    List<string> arguments = new List<string>();
                    //arguments.Add(this.nameAndIdDict[this.txtPlayerName.Text.ToString()]);
                    arguments.Add(this.GetProperSeasonName(this.comboBox1.SelectedItem.ToString()));

                    this.workerStats.RunWorkerAsync(arguments);
                }

                this.txtLeftPlayerName.Enabled = true;
                this.comboBox1.Enabled = true;
            }

        }

        private void DoWork_GetAccountID(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Tuple<string, string> arguments = e.Argument as Tuple<string, string>;

            string name = arguments.Item1;
            string side = arguments.Item2;

            Tuple<RootAccountIDObject, int> pair = this.GetAccountId(name);
            object[] paramsToSend = new object[3];
            paramsToSend[0] = pair.Item1;
            paramsToSend[1] = pair.Item2;
            paramsToSend[2] = side;
            e.Result = paramsToSend;
        }

        private void workerAccountID_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RootAccountIDObject accountObj;
            if (e.Result != null)
            {
                object[] paramsReceived = null;
                paramsReceived = e.Result as object[];
                accountObj = paramsReceived[0] as RootAccountIDObject;
                int code = Int32.Parse(paramsReceived[1].ToString());
                Tuple<RootAccountIDObject, int> response = Tuple.Create(accountObj, code);
                string side = paramsReceived[2].ToString();
                if (response.Item1 != null)
                {

                    string name = response.Item1.data[0].attributes.name;
                    RootAccountIDObject accountObject = response.Item1;

                    //add name and accountobject to global dictionary
                    this.nameAndIdDict.Add(name, accountObject);

                    string account_id = accountObject.data[0].id;
                    string season_id = this.GetProperSeasonName(this.comboBox1.SelectedItem.ToString());

                    Tuple<string, string> idAndSeason = Tuple.Create(account_id, season_id);

                    List<object> paramsToSend = new List<object>();
                    paramsToSend.Add(idAndSeason);
                    paramsToSend.Add(side);

                    this.familiarNames.Add(accountObj.data[0].attributes.name.ToString(), new LeftPlayer(accountObj));
                    this.workerStats.RunWorkerAsync(idAndSeason);       
                }

                else
                {
                    APIResponse errorcode = new APIResponse((int)response.Item2);
                    MessageBox.Show(errorcode.GetDetailedResponse());
                    Console.WriteLine("Account id failed A");
                    

                    if (side == "left" && accountObj == null)
                    {
                        this.leftPlayer = null;
                    }
                    else if (side == "right")
                    {
                        this.rightPlayer = null;
                    }

                }
            }
            
            else
            {
                Console.WriteLine("Account id failed B");

            }

        }

        private void workerAccountID_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // used for progress bars...
        }








        private void DoWork_GetStats(object sender, DoWorkEventArgs e)
        {
            List<object> paramsReceived = e.Result as List<object>;

            Tuple<string, string> idAndSeason = paramsReceived[0] as Tuple<string, string>;
            string side = paramsReceived[1].ToString();

            string account_id = idAndSeason.Item1;
            string season = idAndSeason.Item2;

            List<object> paramsToSend = new List<object>();

            paramsToSend.Add(this.GetStats(account_id, season));
            paramsToSend.Add(side);
            e.Result = paramsToSend;
        }

        private void workerStats_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<object> paramsReceived = e.Result as List<object>;


            Tuple<RootNormalStatsObject, int, RootRankedStatsObject, int> responseQuadTuple = 
                paramsReceived[0] as Tuple<RootNormalStatsObject, int, RootRankedStatsObject, int>;

            string side = paramsReceived[1].ToString();

            if (responseQuadTuple.Item1 != null || responseQuadTuple.Item3 != null)
            {
                if (side == "left")
                {
                    // make acc obj reference from dictionary
                    RootAccountIDObject accountObj = this.nameAndIdDict[this.txtLeftPlayerName.Text.ToString()];
                    RootNormalStatsObject normalStats = responseQuadTuple.Item1;
                    RootRankedStatsObject rankedStats = responseQuadTuple.Item3;

                    this.leftPlayer = new LeftPlayer(
                        this.txtLeftPlayerName.Text.ToString(), 
                        accountObj.data[0].id.ToString(),
                        accountObj, 
                        normalStats, 
                        rankedStats);
                }
                else if (side == "right")
                {
                    // make acc obj reference from dictionary
                    RootAccountIDObject accountObj = this.nameAndIdDict[this.txtLeftPlayerName.Text.ToString()];
                    RootNormalStatsObject normalStats = responseQuadTuple.Item1;
                    RootRankedStatsObject rankedStats = responseQuadTuple.Item3;

                    this.rightPlayer = new RightPlayer(
                        this.txtRightPlayerName.Text.ToString(),
                        accountObj.data[0].id.ToString(),
                        accountObj,
                        normalStats,
                        rankedStats);
                }


                this.ActivateButton(this.iconButton8, Color.White);
                
                this.OpenChildForm(new NormalSinglePlayer(this.leftPlayer, this.rightPlayer, Values.StatType.RankedFPP));

                if (!this.firstLoad)
                {
                    //this.circularProgressBarTop.Value = 0;
                    //this.circularProgressBarTop.Update();
                }
            } 
            else
            {
                Console.WriteLine("Failed getting stats");
                APIResponse response = new APIResponse((int)responseQuadTuple.Item2);
                MessageBox.Show(response.GetDetailedResponse().ToString());
            }
        }

        private void workerStats_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //used for progress bars...
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            

          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            /*
            QueryBuilder queryBuilder = new QueryBuilder();
            string url = queryBuilder.GetRankedSeasonForPlayerQuery("account.0165929b76b147d1a453bbfd21a58b4b", "division.bro.official.pc-2018-07");

            QueryExecutor executor = new QueryExecutor(url);
            object [] objs = executor.ExecuteQuery();
            string json = objs[0].ToString();

            RootRankedObject response = JsonConvert.DeserializeObject<RootRankedObject>(json);

            Console.WriteLine(response.ToString());*/
            /*
            QueryBuilder queryBuilder = new QueryBuilder();
            string url = queryBuilder.GetSeasonForPlayerQuery("account.0165929b76b147d1a453bbfd21a58b4b", "division.bro.official.pc-2018-07");
            QueryExecutor executor = new QueryExecutor(url);
            object[] objs = executor.ExecuteQuery();
            string json = objs[0].ToString();

            RootNormalObject response = JsonConvert.DeserializeObject<RootNormalObject>(json);

            Console.WriteLine(response.ToString());*/
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
