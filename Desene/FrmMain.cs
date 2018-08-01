using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Desene.Properties;

using Utils;

namespace Desene
{
    public partial class FrmMain : Form
    {
        public event EventHandler OnAddButtonPress;
        public event EventHandler OnDeleteButtonPress;

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

                    pMainContainer.Controls.Clear();
                    pMainContainer.Controls.Add(new ucMovies(this) { Dock = DockStyle.Fill });
                }
                else
                {
                    senderItem.Checked = false;
                    pMainContainer.Controls.Clear();
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




        //todo:
        //https://stackoverflow.com/questions/826777/how-to-have-an-auto-incrementing-version-number-visual-studio











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
            var filesPath = string.Empty;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
                    return;


                filesPath = folderBrowserDialog.SelectedPath;
            }

            var shortMoviesData = DAL.GetMoviesGridData();
            var filesWithIssues = new List<string>();
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
            var x = 1;
        }
    }
}