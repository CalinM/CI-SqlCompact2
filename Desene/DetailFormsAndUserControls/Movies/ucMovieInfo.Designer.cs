namespace Desene.DetailFormsAndUserControls
{
    partial class ucMovieInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pMovieDetail = new System.Windows.Forms.Panel();
            this.bRefreshCSMData = new Utils.UnselectableButton();
            this.cbGrabCSMData = new System.Windows.Forms.CheckBox();
            this.pbDbDates = new System.Windows.Forms.PictureBox();
            this.pbLoader = new System.Windows.Forms.PictureBox();
            this.tbSynopsis = new Utils.CustomTextBox();
            this.lbSynopsis = new System.Windows.Forms.Label();
            this.bGotoDescription = new Utils.UnselectableButton();
            this.bGotoTrailer = new Utils.UnselectableButton();
            this.bGotoRecommendedSite = new Utils.UnselectableButton();
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.lbTrailer = new System.Windows.Forms.Label();
            this.tbTrailer = new Utils.CustomTextBox();
            this.tlpMovieStillsWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.pbMovieStill3 = new System.Windows.Forms.PictureBox();
            this.pbMovieStill2 = new System.Windows.Forms.PictureBox();
            this.pbMovieStill1 = new System.Windows.Forms.PictureBox();
            this.chbTitle = new System.Windows.Forms.CheckBox();
            this.tbSize = new Utils.ButtonEdit();
            this.tbmDuration = new System.Windows.Forms.MaskedTextBox();
            this.tbSubtitleSummary = new Utils.CustomTextBox();
            this.tbAudioSummary = new Utils.CustomTextBox();
            this.lbAudioSummary = new System.Windows.Forms.Label();
            this.lbSizeDuration = new System.Windows.Forms.Label();
            this.tbEncodedWith = new Utils.CustomTextBox();
            this.tbFormat = new Utils.CustomTextBox();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbStreamLink = new System.Windows.Forms.Label();
            this.tbStreamLink = new Utils.CustomTextBox();
            this.cbTheme = new System.Windows.Forms.ComboBox();
            this.tbYear = new Utils.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNotes = new Utils.CustomTextBox();
            this.tbRecommendedLink = new Utils.CustomTextBox();
            this.tbRecommended = new Utils.CustomTextBox();
            this.lbRecommended = new System.Windows.Forms.Label();
            this.tbDescriptionLink = new Utils.CustomTextBox();
            this.lbDescriptionLink = new System.Windows.Forms.Label();
            this.tbTitle = new Utils.CustomTextBox();
            this.lbSeriesTitle = new System.Windows.Forms.Label();
            this.lbNotes = new System.Windows.Forms.Label();
            this.tbDummyForFocus = new System.Windows.Forms.TextBox();
            this.tbSizeAsInt = new System.Windows.Forms.TextBox();
            this.ttTitleContent = new System.Windows.Forms.ToolTip(this.components);
            this.ttMovieDbDates = new System.Windows.Forms.ToolTip(this.components);
            this.pMovieDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDbDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).BeginInit();
            this.tlpMovieStillsWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill1)).BeginInit();
            this.SuspendLayout();
            // 
            // pMovieDetail
            // 
            this.pMovieDetail.AllowDrop = true;
            this.pMovieDetail.Controls.Add(this.bRefreshCSMData);
            this.pMovieDetail.Controls.Add(this.cbGrabCSMData);
            this.pMovieDetail.Controls.Add(this.pbDbDates);
            this.pMovieDetail.Controls.Add(this.pbLoader);
            this.pMovieDetail.Controls.Add(this.tbSynopsis);
            this.pMovieDetail.Controls.Add(this.lbSynopsis);
            this.pMovieDetail.Controls.Add(this.bGotoDescription);
            this.pMovieDetail.Controls.Add(this.bGotoTrailer);
            this.pMovieDetail.Controls.Add(this.bGotoRecommendedSite);
            this.pMovieDetail.Controls.Add(this.pbCover);
            this.pMovieDetail.Controls.Add(this.cbQuality);
            this.pMovieDetail.Controls.Add(this.lbTrailer);
            this.pMovieDetail.Controls.Add(this.tbTrailer);
            this.pMovieDetail.Controls.Add(this.tlpMovieStillsWrapper);
            this.pMovieDetail.Controls.Add(this.chbTitle);
            this.pMovieDetail.Controls.Add(this.tbSize);
            this.pMovieDetail.Controls.Add(this.tbmDuration);
            this.pMovieDetail.Controls.Add(this.tbSubtitleSummary);
            this.pMovieDetail.Controls.Add(this.tbAudioSummary);
            this.pMovieDetail.Controls.Add(this.lbAudioSummary);
            this.pMovieDetail.Controls.Add(this.lbSizeDuration);
            this.pMovieDetail.Controls.Add(this.tbEncodedWith);
            this.pMovieDetail.Controls.Add(this.tbFormat);
            this.pMovieDetail.Controls.Add(this.lbFormat);
            this.pMovieDetail.Controls.Add(this.lbStreamLink);
            this.pMovieDetail.Controls.Add(this.tbStreamLink);
            this.pMovieDetail.Controls.Add(this.cbTheme);
            this.pMovieDetail.Controls.Add(this.tbYear);
            this.pMovieDetail.Controls.Add(this.label1);
            this.pMovieDetail.Controls.Add(this.tbNotes);
            this.pMovieDetail.Controls.Add(this.tbRecommendedLink);
            this.pMovieDetail.Controls.Add(this.tbRecommended);
            this.pMovieDetail.Controls.Add(this.lbRecommended);
            this.pMovieDetail.Controls.Add(this.tbDescriptionLink);
            this.pMovieDetail.Controls.Add(this.lbDescriptionLink);
            this.pMovieDetail.Controls.Add(this.tbTitle);
            this.pMovieDetail.Controls.Add(this.lbSeriesTitle);
            this.pMovieDetail.Controls.Add(this.lbNotes);
            this.pMovieDetail.Controls.Add(this.tbDummyForFocus);
            this.pMovieDetail.Controls.Add(this.tbSizeAsInt);
            this.pMovieDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMovieDetail.Location = new System.Drawing.Point(0, 0);
            this.pMovieDetail.Name = "pMovieDetail";
            this.pMovieDetail.Size = new System.Drawing.Size(830, 567);
            this.pMovieDetail.TabIndex = 1;
            this.pMovieDetail.DragDrop += new System.Windows.Forms.DragEventHandler(this.PMovieDetail_DragDrop);
            this.pMovieDetail.DragEnter += new System.Windows.Forms.DragEventHandler(this.PMovieDetail_DragEnter);
            // 
            // bRefreshCSMData
            // 
            this.bRefreshCSMData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRefreshCSMData.BackgroundImage = global::Desene.Properties.Resources.Plugin;
            this.bRefreshCSMData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bRefreshCSMData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRefreshCSMData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bRefreshCSMData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bRefreshCSMData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bRefreshCSMData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRefreshCSMData.Location = new System.Drawing.Point(526, 151);
            this.bRefreshCSMData.Name = "bRefreshCSMData";
            this.bRefreshCSMData.Size = new System.Drawing.Size(20, 20);
            this.bRefreshCSMData.TabIndex = 299;
            this.bRefreshCSMData.TabStop = false;
            this.ttTitleContent.SetToolTip(this.bRefreshCSMData, "Navigate using the default system browser to the current CommonSenseMedia link");
            this.bRefreshCSMData.UseVisualStyleBackColor = true;
            this.bRefreshCSMData.Click += new System.EventHandler(this.bRefreshCSMData_Click);
            // 
            // cbGrabCSMData
            // 
            this.cbGrabCSMData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGrabCSMData.AutoSize = true;
            this.cbGrabCSMData.Checked = true;
            this.cbGrabCSMData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGrabCSMData.Location = new System.Drawing.Point(529, 155);
            this.cbGrabCSMData.Name = "cbGrabCSMData";
            this.cbGrabCSMData.Size = new System.Drawing.Size(15, 14);
            this.cbGrabCSMData.TabIndex = 297;
            this.cbGrabCSMData.UseVisualStyleBackColor = true;
            // 
            // pbDbDates
            // 
            this.pbDbDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDbDates.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbDbDates.Image = global::Desene.Properties.Resources.calender_icon;
            this.pbDbDates.InitialImage = global::Desene.Properties.Resources.calender_icon;
            this.pbDbDates.Location = new System.Drawing.Point(551, 208);
            this.pbDbDates.Name = "pbDbDates";
            this.pbDbDates.Size = new System.Drawing.Size(15, 16);
            this.pbDbDates.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDbDates.TabIndex = 296;
            this.pbDbDates.TabStop = false;
            // 
            // pbLoader
            // 
            this.pbLoader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoader.BackColor = System.Drawing.SystemColors.Window;
            this.pbLoader.Image = global::Desene.Properties.Resources.preloader;
            this.pbLoader.InitialImage = global::Desene.Properties.Resources.preloader;
            this.pbLoader.Location = new System.Drawing.Point(511, 113);
            this.pbLoader.Name = "pbLoader";
            this.pbLoader.Size = new System.Drawing.Size(35, 30);
            this.pbLoader.TabIndex = 294;
            this.pbLoader.TabStop = false;
            this.pbLoader.Visible = false;
            // 
            // tbSynopsis
            // 
            this.tbSynopsis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSynopsis.Location = new System.Drawing.Point(129, 76);
            this.tbSynopsis.Multiline = true;
            this.tbSynopsis.Name = "tbSynopsis";
            this.tbSynopsis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSynopsis.Size = new System.Drawing.Size(438, 69);
            this.tbSynopsis.TabIndex = 293;
            // 
            // lbSynopsis
            // 
            this.lbSynopsis.AutoSize = true;
            this.lbSynopsis.Location = new System.Drawing.Point(27, 79);
            this.lbSynopsis.Name = "lbSynopsis";
            this.lbSynopsis.Size = new System.Drawing.Size(52, 13);
            this.lbSynopsis.TabIndex = 292;
            this.lbSynopsis.Text = "Synopsis:";
            // 
            // bGotoDescription
            // 
            this.bGotoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGotoDescription.BackgroundImage = global::Desene.Properties.Resources.external_icon;
            this.bGotoDescription.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bGotoDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bGotoDescription.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bGotoDescription.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bGotoDescription.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bGotoDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGotoDescription.Location = new System.Drawing.Point(547, 50);
            this.bGotoDescription.Name = "bGotoDescription";
            this.bGotoDescription.Size = new System.Drawing.Size(20, 20);
            this.bGotoDescription.TabIndex = 291;
            this.bGotoDescription.TabStop = false;
            this.bGotoDescription.UseVisualStyleBackColor = true;
            this.bGotoDescription.Click += new System.EventHandler(this.bGotoDescription_Click);
            // 
            // bGotoTrailer
            // 
            this.bGotoTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGotoTrailer.BackgroundImage = global::Desene.Properties.Resources.external_icon;
            this.bGotoTrailer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bGotoTrailer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bGotoTrailer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bGotoTrailer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bGotoTrailer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bGotoTrailer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGotoTrailer.Location = new System.Drawing.Point(547, 296);
            this.bGotoTrailer.Name = "bGotoTrailer";
            this.bGotoTrailer.Size = new System.Drawing.Size(20, 20);
            this.bGotoTrailer.TabIndex = 290;
            this.bGotoTrailer.TabStop = false;
            this.bGotoTrailer.UseVisualStyleBackColor = true;
            this.bGotoTrailer.Click += new System.EventHandler(this.bGotoTrailer_Click);
            // 
            // bGotoRecommendedSite
            // 
            this.bGotoRecommendedSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGotoRecommendedSite.BackgroundImage = global::Desene.Properties.Resources.external_icon;
            this.bGotoRecommendedSite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bGotoRecommendedSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bGotoRecommendedSite.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bGotoRecommendedSite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.bGotoRecommendedSite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.bGotoRecommendedSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGotoRecommendedSite.Location = new System.Drawing.Point(547, 151);
            this.bGotoRecommendedSite.Name = "bGotoRecommendedSite";
            this.bGotoRecommendedSite.Size = new System.Drawing.Size(20, 20);
            this.bGotoRecommendedSite.TabIndex = 289;
            this.bGotoRecommendedSite.TabStop = false;
            this.bGotoRecommendedSite.UseVisualStyleBackColor = true;
            this.bGotoRecommendedSite.Click += new System.EventHandler(this.bGotoRecommendedSite_Click);
            // 
            // pbCover
            // 
            this.pbCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCover.Location = new System.Drawing.Point(591, 24);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(212, 318);
            this.pbCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCover.TabIndex = 267;
            this.pbCover.TabStop = false;
            // 
            // cbQuality
            // 
            this.cbQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Location = new System.Drawing.Point(497, 24);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(70, 21);
            this.cbQuality.TabIndex = 255;
            // 
            // lbTrailer
            // 
            this.lbTrailer.AutoSize = true;
            this.lbTrailer.Location = new System.Drawing.Point(27, 297);
            this.lbTrailer.Name = "lbTrailer";
            this.lbTrailer.Size = new System.Drawing.Size(39, 13);
            this.lbTrailer.TabIndex = 286;
            this.lbTrailer.Text = "Trailer:";
            // 
            // tbTrailer
            // 
            this.tbTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTrailer.Location = new System.Drawing.Point(129, 296);
            this.tbTrailer.Name = "tbTrailer";
            this.tbTrailer.Size = new System.Drawing.Size(415, 20);
            this.tbTrailer.TabIndex = 9;
            // 
            // tlpMovieStillsWrapper
            // 
            this.tlpMovieStillsWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMovieStillsWrapper.ColumnCount = 3;
            this.tlpMovieStillsWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpMovieStillsWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpMovieStillsWrapper.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpMovieStillsWrapper.Controls.Add(this.pbMovieStill3, 0, 0);
            this.tlpMovieStillsWrapper.Controls.Add(this.pbMovieStill2, 0, 0);
            this.tlpMovieStillsWrapper.Controls.Add(this.pbMovieStill1, 0, 0);
            this.tlpMovieStillsWrapper.Location = new System.Drawing.Point(30, 371);
            this.tlpMovieStillsWrapper.Name = "tlpMovieStillsWrapper";
            this.tlpMovieStillsWrapper.RowCount = 1;
            this.tlpMovieStillsWrapper.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMovieStillsWrapper.Size = new System.Drawing.Size(773, 174);
            this.tlpMovieStillsWrapper.TabIndex = 283;
            // 
            // pbMovieStill3
            // 
            this.pbMovieStill3.BackColor = System.Drawing.Color.Black;
            this.pbMovieStill3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMovieStill3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMovieStill3.Location = new System.Drawing.Point(520, 3);
            this.pbMovieStill3.Name = "pbMovieStill3";
            this.pbMovieStill3.Size = new System.Drawing.Size(250, 168);
            this.pbMovieStill3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMovieStill3.TabIndex = 196;
            this.pbMovieStill3.TabStop = false;
            // 
            // pbMovieStill2
            // 
            this.pbMovieStill2.BackColor = System.Drawing.Color.Black;
            this.pbMovieStill2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMovieStill2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMovieStill2.Location = new System.Drawing.Point(258, 3);
            this.pbMovieStill2.Name = "pbMovieStill2";
            this.pbMovieStill2.Size = new System.Drawing.Size(256, 168);
            this.pbMovieStill2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMovieStill2.TabIndex = 195;
            this.pbMovieStill2.TabStop = false;
            // 
            // pbMovieStill1
            // 
            this.pbMovieStill1.BackColor = System.Drawing.Color.Black;
            this.pbMovieStill1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMovieStill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMovieStill1.Location = new System.Drawing.Point(3, 3);
            this.pbMovieStill1.Name = "pbMovieStill1";
            this.pbMovieStill1.Size = new System.Drawing.Size(249, 168);
            this.pbMovieStill1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMovieStill1.TabIndex = 194;
            this.pbMovieStill1.TabStop = false;
            // 
            // chbTitle
            // 
            this.chbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbTitle.AutoSize = true;
            this.chbTitle.Location = new System.Drawing.Point(552, 232);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(15, 14);
            this.chbTitle.TabIndex = 282;
            this.chbTitle.UseVisualStyleBackColor = true;
            this.chbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTitle_MouseClick);
            // 
            // tbSize
            // 
            this.tbSize.ButtonCursor = System.Windows.Forms.Cursors.Help;
            this.tbSize.ButtonImage = global::Desene.Properties.Resources.warning;
            this.tbSize.ButtonImageForceWidth = 12;
            this.tbSize.ButtonImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tbSize.ButtonToolTip = "Size mismatch!";
            this.tbSize.ButtonVisible = false;
            this.tbSize.Location = new System.Drawing.Point(129, 203);
            this.tbSize.Name = "tbSize";
            this.tbSize.ReadOnly = true;
            this.tbSize.Size = new System.Drawing.Size(50, 20);
            this.tbSize.TabIndex = 6;
            this.tbSize.TabStop = false;
            this.tbSize.TextChanged += new System.EventHandler(this.tbSize_TextChanged);
            // 
            // tbmDuration
            // 
            this.tbmDuration.HideSelection = false;
            this.tbmDuration.Location = new System.Drawing.Point(185, 203);
            this.tbmDuration.Mask = "00:00:00";
            this.tbmDuration.Name = "tbmDuration";
            this.tbmDuration.ReadOnly = true;
            this.tbmDuration.Size = new System.Drawing.Size(50, 20);
            this.tbmDuration.TabIndex = 7;
            this.tbmDuration.TabStop = false;
            this.tbmDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbmDuration.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.tbmDuration_TypeValidationCompleted);
            this.tbmDuration.TextChanged += new System.EventHandler(this.tbmDuration_Enter);
            this.tbmDuration.Enter += new System.EventHandler(this.tbmDuration_Enter);
            this.tbmDuration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbmDuration_KeyUp);
            this.tbmDuration.Leave += new System.EventHandler(this.tbmDuration_Leave);
            // 
            // tbSubtitleSummary
            // 
            this.tbSubtitleSummary.Location = new System.Drawing.Point(185, 229);
            this.tbSubtitleSummary.Name = "tbSubtitleSummary";
            this.tbSubtitleSummary.ReadOnly = true;
            this.tbSubtitleSummary.Size = new System.Drawing.Size(50, 20);
            this.tbSubtitleSummary.TabIndex = 277;
            this.tbSubtitleSummary.TabStop = false;
            // 
            // tbAudioSummary
            // 
            this.tbAudioSummary.Location = new System.Drawing.Point(129, 229);
            this.tbAudioSummary.Name = "tbAudioSummary";
            this.tbAudioSummary.ReadOnly = true;
            this.tbAudioSummary.Size = new System.Drawing.Size(50, 20);
            this.tbAudioSummary.TabIndex = 276;
            this.tbAudioSummary.TabStop = false;
            // 
            // lbAudioSummary
            // 
            this.lbAudioSummary.AutoSize = true;
            this.lbAudioSummary.Location = new System.Drawing.Point(27, 232);
            this.lbAudioSummary.Name = "lbAudioSummary";
            this.lbAudioSummary.Size = new System.Drawing.Size(88, 13);
            this.lbAudioSummary.TabIndex = 279;
            this.lbAudioSummary.Text = "Audio / Subtitles:";
            // 
            // lbSizeDuration
            // 
            this.lbSizeDuration.AutoSize = true;
            this.lbSizeDuration.Location = new System.Drawing.Point(27, 206);
            this.lbSizeDuration.Name = "lbSizeDuration";
            this.lbSizeDuration.Size = new System.Drawing.Size(81, 13);
            this.lbSizeDuration.TabIndex = 278;
            this.lbSizeDuration.Text = "Size / Duration:";
            // 
            // tbEncodedWith
            // 
            this.tbEncodedWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEncodedWith.Location = new System.Drawing.Point(241, 322);
            this.tbEncodedWith.Name = "tbEncodedWith";
            this.tbEncodedWith.Size = new System.Drawing.Size(326, 20);
            this.tbEncodedWith.TabIndex = 12;
            // 
            // tbFormat
            // 
            this.tbFormat.Location = new System.Drawing.Point(129, 322);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(106, 20);
            this.tbFormat.TabIndex = 11;
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(27, 325);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(96, 13);
            this.lbFormat.TabIndex = 272;
            this.lbFormat.Text = "Format / Encoded:";
            // 
            // lbStreamLink
            // 
            this.lbStreamLink.AutoSize = true;
            this.lbStreamLink.Location = new System.Drawing.Point(112, 348);
            this.lbStreamLink.Name = "lbStreamLink";
            this.lbStreamLink.Size = new System.Drawing.Size(62, 13);
            this.lbStreamLink.TabIndex = 269;
            this.lbStreamLink.Text = "Stream link:";
            this.lbStreamLink.Visible = false;
            // 
            // tbStreamLink
            // 
            this.tbStreamLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStreamLink.Location = new System.Drawing.Point(217, 345);
            this.tbStreamLink.Name = "tbStreamLink";
            this.tbStreamLink.Size = new System.Drawing.Size(65, 20);
            this.tbStreamLink.TabIndex = 10;
            this.tbStreamLink.Visible = false;
            // 
            // cbTheme
            // 
            this.cbTheme.FormattingEnabled = true;
            this.cbTheme.Location = new System.Drawing.Point(185, 176);
            this.cbTheme.Name = "cbTheme";
            this.cbTheme.Size = new System.Drawing.Size(190, 21);
            this.cbTheme.TabIndex = 5;
            this.cbTheme.SelectionChangeCommitted += new System.EventHandler(this.cbTheme_SelectionChangeCommitted);
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(129, 177);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(50, 20);
            this.tbYear.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 268;
            this.label1.Text = "Year / Theme:";
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(129, 255);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbNotes.Size = new System.Drawing.Size(438, 35);
            this.tbNotes.TabIndex = 8;
            // 
            // tbRecommendedLink
            // 
            this.tbRecommendedLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecommendedLink.Location = new System.Drawing.Point(185, 151);
            this.tbRecommendedLink.Name = "tbRecommendedLink";
            this.tbRecommendedLink.Size = new System.Drawing.Size(339, 20);
            this.tbRecommendedLink.TabIndex = 3;
            // 
            // tbRecommended
            // 
            this.tbRecommended.Location = new System.Drawing.Point(129, 151);
            this.tbRecommended.Name = "tbRecommended";
            this.tbRecommended.Size = new System.Drawing.Size(50, 20);
            this.tbRecommended.TabIndex = 2;
            // 
            // lbRecommended
            // 
            this.lbRecommended.AutoSize = true;
            this.lbRecommended.Location = new System.Drawing.Point(27, 154);
            this.lbRecommended.Name = "lbRecommended";
            this.lbRecommended.Size = new System.Drawing.Size(82, 13);
            this.lbRecommended.TabIndex = 266;
            this.lbRecommended.Text = "Recommended:";
            // 
            // tbDescriptionLink
            // 
            this.tbDescriptionLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescriptionLink.Location = new System.Drawing.Point(129, 50);
            this.tbDescriptionLink.Name = "tbDescriptionLink";
            this.tbDescriptionLink.Size = new System.Drawing.Size(415, 20);
            this.tbDescriptionLink.TabIndex = 1;
            this.tbDescriptionLink.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDescriptionLink_KeyPress);
            this.tbDescriptionLink.Leave += new System.EventHandler(this.tbDescriptionLink_Leave);
            // 
            // lbDescriptionLink
            // 
            this.lbDescriptionLink.AutoSize = true;
            this.lbDescriptionLink.Location = new System.Drawing.Point(27, 53);
            this.lbDescriptionLink.Name = "lbDescriptionLink";
            this.lbDescriptionLink.Size = new System.Drawing.Size(82, 13);
            this.lbDescriptionLink.TabIndex = 265;
            this.lbDescriptionLink.Text = "Description link:";
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(129, 24);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(362, 20);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_TextChanged);
            // 
            // lbSeriesTitle
            // 
            this.lbSeriesTitle.AutoSize = true;
            this.lbSeriesTitle.Location = new System.Drawing.Point(27, 27);
            this.lbSeriesTitle.Name = "lbSeriesTitle";
            this.lbSeriesTitle.Size = new System.Drawing.Size(27, 13);
            this.lbSeriesTitle.TabIndex = 264;
            this.lbSeriesTitle.Text = "Title";
            // 
            // lbNotes
            // 
            this.lbNotes.AutoSize = true;
            this.lbNotes.Location = new System.Drawing.Point(27, 258);
            this.lbNotes.Name = "lbNotes";
            this.lbNotes.Size = new System.Drawing.Size(38, 13);
            this.lbNotes.TabIndex = 263;
            this.lbNotes.Text = "Notes:";
            // 
            // tbDummyForFocus
            // 
            this.tbDummyForFocus.BackColor = System.Drawing.Color.Red;
            this.tbDummyForFocus.Location = new System.Drawing.Point(106, 232);
            this.tbDummyForFocus.Name = "tbDummyForFocus";
            this.tbDummyForFocus.Size = new System.Drawing.Size(100, 20);
            this.tbDummyForFocus.TabIndex = 284;
            // 
            // tbSizeAsInt
            // 
            this.tbSizeAsInt.BackColor = System.Drawing.Color.Red;
            this.tbSizeAsInt.Location = new System.Drawing.Point(106, 203);
            this.tbSizeAsInt.Name = "tbSizeAsInt";
            this.tbSizeAsInt.Size = new System.Drawing.Size(31, 20);
            this.tbSizeAsInt.TabIndex = 281;
            this.tbSizeAsInt.TextChanged += new System.EventHandler(this.tbSizeAsInt_TextChanged);
            // 
            // ttMovieDbDates
            // 
            this.ttMovieDbDates.AutoPopDelay = 20000;
            this.ttMovieDbDates.InitialDelay = 500;
            this.ttMovieDbDates.IsBalloon = true;
            this.ttMovieDbDates.ReshowDelay = 100;
            this.ttMovieDbDates.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttMovieDbDates.ToolTipTitle = "Operations dates:";
            // 
            // ucMovieInfo
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pMovieDetail);
            this.Name = "ucMovieInfo";
            this.Size = new System.Drawing.Size(830, 590);
            this.Load += new System.EventHandler(this.ucMovieInfo_Load);
            this.pMovieDetail.ResumeLayout(false);
            this.pMovieDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDbDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).EndInit();
            this.tlpMovieStillsWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMovieStill1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMovieDetail;
        private System.Windows.Forms.TableLayoutPanel tlpMovieStillsWrapper;
        private System.Windows.Forms.PictureBox pbMovieStill3;
        private System.Windows.Forms.PictureBox pbMovieStill2;
        private System.Windows.Forms.PictureBox pbMovieStill1;
        private System.Windows.Forms.CheckBox chbTitle;
        private System.Windows.Forms.TextBox tbSizeAsInt;
        private Utils.ButtonEdit tbSize;
        private System.Windows.Forms.MaskedTextBox tbmDuration;
        private Utils.CustomTextBox tbSubtitleSummary;
        private Utils.CustomTextBox tbAudioSummary;
        private System.Windows.Forms.Label lbAudioSummary;
        private System.Windows.Forms.Label lbSizeDuration;
        public Utils.CustomTextBox tbEncodedWith;
        private Utils.CustomTextBox tbFormat;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.Label lbStreamLink;
        private Utils.CustomTextBox tbStreamLink;
        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.ComboBox cbTheme;
        private Utils.CustomTextBox tbYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbCover;
        private Utils.CustomTextBox tbNotes;
        private Utils.CustomTextBox tbRecommendedLink;
        private Utils.CustomTextBox tbRecommended;
        private System.Windows.Forms.Label lbRecommended;
        private Utils.CustomTextBox tbDescriptionLink;
        private System.Windows.Forms.Label lbDescriptionLink;
        public Utils.CustomTextBox tbTitle;
        private System.Windows.Forms.Label lbSeriesTitle;
        private System.Windows.Forms.Label lbNotes;
        private System.Windows.Forms.ToolTip ttTitleContent;
        public System.Windows.Forms.TextBox tbDummyForFocus;
        private System.Windows.Forms.Label lbTrailer;
        private Utils.CustomTextBox tbTrailer;
        private Utils.UnselectableButton bGotoRecommendedSite;
        private Utils.UnselectableButton bGotoTrailer;
        private Utils.UnselectableButton bGotoDescription;
        private System.Windows.Forms.Label lbSynopsis;
        private System.Windows.Forms.PictureBox pbLoader;
        private System.Windows.Forms.ToolTip ttMovieDbDates;
        private System.Windows.Forms.PictureBox pbDbDates;
        private Utils.CustomTextBox tbSynopsis;
        private System.Windows.Forms.CheckBox cbGrabCSMData;
        private Utils.UnselectableButton bRefreshCSMData;
    }
}
