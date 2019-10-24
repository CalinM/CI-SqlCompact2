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
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.lbTrailer = new System.Windows.Forms.Label();
            this.tlpMovieStillsWrapper = new System.Windows.Forms.TableLayoutPanel();
            this.pbMovieStill3 = new System.Windows.Forms.PictureBox();
            this.pbMovieStill2 = new System.Windows.Forms.PictureBox();
            this.pbMovieStill1 = new System.Windows.Forms.PictureBox();
            this.chbTitle = new System.Windows.Forms.CheckBox();
            this.tbmDuration = new System.Windows.Forms.MaskedTextBox();
            this.lbSubtitleSummary = new System.Windows.Forms.Label();
            this.lbAudioSummary = new System.Windows.Forms.Label();
            this.lbSizeDuration = new System.Windows.Forms.Label();
            this.lbEncodedWith = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.lbStreamLink = new System.Windows.Forms.Label();
            this.cbTheme = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRecommended = new System.Windows.Forms.Label();
            this.pbDescriptionLink = new System.Windows.Forms.Label();
            this.lbSeriesTitle = new System.Windows.Forms.Label();
            this.lbNotes = new System.Windows.Forms.Label();
            this.tbDummyForFocus = new System.Windows.Forms.TextBox();
            this.tbSizeAsInt = new System.Windows.Forms.TextBox();
            this.ttTitleContent = new System.Windows.Forms.ToolTip(this.components);
            this.tbTrailer = new Utils.CustomTextBox();
            this.tbSize = new Utils.ButtonEdit();
            this.tbSubtitleSummary = new Utils.CustomTextBox();
            this.tbAudioSummary = new Utils.CustomTextBox();
            this.tbEncodedWith = new Utils.CustomTextBox();
            this.tbFormat = new Utils.CustomTextBox();
            this.tbStreamLink = new Utils.CustomTextBox();
            this.tbYear = new Utils.CustomTextBox();
            this.tbNotes = new Utils.CustomTextBox();
            this.tbRecommendedLink = new Utils.CustomTextBox();
            this.tbRecommended = new Utils.CustomTextBox();
            this.tbDescriptionLink = new Utils.CustomTextBox();
            this.tbTitle = new Utils.CustomTextBox();
            this.pMovieDetail.SuspendLayout();
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
            this.pMovieDetail.Controls.Add(this.pbCover);
            this.pMovieDetail.Controls.Add(this.cbQuality);
            this.pMovieDetail.Controls.Add(this.lbTrailer);
            this.pMovieDetail.Controls.Add(this.tbTrailer);
            this.pMovieDetail.Controls.Add(this.tlpMovieStillsWrapper);
            this.pMovieDetail.Controls.Add(this.chbTitle);
            this.pMovieDetail.Controls.Add(this.tbSize);
            this.pMovieDetail.Controls.Add(this.tbmDuration);
            this.pMovieDetail.Controls.Add(this.tbSubtitleSummary);
            this.pMovieDetail.Controls.Add(this.lbSubtitleSummary);
            this.pMovieDetail.Controls.Add(this.tbAudioSummary);
            this.pMovieDetail.Controls.Add(this.lbAudioSummary);
            this.pMovieDetail.Controls.Add(this.lbSizeDuration);
            this.pMovieDetail.Controls.Add(this.tbEncodedWith);
            this.pMovieDetail.Controls.Add(this.tbFormat);
            this.pMovieDetail.Controls.Add(this.lbEncodedWith);
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
            this.pMovieDetail.Controls.Add(this.pbDescriptionLink);
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
            this.cbQuality.Location = new System.Drawing.Point(478, 24);
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(89, 21);
            this.cbQuality.TabIndex = 255;
            // 
            // lbTrailer
            // 
            this.lbTrailer.AutoSize = true;
            this.lbTrailer.Location = new System.Drawing.Point(27, 249);
            this.lbTrailer.Name = "lbTrailer";
            this.lbTrailer.Size = new System.Drawing.Size(39, 13);
            this.lbTrailer.TabIndex = 286;
            this.lbTrailer.Text = "Trailer:";
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
            this.chbTitle.Location = new System.Drawing.Point(552, 299);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(15, 14);
            this.chbTitle.TabIndex = 282;
            this.chbTitle.UseVisualStyleBackColor = true;
            this.chbTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTitle_MouseClick);
            // 
            // tbmDuration
            // 
            this.tbmDuration.HideSelection = false;
            this.tbmDuration.Location = new System.Drawing.Point(188, 127);
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
            // lbSubtitleSummary
            // 
            this.lbSubtitleSummary.AutoSize = true;
            this.lbSubtitleSummary.Location = new System.Drawing.Point(27, 183);
            this.lbSubtitleSummary.Name = "lbSubtitleSummary";
            this.lbSubtitleSummary.Size = new System.Drawing.Size(91, 13);
            this.lbSubtitleSummary.TabIndex = 280;
            this.lbSubtitleSummary.Text = "Subtiles summary:";
            // 
            // lbAudioSummary
            // 
            this.lbAudioSummary.AutoSize = true;
            this.lbAudioSummary.Location = new System.Drawing.Point(27, 157);
            this.lbAudioSummary.Name = "lbAudioSummary";
            this.lbAudioSummary.Size = new System.Drawing.Size(81, 13);
            this.lbAudioSummary.TabIndex = 279;
            this.lbAudioSummary.Text = "Audio summary:";
            // 
            // lbSizeDuration
            // 
            this.lbSizeDuration.AutoSize = true;
            this.lbSizeDuration.Location = new System.Drawing.Point(27, 131);
            this.lbSizeDuration.Name = "lbSizeDuration";
            this.lbSizeDuration.Size = new System.Drawing.Size(81, 13);
            this.lbSizeDuration.TabIndex = 278;
            this.lbSizeDuration.Text = "Size / Duration:";
            // 
            // lbEncodedWith
            // 
            this.lbEncodedWith.AutoSize = true;
            this.lbEncodedWith.Location = new System.Drawing.Point(27, 325);
            this.lbEncodedWith.Name = "lbEncodedWith";
            this.lbEncodedWith.Size = new System.Drawing.Size(75, 13);
            this.lbEncodedWith.TabIndex = 273;
            this.lbEncodedWith.Text = "Encoded with:";
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(27, 299);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(42, 13);
            this.lbFormat.TabIndex = 272;
            this.lbFormat.Text = "Format:";
            // 
            // lbStreamLink
            // 
            this.lbStreamLink.AutoSize = true;
            this.lbStreamLink.Location = new System.Drawing.Point(27, 273);
            this.lbStreamLink.Name = "lbStreamLink";
            this.lbStreamLink.Size = new System.Drawing.Size(62, 13);
            this.lbStreamLink.TabIndex = 269;
            this.lbStreamLink.Text = "Stream link:";
            // 
            // cbTheme
            // 
            this.cbTheme.FormattingEnabled = true;
            this.cbTheme.Location = new System.Drawing.Point(188, 101);
            this.cbTheme.Name = "cbTheme";
            this.cbTheme.Size = new System.Drawing.Size(190, 21);
            this.cbTheme.TabIndex = 5;
            this.cbTheme.SelectionChangeCommitted += new System.EventHandler(this.cbTheme_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 268;
            this.label1.Text = "Year / Theme:";
            // 
            // lbRecommended
            // 
            this.lbRecommended.AutoSize = true;
            this.lbRecommended.Location = new System.Drawing.Point(27, 79);
            this.lbRecommended.Name = "lbRecommended";
            this.lbRecommended.Size = new System.Drawing.Size(82, 13);
            this.lbRecommended.TabIndex = 266;
            this.lbRecommended.Text = "Recommended:";
            // 
            // pbDescriptionLink
            // 
            this.pbDescriptionLink.AutoSize = true;
            this.pbDescriptionLink.Location = new System.Drawing.Point(27, 53);
            this.pbDescriptionLink.Name = "pbDescriptionLink";
            this.pbDescriptionLink.Size = new System.Drawing.Size(82, 13);
            this.pbDescriptionLink.TabIndex = 265;
            this.pbDescriptionLink.Text = "Description link:";
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
            this.lbNotes.Location = new System.Drawing.Point(27, 209);
            this.lbNotes.Name = "lbNotes";
            this.lbNotes.Size = new System.Drawing.Size(38, 13);
            this.lbNotes.TabIndex = 263;
            this.lbNotes.Text = "Notes:";
            // 
            // tbDummyForFocus
            // 
            this.tbDummyForFocus.BackColor = System.Drawing.Color.Red;
            this.tbDummyForFocus.Location = new System.Drawing.Point(109, 218);
            this.tbDummyForFocus.Name = "tbDummyForFocus";
            this.tbDummyForFocus.Size = new System.Drawing.Size(100, 20);
            this.tbDummyForFocus.TabIndex = 284;
            // 
            // tbSizeAsInt
            // 
            this.tbSizeAsInt.BackColor = System.Drawing.Color.Red;
            this.tbSizeAsInt.Location = new System.Drawing.Point(109, 128);
            this.tbSizeAsInt.Name = "tbSizeAsInt";
            this.tbSizeAsInt.Size = new System.Drawing.Size(31, 20);
            this.tbSizeAsInt.TabIndex = 281;
            this.tbSizeAsInt.TextChanged += new System.EventHandler(this.tbSizeAsInt_TextChanged);
            // 
            // tbTrailer
            // 
            this.tbTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTrailer.Location = new System.Drawing.Point(132, 248);
            this.tbTrailer.Name = "tbTrailer";
            this.tbTrailer.Size = new System.Drawing.Size(435, 20);
            this.tbTrailer.TabIndex = 9;
            // 
            // tbSize
            // 
            this.tbSize.ButtonCursor = System.Windows.Forms.Cursors.Help;
            this.tbSize.ButtonImage = global::Desene.Properties.Resources.warning;
            this.tbSize.ButtonImageForceWidth = 12;
            this.tbSize.ButtonImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tbSize.ButtonToolTip = "Size mismatch!";
            this.tbSize.ButtonVisible = false;
            this.tbSize.Location = new System.Drawing.Point(132, 128);
            this.tbSize.Name = "tbSize";
            this.tbSize.ReadOnly = true;
            this.tbSize.Size = new System.Drawing.Size(50, 20);
            this.tbSize.TabIndex = 6;
            this.tbSize.TabStop = false;
            this.tbSize.TextChanged += new System.EventHandler(this.tbSize_TextChanged);
            // 
            // tbSubtitleSummary
            // 
            this.tbSubtitleSummary.Location = new System.Drawing.Point(132, 180);
            this.tbSubtitleSummary.Name = "tbSubtitleSummary";
            this.tbSubtitleSummary.ReadOnly = true;
            this.tbSubtitleSummary.Size = new System.Drawing.Size(106, 20);
            this.tbSubtitleSummary.TabIndex = 277;
            this.tbSubtitleSummary.TabStop = false;
            // 
            // tbAudioSummary
            // 
            this.tbAudioSummary.Location = new System.Drawing.Point(132, 154);
            this.tbAudioSummary.Name = "tbAudioSummary";
            this.tbAudioSummary.ReadOnly = true;
            this.tbAudioSummary.Size = new System.Drawing.Size(106, 20);
            this.tbAudioSummary.TabIndex = 276;
            this.tbAudioSummary.TabStop = false;
            // 
            // tbEncodedWith
            // 
            this.tbEncodedWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEncodedWith.Location = new System.Drawing.Point(132, 322);
            this.tbEncodedWith.Name = "tbEncodedWith";
            this.tbEncodedWith.Size = new System.Drawing.Size(435, 20);
            this.tbEncodedWith.TabIndex = 12;
            // 
            // tbFormat
            // 
            this.tbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFormat.Location = new System.Drawing.Point(132, 296);
            this.tbFormat.Name = "tbFormat";
            this.tbFormat.Size = new System.Drawing.Size(246, 20);
            this.tbFormat.TabIndex = 11;
            // 
            // tbStreamLink
            // 
            this.tbStreamLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStreamLink.Location = new System.Drawing.Point(132, 270);
            this.tbStreamLink.Name = "tbStreamLink";
            this.tbStreamLink.Size = new System.Drawing.Size(435, 20);
            this.tbStreamLink.TabIndex = 10;
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(132, 102);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(50, 20);
            this.tbYear.TabIndex = 4;
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(132, 206);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(435, 35);
            this.tbNotes.TabIndex = 8;
            // 
            // tbRecommendedLink
            // 
            this.tbRecommendedLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecommendedLink.Location = new System.Drawing.Point(188, 76);
            this.tbRecommendedLink.Name = "tbRecommendedLink";
            this.tbRecommendedLink.Size = new System.Drawing.Size(379, 20);
            this.tbRecommendedLink.TabIndex = 3;
            // 
            // tbRecommended
            // 
            this.tbRecommended.Location = new System.Drawing.Point(132, 76);
            this.tbRecommended.Name = "tbRecommended";
            this.tbRecommended.Size = new System.Drawing.Size(50, 20);
            this.tbRecommended.TabIndex = 2;
            // 
            // tbDescriptionLink
            // 
            this.tbDescriptionLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescriptionLink.Location = new System.Drawing.Point(132, 50);
            this.tbDescriptionLink.Name = "tbDescriptionLink";
            this.tbDescriptionLink.Size = new System.Drawing.Size(435, 20);
            this.tbDescriptionLink.TabIndex = 1;
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(132, 24);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(340, 20);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_TextChanged);
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
        private System.Windows.Forms.Label lbSubtitleSummary;
        private Utils.CustomTextBox tbAudioSummary;
        private System.Windows.Forms.Label lbAudioSummary;
        private System.Windows.Forms.Label lbSizeDuration;
        public Utils.CustomTextBox tbEncodedWith;
        private Utils.CustomTextBox tbFormat;
        private System.Windows.Forms.Label lbEncodedWith;
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
        private System.Windows.Forms.Label pbDescriptionLink;
        public Utils.CustomTextBox tbTitle;
        private System.Windows.Forms.Label lbSeriesTitle;
        private System.Windows.Forms.Label lbNotes;
        private System.Windows.Forms.ToolTip ttTitleContent;
        public System.Windows.Forms.TextBox tbDummyForFocus;
        private System.Windows.Forms.Label lbTrailer;
        private Utils.CustomTextBox tbTrailer;
    }
}
