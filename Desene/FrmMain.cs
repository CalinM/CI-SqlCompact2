using Common;
using Desene.DetailFormsAndUserControls.Movies;
using Desene.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

using Common.ExtensionMethods;

using DAL;

using Utils;
using Helpers = Utils.Helpers;

namespace Desene
{
    public partial class FrmMain : Form
    {
        public event EventHandler OnAddButtonPress;
        public event EventHandler OnDeleteButtonPress;
        public event EventHandler OnCloseModule;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
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

            pMainContainer.Controls.Clear();

            DAL.LoadBaseDbValues();

            LoadMainWindowConfig();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Helpers.ConfirmDiscardChanges();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Copy window location to app settings
            Settings.Default.WindowLocation = Location;

            // Copy window size to app settings
            Settings.Default.WindowSize = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;

            // Save settings
            Settings.Default.Save();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (!Helpers.ConfirmDiscardChanges())
                return;

            Close();
        }

        private void LoadMainWindowConfig()
        {
            if (Settings.Default.WindowLocation.X > -100 && Settings.Default.WindowLocation.Y > -100) //to auto-correct bad configuration
            {
                Location = Settings.Default.WindowLocation;
            }

            // Set window size
            if (Settings.Default.WindowSize != null)
            {
                Size = Settings.Default.WindowSize;
            }

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            var buildDate = new DateTime(2010, 1, 1).AddDays(version.Revision);
            Text = "Movies Indexer v"+ string.Format("{0} (build date {1})", version, buildDate.ToString("dd.MM.yyyy"));
        }

        private void miMovies_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);
                    GetStatistics(true, false);

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucMovies(this) { Dock = DockStyle.Fill });
                }
                else
                {
                    if (!(OnCloseModule is null))
                        OnCloseModule(sender, e);

                    senderItem.Checked = false;

                    pMainContainer.Controls.Clear();

                    GetStatistics(false, false);
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
        private void miSeries_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                var senderItem = (ToolStripMenuItem)sender;
                if (!senderItem.Checked)
                {
                    MarkCurrentCategory(senderItem);

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucSeries(this) { Dock = DockStyle.Fill });
                }
                else
                {
                    senderItem.Checked = false;
                    pMainContainer.Controls.Clear();
                }

                SetMainCrudButtonsState(senderItem.Checked, "Add series");

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

        private void btnMoviesList_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DrawingControl.SuspendDrawing(pMainContainer);
                ClearAllDelegates();

                pMainContainer.Controls.Clear();
                pMainContainer.Controls.Add(new ucMoviesList(this) { Dock = DockStyle.Fill });

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

        private void GetStatistics(bool show, bool forSeries)
        {
            if (show)
            {
                var opRes = DAL.GetStatistics(forSeries);

                sslbStatistics.Text =
                    opRes.Success
                        ? (string)opRes.AdditionalDataReturn
                        : opRes.CustomErrorMessage;

                var moviesInList = (string)opRes.AdditionalDataReturn != "There are no movies in the list ...";

                sslbClick.Visible = !forSeries && moviesInList;
                sslbAdditionalInfo1.Visible = !forSeries && moviesInList;
                sslbAdditionalInfo2.Visible = !forSeries && moviesInList;
            }

            sslbStatistics.Visible = show;
        }

        public void SetStatistics(bool show, string text)
        {
            sslbStatistics.Visible = show;
            sslbStatistics.Text = text;
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
            MsgBox.Show(
                string.Format(
                    "{0}{1}{1}{2}",
                    DAL.SectionDetails,
                    Environment.NewLine,
                    "Warning! The list only refreshes when a section is shown!"),
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //, new Font("Courier New", 8, FontStyle.Regular)
        }










        private void button1_Click(object sender, EventArgs e)
        {
            var opRes = OldDataMigration.ImportFilmeHD();

            if (!opRes.Success)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while importing data (1):{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            MessageBox.Show("Done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var opRes = OldDataMigration.ImportSeriale();
            if (!opRes.Success)
            {
                MessageBox.Show(
                    string.Format("The following error occurred while importing data (2):{0}{0}{1}", Environment.NewLine, opRes.CustomErrorMessage),
                    @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            MessageBox.Show("Done");
        }

        private void button4_Click(object sender, EventArgs e)
        {
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
                        var fileId = (int)reader["Id"];
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
                        var x =1;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            MsgBox.Show("desc", "title", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1,
                new Font("Times New Roman", 15, FontStyle.Bold) );
        }

        private void btnGenerateHtml_Click(object sender, EventArgs e)
        {
            var genParams = new FrmSiteGenParams(Settings.Default.LastPath) { Owner = this };

            if (genParams.ShowDialog() != DialogResult.OK)
                return;

            Settings.Default.LastPath = Path.GetFullPath(genParams.SiteGenParams.Location);
            Settings.Default.Save();

            
            var opRes = SiteGenerator.GenerateSiteFiles(genParams.SiteGenParams);

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

            #endregion

            var genUniqueId = string.Empty;//DateTime.Now.ToString("yyyyMMddhhmmss");

            #region Site scripts

            var serializedData = (KeyValuePair<string, string>)opRes.AdditionalDataReturn;

            var scriptPath = Path.Combine(genParams.SiteGenParams.Location, "Scripts");
            if (!Directory.Exists(scriptPath))
                Directory.CreateDirectory(scriptPath);

            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\detaliiFilme_{genUniqueId}.js"), serializedData.Key);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, $"Scripts\\detaliiSeriale_{genUniqueId}.js"), serializedData.Value);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery-2.2.4.min.js"), Resources.jquery_2_2_4_min);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\desene.js"), Resources.deseneJS);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.lazy.min.js"), Resources.jquery_lazy_min);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jquery.slimscroll.min.js"), Resources.jquery_slimscroll_min);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\jsgrid.min.js"), Resources.jsgrid_minJS);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Scripts\\YouTubePopUp.jquery.js"), Resources.YouTubePopUp_jquery);

            #endregion

            #region Site styles

            var stylesPath = Path.Combine(genParams.SiteGenParams.Location, "Styles");
            if (!Directory.Exists(stylesPath))
                Directory.CreateDirectory(stylesPath);

            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\desene.css"), Resources.deseneCSS);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\jsgrid-theme.min.css"), Resources.jsgrid_theme_min);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\jsgrid.min.css"), Resources.jsgrid_minCSS);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\sections.css"), Resources.sections);
            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "Styles\\YouTubePopUp.css"), Resources.YouTubePopUp);

            #endregion

            File.WriteAllText(Path.Combine(genParams.SiteGenParams.Location, "index.html"), Resources.index.Replace("##", genUniqueId));

            FlashWindow.Flash(this, 5);

            if (MsgBox.Show("The new site files have been saved! Do you want to open the folder location?", "Info",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process.Start(genParams.SiteGenParams.Location);
            }
        }

        private void btnFilesDetails_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Please select the files location";
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.SelectedPath = Settings.Default.LastPath;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.LastPath = folderBrowserDialog.SelectedPath;
                    Settings.Default.Save();

                   var files =
                        Directory.EnumerateFiles(folderBrowserDialog.SelectedPath, "*.*", SearchOption.AllDirectories)
                            .Where(s => s.EndsWith(".mkv") || s.EndsWith(".mp4")).ToArray();

                        Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*");

                    if (!files.ToList().DistinctBy(Path.GetExtension).Any())
                        MsgBox.Show("The folder is empty!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        var param = new FilesImportParams
                                        {
                                            Location = folderBrowserDialog.SelectedPath,
                                            FilesExtension = "*.*",
                                            DisplayInfoOnly = true
                                        };

                        var opRes = FilesMetaData.GetFilesTechnicalDetails(files, param);

                        if (opRes.AdditionalDataReturn == null)
                        {
                            MsgBox.Show("There was an issue and no files details were retrieved!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var filesInfoDeterminationResult = (KeyValuePair<List<MovieTechnicalDetails>, List<TechnicalDetailsImportError>>)opRes.AdditionalDataReturn;
                        var displayResult = new List<dynamic>();

                        foreach (var file in files)
                        {
                            var fileInfoObj = filesInfoDeterminationResult.Key.FirstOrDefault(f => f.InitialPath == file);
                            var dynamicObj = new ExpandoObject() as IDictionary<string, object>;
                            dynamicObj.Add("Filename", Path.GetFileName(file));
                            dynamicObj.Add("FileVideo Title", fileInfoObj.HasTitle || fileInfoObj.VideoStreams.Any(vs => vs.HasTitle));

                            if (fileInfoObj != null)
                            {
                                foreach (var audioObj in fileInfoObj.AudioStreams)
                                {
                                    dynamicObj.Add(string.Format("Audio {0}", audioObj.Index), audioObj.Language);
                                    dynamicObj.Add(string.Format("Default {0}", audioObj.Index), audioObj.Default);
                                    dynamicObj.Add(string.Format("Forced {0}", audioObj.Index), audioObj.Forced);
                                    dynamicObj.Add(string.Format("Title {0}", audioObj.Index), audioObj.HasTitle);
                                }

                                dynamicObj.Add("Error", "");
                            }
                            else
                            {
                                var fileErrorObj = filesInfoDeterminationResult.Value.FirstOrDefault(f => f.FilePath == file);
                                dynamicObj.Add("Error", fileInfoObj != null ? fileErrorObj.ErrorMesage : "Unknown error!");
                            }

                            dynamicObj.Add("Cover", !string.IsNullOrEmpty(fileInfoObj.Cover));

                            displayResult.Add(dynamicObj);
                        }

                        if (!displayResult.Any())
                        {
                            MsgBox.Show("Something went wrong while processing the files details!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var frmFileDetails = new FrmFileDetails(displayResult)  { Owner = this };
                        frmFileDetails.Show();
                    }
                }
            }
        }
    }
}