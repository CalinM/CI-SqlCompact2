using Common;
using Desene.DetailFormsAndUserControls.Movies;
using Desene.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using DAL;

using Utils;
using Helpers = Utils.Helpers;
using System.Text;
using System.Drawing.Text;


namespace Desene
{
    public partial class FrmMain : BaseApplicationForm
    {
        private bool _formActive = false;
        public event EventHandler OnAddButtonPress;
        public event EventHandler OnDeleteButtonPress;
        public event EventHandler OnCloseModule;
        private IniFile _iniFile = new IniFile();
        private Size _minSizeWithModule = new Size(1284, 514);
        private Size _minSizeWithoutModule = new Size(500, 250);

        public FrmMain()
        {
            InitializeComponent();

            mainMenu.ImageScalingSize = new Size(16, 16);

            separatorMainButtons.Visible = false;
            btnAdd.Visible = false;
            btnDelete.Visible = false;
            //ddbTools.Visible = false;

            miCheckFiles.Visible = false;
            miImportSynopsis.Visible = false;
            miImportCommonSenseMediaData.Visible = false;
            miSQLmanagement.Visible = true;

            MinimumSize = _minSizeWithoutModule;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            /*
            //using MessageBox instead of MsgBox because the "owner" is not available yet
            if (!File.Exists("CartoonsRepo.sdf"))
            {
                var opRes = DatabaseOperations.CreateDatabase();

                if (!opRes.Success)
                {
                    if (File.Exists("CartoonsRepo.sdf"))
                        File.Delete("CartoonsRepo.sdf");

                    MessageBox.Show(
                        string.Format("The following error occurred while creating the database:{0}{0}{1}{0}{0}The application will now close!", Environment.NewLine, opRes.CustomErrorMessage),
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Close();
                }
            }
            else
            {
                DatabaseOperations.CreateField("FileDetail", "Synopsis", "ntext NULL");
                DatabaseOperations.CreateIndex("AudioStream", "audio1", "FileDetailId ASC");
                DatabaseOperations.CreateIndex("VideoStream", "video1", "FileDetailId ASC");
                DatabaseOperations.CreateIndex("Thumbnails", "stills1", "FileDetailId ASC");
                DatabaseOperations.CreateField("FileDetail", "SectionType", "int NULL");
            }*/

            if (!File.Exists("movies-index.db"))
                File.WriteAllBytes("movies-index.db", Resources.movies_index);

            DatabaseOperations.ExecuteSqlString(@"
                CREATE TABLE IF NOT EXISTS CommonSenseMediaDetail (
                    Id                 INTEGER        PRIMARY KEY AUTOINCREMENT,
                    DateTimeStamp      DATETIME,
                    FileDetailId       INTEGER        REFERENCES FileDetail (Id)
                                                      NOT NULL,
                    GreenAge           VARCHAR (15),
                    Rating             INT,
                    ShortDescription   VARCHAR (1000),
                    Review             TEXT,
                    AdultRecomendedAge VARCHAR (15),
                    AdultRating        INT,
                    ChildRecomendedAge VARCHAR (15),
                    ChildRating        INT,
                    Story              TEXT,
                    IsItAnyGood        TEXT
                );");

            DatabaseOperations.ExecuteSqlString(@"
                CREATE TABLE IF NOT EXISTS CommonSenseMediaDetail_TalkAbout (
                    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                    CSMDetailId   INT     REFERENCES CommonSenseMediaDetail (Id),
                    SummaryPoint TEXT
                );");

            DatabaseOperations.ExecuteSqlString(@"
                CREATE TABLE IF NOT EXISTS CommonSenseMediaDetail_ALotOrALittle (
                    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    CSMDetailId INT     REFERENCES CommonSenseMediaDetail (Id),
                    Category    INT,
                    Rating      INT,
                    Description TEXT
                );");

            DatabaseOperations.ExecuteSqlString(@"
                CREATE INDEX IF NOT EXISTS csmDetail1 ON CommonSenseMediaDetail (
                    FileDetailId ASC
                );");

            DatabaseOperations.ExecuteSqlString(@"
                CREATE INDEX IF NOT EXISTS csmALotOrALittle1 ON CommonSenseMediaDetail_ALotOrALittle (
                    CSMDetailId ASC
                );");

            DatabaseOperations.ExecuteSqlString(@"
                CREATE INDEX IF NOT EXISTS csmTalkAbout1 ON CommonSenseMediaDetail_TalkAbout (
                    CSMDetailId ASC
                );");

            pMainContainer.Controls.Clear();

            DAL.LoadBaseDbValues();

            LoadMainWindowConfig();

            Common.Helpers.MainFormHandle = Handle;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _formActive = true;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            _formActive = false;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Helpers.ConfirmDiscardChanges();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _iniFile.Write("Top", Location.Y.ToString(), "MainWindow");
            _iniFile.Write("Left", Location.X.ToString(), "MainWindow");

            var xSize = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;
            _iniFile.Write("Width", xSize.Width.ToString(), "MainWindow");
            _iniFile.Write("Height", xSize.Height.ToString(), "MainWindow");
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (!Helpers.ConfirmDiscardChanges())
                return;

            Close();
        }

        private void LoadMainWindowConfig()
        {
            if (_iniFile.KeyExists("Top", "MainWindow") && _iniFile.KeyExists("Left", "MainWindow"))
            {
                Location = new Point(_iniFile.ReadInt("Left", "MainWindow"), _iniFile.ReadInt("Top", "MainWindow"));
            }

            if (_iniFile.KeyExists("Width", "MainWindow") && _iniFile.KeyExists("Height", "MainWindow"))
            {
                Size = new Size(_iniFile.ReadInt("Width", "MainWindow"), _iniFile.ReadInt("Height", "MainWindow"));
            }

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var buildDate = new DateTime(2010, 1, 1).AddDays(version.Revision);
            Text = "Movies Indexer v" + string.Format("{0} (build date {1})", version, buildDate.ToString("dd.MM.yyyy"));
        }

        private ucMovies _ucMovies;
        private void miMovies_Click(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);
                    GetStatistics(true, Sections.Movies);

                    pMainContainer.Controls.Clear();
                    _ucMovies = new ucMovies(this) { Dock = DockStyle.Fill };

                    pMainContainer.Controls.Add(_ucMovies);

                    miCheckFiles.Visible = true;
                    miImportSynopsis.Visible = true;
                    miImportCommonSenseMediaData.Visible = true;

                    MinimumSize = _minSizeWithModule;
                }
                else
                {
                    if (!(OnCloseModule is null))
                        OnCloseModule(sender, e);

                    _ucMovies = null;
                    senderItem.Checked = false;

                    pMainContainer.Controls.Clear();

                    GetStatistics(false, Sections.Movies);

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = false;

                    MinimumSize = _minSizeWithoutModule;
                }

                SetMainCrudButtonsState(senderItem.Checked, "Add movie");

                //todo: check if necessary
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                DrawingControl.ResumeDrawing(pMainContainer);
                Cursor = Cursors.Default;
            }
        }

        private void miMoviesList_Click(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);
                    GetStatistics(true, Sections.Movies);

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucMoviesList(this) { Dock = DockStyle.Fill });

                    miCheckFiles.Visible = true;
                    miImportSynopsis.Visible = true;
                    miImportCommonSenseMediaData.Visible = true;

                    MinimumSize = _minSizeWithModule;
                }
                else
                {
                    if (!(OnCloseModule is null))
                        OnCloseModule(sender, e);

                    senderItem.Checked = false;

                    pMainContainer.Controls.Clear();

                    GetStatistics(false, Sections.Movies);

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = false;

                    MinimumSize = _minSizeWithoutModule;
                }

                SetMainCrudButtonsState(senderItem.Checked, "Add movie");

                //todo: check if necessary
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                DrawingControl.ResumeDrawing(pMainContainer);
                Cursor = Cursors.Default;
            }
        }

        private void miCollections_Click(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);
                    GetStatistics(true, Sections.Collections);

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucCollections(this) { Dock = DockStyle.Fill });

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = false;

                    MinimumSize = _minSizeWithModule;
                }
                else
                {
                    GetStatistics(false, Sections.Collections);

                    senderItem.Checked = false;
                    pMainContainer.Controls.Clear();

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = false;

                    MinimumSize = _minSizeWithoutModule;
                }

                SetMainCrudButtonsState(senderItem.Checked, "Add collection");

                //todo: check if necessary
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                DAL.EpisodeParentType = EpisodeParentType.Collection;
                DrawingControl.ResumeDrawing(pMainContainer);
                Cursor = Cursors.Default;
            }
        }

        private void miSeries_Click(object sender, EventArgs e)
        {
            LoadSeriesTypeControls(sender, SeriesType.Final);
        }

        private void miRecordings_Click(object sender, EventArgs e)
        {
            LoadSeriesTypeControls(sender, SeriesType.Recordings);
        }

        private void LoadSeriesTypeControls(object sender, SeriesType st)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);

                    GetStatistics(true, st == SeriesType.Final ? Sections.Series : Sections.Recordings);

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucSeries(this, st) { Dock = DockStyle.Fill });

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = true;

                    MinimumSize = _minSizeWithModule;
                }
                else
                {
                    senderItem.Checked = false;
                    pMainContainer.Controls.Clear();

                    GetStatistics(false, st == SeriesType.Final ? Sections.Series : Sections.Recordings);

                    miCheckFiles.Visible = false;
                    miImportSynopsis.Visible = false;
                    miImportCommonSenseMediaData.Visible = false;

                    MinimumSize = _minSizeWithoutModule;
                }

                SetMainCrudButtonsState(senderItem.Checked, "Add " + (st == SeriesType.Final ? "series" : "recordings group"));

                //todo: check if necessary
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                DAL.EpisodeParentType = EpisodeParentType.Series;
                DrawingControl.ResumeDrawing(pMainContainer);
                Cursor = Cursors.Default;
            }
        }

        private void GetStatistics(bool show, Sections section)
        {
            if (show)
            {
                var opRes = DAL.GetStatistics(section);

                sslbStatistics.Text =
                    opRes.Success
                        ? (string)opRes.AdditionalDataReturn
                        : opRes.CustomErrorMessage;

                /*
                var moviesInList = (string)opRes.AdditionalDataReturn != "There are no movies in the list ...";

                sslbClick.Visible = !forSeries && moviesInList;
                sslbAdditionalInfo1.Visible = !forSeries && moviesInList;
                sslbAdditionalInfo2.Visible = !forSeries && moviesInList;*/
            }

            sslbStatistics.Visible = show;
        }

        public void SetStatistics(bool show, string text)
        {
            sslbStatistics.Visible = show;
            sslbStatistics.Text = text;

            sslbInfo2.Visible = false;
            sslbInfo2.Text = "";
        }

        public void SetInfo2(bool show, string text)
        {
            sslbInfo2.Visible = show;
            sslbInfo2.Text = text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (OnAddButtonPress is null) return;

            OnAddButtonPress(sender, e);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDeleteButtonPress is null) return;

            OnDeleteButtonPress(sender, e);
        }

        private void ClearAllDelegates()
        {
            if (OnAddButtonPress == null) return;

            foreach (Delegate d in OnAddButtonPress.GetInvocationList())
            {
                OnAddButtonPress -= (EventHandler)d;
            }

            if (OnDeleteButtonPress == null) return;

            foreach (Delegate d in OnDeleteButtonPress.GetInvocationList())
            {
                OnDeleteButtonPress -= (EventHandler)d;
            }
        }

        void MarkCurrentCategory(ToolStripMenuItem currentSelected)
        {
            miSQLmanagement.Checked = false;

            DAL.CachedMTDs = new Dictionary<int, MovieTechnicalDetails>();

            //var x1 = btnCategory.DropDownItems.Where<ToolStripDropDownItem>(x => x.GetType() is ToolStripSeparator);
            foreach (var menuItem in btnCategory.DropDownItems)
            {
                if (menuItem is ToolStripMenuItem && ((ToolStripMenuItem)menuItem).Tag != null)
                    ((ToolStripMenuItem)menuItem).Checked = false;
            }

            currentSelected.Checked = true;
        }

        void SetMainCrudButtonsState(bool b, string caption)
        {
            separatorMainButtons.Visible = b;
            btnAdd.Visible = b;
            btnDelete.Visible = b;

            if (b) btnAdd.Text = caption;
        }

        private void sslbClick_Click(object sender, EventArgs e)
        {
            /*
            MsgBox.Show(
                string.Format(
                    "{0}{1}{1}{2}",
                    DAL.SectionDetails,
                    Environment.NewLine,
                    "Warning! The list only refreshes when a section is shown!"),
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //, new Font("Courier New", 8, FontStyle.Regular)
            */
        }










        private void button1_Click(object sender, EventArgs e)
        {
            /*
            var opRes = OldDataMigration.ImportFilmeHD();

            if (!opRes.Success)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while importing data (1):{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            MessageBox.Show("Done");*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            var opRes = OldDataMigration.ImportSeriale();
            if (!opRes.Success)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while importing data (2):{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            MessageBox.Show("Done");*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                var conn = new SqlCeConnection(Constants.ConnectionString);
                var cmd = new SqlCeCommand("select Id, NlAudioSource, Notes from FileDetail WHERE NlAudioSource <> '' AND NlAudioSource <> 'fara Nl' ", conn);
                var cmd2 = new SqlCeCommand("UPDATE AudioStream SET AudioSource = @AudioSource WHERE FileDetailId = @FileDetailId AND Language = 'nl' ", conn);
                var cmd3 = new SqlCeCommand("UPDATE FileDetail SET Notes = @Notes WHERE Id = @FileDetailId ", conn);
                var cmd4 = new SqlCeCommand("UPDATE FileDetail SET NlAudioSource = @NlAudioSource  WHERE Id = @FileDetailId ", conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fileId = (int)(long)reader["Id"];
                        var nlSourceVal = reader["NlAudioSource"].ToString();
                        var notes = reader["Notes"].ToString();

                        if (nlSourceVal.IndexOf(",") >= 0)
                            continue;

                        var extraInfo = nlSourceVal.Replace("*", "").Replace(" (", "").Replace(")", "").Trim();
                        var nlSource2 = Regex.Replace(reader["NlAudioSource"].ToString(), "[^*]", "");

                        cmd2.Parameters.Clear();
                        cmd2.Parameters.AddWithValue("@AudioSource", nlSource2.Length);
                        cmd2.Parameters.AddWithValue("@FileDetailId", fileId);
                        cmd2.ExecuteNonQuery();


                        if (!string.IsNullOrEmpty(extraInfo))
                        {
                            extraInfo =
                                string.Format("{0}{1}{1}AudioNl: {2}",
                                    string.IsNullOrEmpty(notes) ? string.Empty : notes,
                                    string.IsNullOrEmpty(notes) ? string.Empty : Environment.NewLine,
                                    extraInfo
                                );

                            cmd3.Parameters.Clear();
                            cmd3.Parameters.AddWithValue("@Notes", extraInfo);
                            cmd3.Parameters.AddWithValue("@FileDetailId", fileId);
                            cmd3.ExecuteNonQuery();
                        }

                        cmd4.Parameters.Clear();
                        cmd4.Parameters.AddWithValue("@NlAudioSource", "done - " + nlSourceVal);
                        cmd4.Parameters.AddWithValue("@FileDetailId", fileId);
                        cmd4.ExecuteNonQuery();
                        var x = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */

            /*
            var filesPath = string.Empty;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
                    return;


                filesPath = folderBrowserDialog.SelectedPath;
            }

            var shortMoviesData = DAL.GetMoviesGridData();
            var filesWithIssues = new List<string>();
            */
            /*

            foreach (var filePath in Directory.GetFiles(filesPath))
            {
                var fi = new FileInfo(filePath);
                var fileName = Path.GetFileNameWithoutExtension(fi.Name);

                var smdObj = shortMoviesData.FirstOrDefault(m => m.FileName == fileName);

                if (smdObj == null)
                {
                    filesWithIssues.Add("notfound: " + fileName);
                    continue;
                }

                try
                {
                    #region GetMovie details from file






                    #endregion

                    using (var conn = new SqlCeConnection(Constants.ConnectionString))
                    {
                        conn.Open();
                        SqlCeCommand cmd;

                        cmd = new SqlCeCommand("delete from VideoStream where FileDetailId = " + smdObj.Id, conn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCeCommand("delete from AudioStream where FileDetailId = " + smdObj.Id, conn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCeCommand("delete from SubtitleStream where FileDetailId = " + smdObj.Id, conn);
                        cmd.ExecuteNonQuery();


                        #region FileInfo

                        const string fileInfoSql =
                            @"
                            UPDATE FileDetail (
                               SET Format = @Format,
                                   FileSize = @FileSize,
                                   FileSize2 = @FileSize2,
                                   TitleEmbedded = @Title,
                                   Encoded_Application = @Encoded_Application,
                                   CoverEmbedded = @Cover,
                                   Duration = @Duration
                             WHERE Id = @Id";

                            cmd = new SqlCeCommand(fileInfoSql, conn) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@Format", mtd.Format);
                            cmd.Parameters.AddWithValue("@FileSize", mtd.FileSize);
                            cmd.Parameters.AddWithValue("@FileSize2", mtd.FileSize2);
                            cmd.Parameters.AddWithValue("@Title", mtd.Title);
                            cmd.Parameters.AddWithValue("@Encoded_Application", mtd.Encoded_Application);
                            cmd.Parameters.AddWithValue("@CoverEmbedded", mtd.Cover);
                            cmd.Parameters.AddWithValue("@Duration", mtd.Duration2);
                            cmd.ExecuteNonQuery();

                        #endregion

                        #region VideoStream data

                        const string videoStreamSql =
                            @"
                            INSERT INTO VideoStream (
                                FileDetailId,
                                Index,
                                Format,
                                Format_Profile,
                                BitRateMode,
                                BitRate,
                                Width,
                                Height,
                                FrameRate_Mode,
                                FrameRate,
                                Delay,
                                StreamSize,
                                TitleEmbedded,
                                Language)
                            VALUES (
                                @FileDetailId,
                                @Index,
                                @Format,
                                @Format_Profile,
                                @BitRateMode,
                                @BitRate,
                                @Width,
                                @Height,
                                @FrameRate_Mode,
                                @FrameRate,
                                @Delay,
                                @StreamSize,
                                @TitleEmbedded,
                                @Language)";


                        foreach (var vs in mtd.VideoStreams)
                        {
                            cmd = new SqlCeCommand(videoStreamSql, conn) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@FileDetailId", smdObj.Id);
                            cmd.Parameters.AddWithValue("@Index", vs.Index);
                            cmd.Parameters.AddWithValue("@Format_Profile", vs.Format_Profile);
                            cmd.Parameters.AddWithValue("@BitRateMode", vs.BitRateMode);
                            cmd.Parameters.AddWithValue("@BitRate", vs.BitRate);
                            cmd.Parameters.AddWithValue("@Width", vs.Width);
                            cmd.Parameters.AddWithValue("@Height", vs.Height);
                            cmd.Parameters.AddWithValue("@FrameRate_Mode", vs.FrameRate_Mode);
                            cmd.Parameters.AddWithValue("@FrameRate", vs.FrameRate);
                            cmd.Parameters.AddWithValue("@Delay", vs.Delay);
                            cmd.Parameters.AddWithValue("@StreamSize", vs.StreamSize);
                            cmd.Parameters.AddWithValue("@Title", vs.Title);
                            cmd.Parameters.AddWithValue("@Language", vs.Language);

                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                        #region AudioStream data

                        const string audioStreamSql =
                            @"
                            INSERT INTO AudioStream (
                                FileDetailId,
                                Index,
                                Format,
                                Channel,
                                ChannelPosition,
                                SamplingRate,
                                Resolution,
                                Delay,
                                Video_Delay,
                                StreamSize,
                                TitleEmbedded,
                                Language)
                            VALUES (
                                @FileDetailId,
                                @Index,
                                @Format,
                                @Channel,
                                @ChannelPosition,
                                @SamplingRate,
                                @Resolution,
                                @Delay,
                                @Video_Delay,
                                @StreamSize,
                                @Title,
                                @Language)";


                        foreach (var aus in mtd.AudioStreams)
                        {
                            cmd = new SqlCeCommand(audioStreamSql, conn) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@FileDetailId", smdObj.Id);
                            cmd.Parameters.AddWithValue("@Index", aus.Index);
                            cmd.Parameters.AddWithValue("@Format", aus.Format);
                            cmd.Parameters.AddWithValue("@Channel", aus.Channel);
                            cmd.Parameters.AddWithValue("@ChannelPosition", aus.ChannelPosition);
                            cmd.Parameters.AddWithValue("@SamplingRate", aus.SamplingRate);
                            cmd.Parameters.AddWithValue("@Resolution", aus.Resolution);
                            cmd.Parameters.AddWithValue("@Delay", aus.Delay);
                            cmd.Parameters.AddWithValue("@Video_Delay", aus.Video_Delay);
                            cmd.Parameters.AddWithValue("@StreamSize", aus.StreamSize);
                            cmd.Parameters.AddWithValue("@Title", aus.Title);
                            cmd.Parameters.AddWithValue("@Language", aus.Language);

                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                        #region SubtitleStream data

                        const string subtitleStreamSql =
                            @"
                            INSERT INTO VideoStream (
                                FileDetailId,
                                Index,
                                Format,
                                StreamSize,
                                TitleEmbedded,
                                Language)
                            VALUES (
                                @FileDetailId,
                                @Index,
                                @Format,
                                @StreamSize,
                                @Title,
                                @Language)";


                        foreach (var ss in mtd.SubtitleStreams)
                        {
                            cmd = new SqlCeCommand(subtitleStreamSql, conn) { CommandType = CommandType.Text };
                            cmd.Parameters.AddWithValue("@FileDetailId", smdObj.Id);
                            cmd.Parameters.AddWithValue("@Index", ss.Index);
                            cmd.Parameters.AddWithValue("@Format", ss.Format);
                            cmd.Parameters.AddWithValue("@StreamSize", ss.StreamSize);
                            cmd.Parameters.AddWithValue("@Title", ss.Title);
                            cmd.Parameters.AddWithValue("@Language", ss.Language);

                            cmd.ExecuteNonQuery();
                        }

                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    filesWithIssues.Add("error: " + fileName + " ---> " + ex.Message);
                }
            }
            */
            //var x = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //////MsgBox.Show("desc", "title", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1,
            //////    new Font("Times New Roman", 15, FontStyle.Bold) );
            /////

            //var moviesData = Desene.DAL.GetMoviesForWeb();

            ///*
            //    select top 50 FileName, InsertedDate from FIleDetail
            //    where ParentId is null
            //    order by insertedDate desc
            //*/

            //var newMovies =
            //    moviesData
            //        .OrderByDescending(o => o.InsertedDate)
            //        .Select(md => md.FN)
            //        .Take(25)
            //        .ToList();


            ///*
            //    select top 50 FileName, LastChangeDate,
            //    DATEDIFF(d, InsertedDate, LastChangeDate) AS Diff
            //    from FIleDetail
            //    where ParentId is null and DATEDIFF(d, InsertedDate, LastChangeDate) > 1
            //    order by LastChangeDate des
            //*/

            //var updatedMovies =
            //    moviesData
            //        .Where(md => md.LastChangeDate.Subtract(md.InsertedDate).Days > 1)
            //        .OrderByDescending(o => o.LastChangeDate)
            //        .Select(md => md.FN)
            //        .Take(25)
            //        .ToList();

            //var seriesData = Desene.DAL.GetSeriesForWeb();
            //var episodesData = Desene.DAL.GetEpisodesForWeb(true);

            //var seriesWithInsertedEp = new List<int>();

            //foreach (var epData in episodesData.OrderByDescending(o => o.InsertedDate))
            //{
            //    if (seriesWithInsertedEp.IndexOf(epData.SId) == -1)
            //    {
            //        seriesWithInsertedEp.Add(epData.SId);

            //        if (seriesWithInsertedEp.Count() >= 10)
            //            break;
            //    }
            //}

            //var x = 1;

            Utils.Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "Synopsis retrieval",
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", 5000, null);
        }

        private void btnGenerateHtml_Click(object sender, EventArgs e)
        {
            var genParams = new FrmSiteGenParams(_iniFile.ReadString("LastPath", "General")) { Owner = this };

            if (genParams.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _iniFile.Write("LastPath", Path.GetFullPath(genParams.SiteGenParams.Location), "General");

                var opRes = SiteGenerator.GenerateSiteFiles(genParams.SiteGenParams, this.Handle);

                if (!opRes.Success)
                {
                    if (opRes.CustomErrorMessage == "Operation has been canceled")
                    {
                        MessageBox.Show("Operation has been canceled", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            string.Format("The following error occurred generating the site:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return;
                }

                #region Site images

                var imagesPath = Path.Combine(genParams.SiteGenParams.Location, "Images");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                new Bitmap(Resources.pixel).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\pixel.gif"), ImageFormat.Gif);
                new Bitmap(Resources.info).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\info.png"), ImageFormat.Png);
                new Bitmap(Resources.msg).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\msg.png"), ImageFormat.Png);
                new Bitmap(Resources.arrowRight12).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\arrowRight12.png"), ImageFormat.Png);
                new Bitmap(Resources.arrowDown12).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\arrowDown12.png"), ImageFormat.Png);
                new Bitmap(Resources.search).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\search.png"), ImageFormat.Png);
                new Bitmap(Resources.thumbnail).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\thumbnail.png"), ImageFormat.Png);
                new Bitmap(Resources.mickey_mouse).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\mickey_mouse.png"), ImageFormat.Png);
                new Bitmap(Resources.nav_icon).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\nav_icon.png"), ImageFormat.Png);
                new Bitmap(Resources.settings16_h).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\settings16_h.png"), ImageFormat.Png);
                new Bitmap(Resources.settings16_n).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\settings16_n.png"), ImageFormat.Png);
                new Bitmap(Resources.settings24_n).Save(Path.Combine(genParams.SiteGenParams.Location, "Images\\settings24_n.png"), ImageFormat.Png);

                #endregion

                var genUniqueId = string.Empty;//DateTime.Now.ToString("yyyyMMddhhmmss");

                #region Site scripts

                var serializedData = (GeneratedJSData)opRes.AdditionalDataReturn;

                var scriptPath = Path.Combine(genParams.SiteGenParams.Location, "Scripts");
                if (!Directory.Exists(scriptPath))
                    Directory.CreateDirectory(scriptPath);

                //File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\detaliiFilme_{genUniqueId}.js"), serializedData.Key);
                //File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\detaliiSeriale_{genUniqueId}.js"), serializedData.Value);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\moviesDetails.js"), serializedData.MoviesData);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\seriesDetails.js"), serializedData.SeriesData);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\recordingDetails.js"), serializedData.RecordingsData);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\moviesDetails2.js"), serializedData.MoviesDetails2);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\collectionDetails.js"), serializedData.CollectionsData);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\collectionDetails2.js"), serializedData.CollectionsDetails2);

                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery-2.2.4.min.js"), Resources.jquery_2_2_4_min);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.contextMenu.min.js"), Resources.jquery_contextMenu_minJS);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.ui.position.min.js"), Resources.jquery_ui_position_minJS);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.lazy.min.js"), Resources.jquery_lazy_min);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.slimscroll.min.js"), Resources.jquery_slimscroll_min);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jsgrid.min.js"), Resources.jsgrid_minJS);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\owl.carousel.min.js"), Resources.owl_carousel_minJS);

                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\cr_main.js"), SiteGenerator.MinifyScript(genParams.SiteGenParams, Resources.cr_mainJS));
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\cr_shared.js"), SiteGenerator.MinifyScript(genParams.SiteGenParams, Resources.cr_sharedJS), Encoding.UTF8); //also set the file encoding to UTF8 in the Resources  !!!
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\cr_collections.js"), SiteGenerator.MinifyScript(genParams.SiteGenParams, Resources.cr_collectionsJS));
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\cr_movies.js"), SiteGenerator.MinifyScript(genParams.SiteGenParams, Resources.cr_moviesJS));
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\cr_series.js"), SiteGenerator.MinifyScript(genParams.SiteGenParams, Resources.cr_seriesJS));

                //File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\YouTubePopUp.jquery.js"), Resources.YouTubePopUp_jquery);

                #endregion

                #region Site styles

                var stylesPath = Path.Combine(genParams.SiteGenParams.Location, "Styles");
                if (!Directory.Exists(stylesPath))
                    Directory.CreateDirectory(stylesPath);

                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\desene.css"), Resources.deseneCSS); //not working on CSS !!
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\jsgrid-theme.min.css"), Resources.jsgrid_theme_min);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\jsgrid.min.css"), Resources.jsgrid_minCSS);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\sections.css"), Resources.sections);
                //File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\YouTubePopUp.css"), Resources.YouTubePopUp);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\owl.carousel.min.css"), Resources.owl_carousel_minCSS);
                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\jquery.contextMenu.min.css"), Resources.jquery_contextMenu_minCSS);

                #endregion

                File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "index.html"), Resources.index.Replace("##", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


            //if (this.Focused())
            if (!_formActive)
                FlashWindow.Flash(this, 5);

            if (MsgBox.Show("The new site files have been saved! Do you want to open the folder location?", "Info",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process.Start(genParams.SiteGenParams.Location);
            }
        }

        private void btnFilesDetails_Click(object sender, EventArgs e)
        {
            var selectedPath = Helpers.SelectFolder("Please select the files location", _iniFile.ReadString("LastPath", "General"));
            if (string.IsNullOrEmpty(selectedPath))
                return;

            _iniFile.Write("LastPath", Path.GetFullPath(selectedPath), "General");

            Helpers.GetFilesDetails(selectedPath, this);
        }

        //private void GetFilesDetails(string path)
        //{
        //    var opRes = Utils.Helpers.GetFilesDetails(path);

        //    if (!opRes.Success)
        //        Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Warning, "File details",
        //            opRes.CustomErrorMessage, 5000, this);
        //    else
        //    {

        //    }
        //}

        private void BtnBuildFileNames_Click(object sender, EventArgs e)
        {
            var frmNfNamesMix = new FrmNfNamesMix() { Owner = this };
            frmNfNamesMix.ShowDialog();
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            var droppedObj = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            OperationResult opRes;

            if (File.GetAttributes(droppedObj).HasFlag(FileAttributes.Directory))
            {
                opRes = Helpers.GetFilesDetails(droppedObj, this);
            }
            else
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Count() > 1)
                {
                    MsgBox.Show("Handling multiple files is not yet supported!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var ext = Path.GetExtension(files[0]);

                switch (ext)
                {
                    case ".mp4":
                    case ".mkv":
                    case ".avi":
                        opRes = Helpers.GetFilesDetails(files, this);
                        break;

                    case ".txt":
                    case ".srt":
                    case ".sub":
                    case ".vob":
                        opRes = Helpers.FixDiacriticsInTextFile(droppedObj);
                        break;

                    default:
                        MsgBox.Show(string.Format("The file extension ({0}) cannot be handled (lazy developer!) :)", ext), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }
            }

            if (!opRes.Success)
                MsgBox.Show(opRes.CustomErrorMessage, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void PMainContainer_Paint(object sender, PaintEventArgs e)
        {
            var waterMarkText = string.Format("Drag a subtitle here to convert it's diacritics{0}Drag a folder to get the files details", Environment.NewLine);
            const int opacity = 35;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            var panel = (Panel)sender;

            using (Font font = new Font("Arial", 14.0f))
            {
                SizeF textSize = g.MeasureString(waterMarkText, font);

                var pt = new Point(
                    panel.Size.Width - (int)textSize.Width - 10,
                    panel.Size.Height - (int)textSize.Height - 10);


                using (var brush = new SolidBrush(Color.FromArgb(opacity, 0, 0, 0)))
                {
                    g.DrawString(waterMarkText, font, brush, pt);
                }
            }
        }

        private void PMainContainer_Resize(object sender, EventArgs e)
        {
            pMainContainer.Refresh();
        }

        private void btnGenerateCatalog_Click(object sender, EventArgs e)
        {
            var genParams = new FrmPDFCatalogGenParams() { Owner = this };
            if (genParams.ShowDialog() != DialogResult.OK)
                return;

            var opRes = PdfGenerator.CreateCatalog(genParams.PdfGenParams);
            if (!opRes.Success)
            {
                MsgBox.Show(string.Format("The following error occurred while creating the catalog (1):{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Helpers.ShowToastForm(StartPosition2.BottomRight, MessageType.Information, "PDF Catalog Generation",
                    string.Format("The catalog has been succesfully created and saved in '{0}'", genParams.PdfGenParams.FileName), 10000, this);
            }
        }

        private void FrmMain_ResizeBegin(object sender, EventArgs e)
        {
            DrawingControl.SuspendDrawing(pMainContainer);
        }

        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            DrawingControl.ResumeDrawing(pMainContainer);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                var seriesList = DAL.GetMoviesForPDF(new PdfGenParams { ForMovies = false, PDFGenType = PDFGenType.All });
                //var episodesAudioLanguages = new List<string>();
                //var audios = "?";

                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var sqlString = @"
                            UPDATE FileDetail
                               SET AudioLanguages = '{0}'
                             WHERE Id = {1}";

                    SqlCeCommand cmd;

                    foreach (var series in seriesList)
                    {
                        cmd = new SqlCeCommand(string.Format(sqlString, series.A, series.Id), conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                */

                //var xxx = DAL.GetMoviesDetails2ForWeb();
                //var x= 1;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckFiles_Click(object sender, EventArgs e)
        {
            var selectedPath = Helpers.SelectFolder("Please select the movies location (folder)", _iniFile.ReadString("LastPath", "General"));
            if (string.IsNullOrEmpty(selectedPath))
                return;

            _iniFile.Write("LastPath", Path.GetFullPath(selectedPath), "General");

            var d = new DirectoryInfo(selectedPath);
            var files = d.GetFiles("*.mkv");

            var movies = DAL.GetMoviesGridData("FileName COLLATE NOCASE ASC", "");

            var titlesWithoutFiles = new List<string>();
            foreach (var movie in movies)
            {
                if (files.Any(_ => _.Name.Contains(movie.FileName)))
                {
                    continue;
                }

                titlesWithoutFiles.Add(movie.FileName);
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save combined names as ...";
                saveDialog.Filter = "Text files (*.txt)|*.txt";
                saveDialog.FileName = "_#_titlesWithoutFiles";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (var sw = new StreamWriter(saveDialog.FileName, false, Encoding.Unicode))
                    {
                        foreach (var s in titlesWithoutFiles)
                            sw.WriteLine(s);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(OperationResult.GetErrorMessage(ex), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImportSynopsis_Click(object sender, EventArgs e)
        {
            var dlgResult =
                MsgBox.Show("Do you want to preserve existing data?", "Confirmation", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

            if (dlgResult == DialogResult.Cancel) return;

            var opRes = WebScraping.ImportSynopsis(dlgResult == DialogResult.Yes);

            var importErrors = (List<TechnicalDetailsImportError>)opRes.AdditionalDataReturn;

            if (importErrors.Any())
            {
                var frmIE = new FrmImportErrors(importErrors, true);
                frmIE.ShowDialog();
            }

            if (_ucMovies != null)
                _ucMovies.ReloadData(true);
        }

        private void miImportCommonSenseMediaData_Click(object sender, EventArgs e)
        {
            var dlgResult =
                MsgBox.Show("Do you want to preserve existing data?", ("Import CommonSenseMedia for " + (miMovies.Checked ? "movies" : "series")), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

            if (dlgResult == DialogResult.Cancel) return;

            var opRes = WebScraping.ImportCommonSenseMediaData(miMovies.Checked, dlgResult == DialogResult.Yes);

            if (!opRes.Success)
            {
                MsgBox.Show(string.Format("The following error occurred attempting to retrieve the CommonSenseMedia data:{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Done");
            }
        }

        private void miSQLmanagement_Click(object sender, EventArgs e)
        {
            if (!Utils.Helpers.ConfirmDiscardChanges())
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                senderItem.Checked = !senderItem.Checked;

                pMainContainer.Controls.Clear();

                if (senderItem.Checked)
                {
                    pMainContainer.Controls.Add(new ucSQLmanagement(this) { Dock = DockStyle.Fill });
                }
                else
                {
                }

                //todo: check if necessary
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                DAL.EpisodeParentType = EpisodeParentType.Series;
                DrawingControl.ResumeDrawing(pMainContainer);
                Cursor = Cursors.Default;
            }
        }

        private void miOptions_Click(object sender, EventArgs e)
        {
            var frmOptions = new FrmOptions() { Owner = this };
            frmOptions.ShowDialog();
        }



        //public Encoding GetEncoding(string filename)
        //{
        //    // Read the BOM
        //    var bom = new byte[4];
        //    using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
        //    {
        //        file.Read(bom, 0, 4);
        //    }

        //    // Analyze the BOM
        //    if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
        //    if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
        //    if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
        //    if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
        //    if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
        //    return Encoding.ASCII;
        //}
    }
}