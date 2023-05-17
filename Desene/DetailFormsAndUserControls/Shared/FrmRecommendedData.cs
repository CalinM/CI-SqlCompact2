using Common;
using DAL;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utils;

namespace Desene.DetailFormsAndUserControls.Shared
{
    public partial class FrmRecommendedData : Form
    {
        private int _fileDetailsId;
        private IniFile _iniFile = new IniFile();

        public FrmRecommendedData(int id)
        {
            InitializeComponent();
            _fileDetailsId = id;
        }

        private void LoadWindowConfig()
        {
            if (_iniFile.KeyExists("Top", "RecommendedWindow") && _iniFile.KeyExists("Left", "RecommendedWindow"))
            {
                Location = new Point(_iniFile.ReadInt("Left", "RecommendedWindow"), _iniFile.ReadInt("Top", "RecommendedWindow"));
            }
        }

        private void FrmRecommendedData_Load(object sender, EventArgs e)
        {
            LoadWindowConfig();

            lbTitle.Text = DAL.CurrentMTD.FileName;

            if (!string.IsNullOrEmpty(DAL.CurrentMTD.RecommendedLink))
            {
                bGotoRecommendedSite.Visible = true;

                var tt2 = new ToolTip();
                tt2.SetToolTip(bGotoRecommendedSite, "Navigate using the default system browser to the current CommonSenseMedia link");
            }

            var opRes = DAL.LoadCSMData(_fileDetailsId);

            if (!opRes.Success)
            {
                cpScrapedData.Visible = false;

                pError.Dock = DockStyle.Fill;

                lbError2.Text = opRes.CustomErrorMessage;
                pError.Visible = true;
            }
            else
            {
                pError.Visible = false;

                cpScrapedData.Dock = DockStyle.Fill;

                DataToControls((CSMScrapeResult)opRes.AdditionalDataReturn);
                cpScrapedData.Visible = true;
            }
        }

        private void FrmRecommendedData_FormClosed(object sender, FormClosedEventArgs e)
        {
            _iniFile.Write("Top", Location.Y.ToString(), "RecommendedWindow");
            _iniFile.Write("Left", Location.X.ToString(), "RecommendedWindow");
        }

        private void DataToControls(CSMScrapeResult csmData)
        {
            lbGreenAge.Text = csmData.GreenAge;

            GraphicsHelpers.DrawRating(csmData.Rating, pbCSMRating, new Font("Microsoft Sans Serif", 24, FontStyle.Regular), "⋆");

            var tt = new ToolTip();
            tt.IsBalloon = true;
            tt.AutoPopDelay = 20000;
            tt.SetToolTip(
                pbInfo,
                string.Format(
                    "The ratings are based on child{0}development best practices.{0}We display the minimum age{0}for which content is{0}developmentally appropriate.{0}The star rating reflects overall{0}quality.",
                    Environment.NewLine)
                );

            lbShortDescription.Text = csmData.ShortDescription;


            if (string.IsNullOrEmpty(csmData.AdultRecomendedAge))
            {
                lbAdultRecommendedAge.Text = "No reviews yet";
                lbInfo1.Visible = false;
            }
            else
            {
                lbAdultRecommendedAge.Text = csmData.AdultRecomendedAge;
                GraphicsHelpers.DrawRating(csmData.AdultRating, pbAdultRating, new Font("Microsoft Sans Serif", 20, FontStyle.Regular), "⋆");
            }

            if (string.IsNullOrEmpty(csmData.ChildRecomendedAge))
            {
                lbKidsRecommendedAge.Text = "No reviews yet";
                lbInfo2.Visible = false;
            }
            else
            {
                lbKidsRecommendedAge.Text = csmData.ChildRecomendedAge;
                GraphicsHelpers.DrawRating(csmData.ChildRating, pbKidsRating, new Font("Microsoft Sans Serif", 20, FontStyle.Regular), "⋆");
            }


            var scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.EducationalValue);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbEducationalValue_Rating, lbEducationalValue_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.PositiveMessages);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbPositiveMessages_Rating, lbPosiviteMessages_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.PositiveRoleModelsAndRepresentations);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbPositiveRoles_Rating, lbPositiveRoles_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.ViolenceAndScariness);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbViolenceScariness_Rating, lbViolenceScariness_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.SexyStuff);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbSexyStuff_Rating, lbSexyStuff_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.Language);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbLanguage_Rating, lbLanguage_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.Consumerism);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbConsumerism_Rating, lbConsumerism_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.DrinkingDrugsAndSmoking);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbDrinkingSmoking_Rating, lbDrinkingSmoking_Expl);
            }

            scrapedDataObj = csmData.ALotOrALittle.FirstOrDefault(_ => _.Category == ALotOrAlittleElements.DiverseRepresentations);
            if (scrapedDataObj != null)
            {
                SetDataToControls(scrapedDataObj, pbDiverseRepresentation_Rating, lbDiverseRepresentation_Expl);
            }


            void SetDataToControls(ALotOrALittle dataObj, PictureBox pbCtrl, Label detailsLabelCtrl)
            {
                if (dataObj.Rating > 0)
                {
                    pbCtrl.Visible = true;
                    GraphicsHelpers.DrawRating(dataObj.Rating, pbCtrl, new Font("Microsoft Sans Serif", 16, FontStyle.Regular), "●");
                }

                if (!string.IsNullOrEmpty(dataObj.Description))
                {
                    detailsLabelCtrl.Visible = true;
                    detailsLabelCtrl.Click += (sender, e) =>
                    {
                        var frmDetails = new FrmALotOrALittle_Explanation(dataObj);
                        frmDetails.Owner = this;
                        frmDetails.ShowDialog();
                    };
                }
            }


            lbWhatParentsNeedToKnow.Text = csmData.Review;
            pWhatParentsNeedToKnow.Height = (lbWhatParentsNeedToKnow.Location.Y * 2) + lbWhatParentsNeedToKnow.Height;

            lbWhatsTheStory.Text = csmData.Story;
            pWhatsTheStory.Height = (lbWhatsTheStory.Location.Y * 2) + lbWhatsTheStory.Height;

            lbIsItAnyGood.Text = csmData.IsItAnyGood;
            pIsItAnyGood.Height = (lbIsItAnyGood.Location.Y * 2) + lbIsItAnyGood.Height;

            var totalHeight = 0;

            foreach (var item in csmData.TalkWithKidsAbout)
            {
                var p = new Panel();
                p.Dock = DockStyle.Top;

                //the "Fill" must go fist!
                var p_text = new Panel();
                p_text.Dock = DockStyle.Fill;
                p_text.Margin = new Padding(0);
                p.Controls.Add(p_text);

                var p_bullet = new Panel();
                p_bullet.Dock = DockStyle.Left;
                p_bullet.Margin = new Padding(0);
                //p_bullet.BackColor = Color.Yellow;
                p_bullet.Width = 20;
                p.Controls.Add(p_bullet);

                var l_text = new Label();
                l_text.AutoSize = true;
                l_text.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                l_text.Location = new System.Drawing.Point(0, 8);
                l_text.MaximumSize = new System.Drawing.Size(570, 0);
                l_text.Text = item;

                p_text.Controls.Add(l_text);

                var l_bullet = new Label();
                l_bullet.AutoSize = true;
                //l_bullet.BackColor = Color.Green;
                l_bullet.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                l_bullet.Location = new System.Drawing.Point(7, 8);
                l_bullet.Text = "●";

                p_bullet.Controls.Add(l_bullet);

                p.Height = l_text.Location.Y + l_text.Height;

                totalHeight += p.Height;
                pTalkAbout.Controls.Add(p);
            }

            pTalkAbout.Height = totalHeight + 8;
        }
        /*
        private void DrawRating(int? rating, PictureBox pb, Font font, string textChar)
        {
            var r = rating.GetValueOrDefault(0);
            var stars = new List<RatingData>();

            for (int i = 0; i < r; i++)
            {
                stars.Add(new RatingData() { TextChar = textChar, Color = Color.Green });
            }

            for (int i = ((int)r)+1; i <=5 ; i++)
            {
                stars.Add(new RatingData() { TextChar = textChar, Color = Color.Silver });
            }

            // PictureBox needs an image to draw on
            pb.Image = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(pb.Image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // create all-white background for drawing
                SolidBrush brush = new SolidBrush(Color.Transparent);
                g.FillRectangle(brush, 0, 0, pb.Image.Width, pb.Image.Height);

                float x = 0;

                for (int i = 0; i < stars.Count; i++)
                {
                    var rd = stars[i];

                    // draw text in whatever color
                    g.DrawString(rd.TextChar, font, new SolidBrush(rd.Color), x, 0);
                    // measure text and advance x
                    x +=  MeasureDisplayStringWidth(g, rd.TextChar, font)-5;//(g.MeasureString(chunks[i], pb.Font)).Width;
                 }
            }
        }

        private int MeasureDisplayStringWidth(Graphics graphics, string text, Font font)
        {
            StringFormat format = new StringFormat ();
            RectangleF rect = new RectangleF(0, 0, 1000, 1000);
            CharacterRange[] ranges  = { new CharacterRange(0, text.Length) } ;
            Region[] regions = new Region[1];

            format.SetMeasurableCharacterRanges (ranges);

            regions = graphics.MeasureCharacterRanges (text, font, rect, format);
            rect = regions[0].GetBounds (graphics);

            return (int)(rect.Right + 1.0f);
        }
        */

        private void genericLabelArrow_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Black;
        }

        private void genericLabelArrow_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Gray;
        }

        private void bGotoRecommendedSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DAL.CurrentMTD.RecommendedLink);
        }
    }
}
