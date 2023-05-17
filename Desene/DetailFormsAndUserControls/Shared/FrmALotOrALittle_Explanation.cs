using Common;
using DAL;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace Desene.DetailFormsAndUserControls.Shared
{
    public partial class FrmALotOrALittle_Explanation : Form
    {
        public FrmALotOrALittle_Explanation(ALotOrALittle data)
        {
            InitializeComponent();

            DataToControls(data);

        }

        private void DataToControls(ALotOrALittle data)
        {
            switch (data.Category)
            {
                case ALotOrAlittleElements.EducationalValue:
                    lbSectionTitle.Text = "Educational Value";
                    pbCategory.Image = Properties.Resources.educational_value;
                    break;

                case ALotOrAlittleElements.PositiveMessages:
                    lbSectionTitle.Text = "Positive Messages";
                    pbCategory.Image = Properties.Resources.positive_messages;
                    break;

                case ALotOrAlittleElements.PositiveRoleModelsAndRepresentations:
                    lbSectionTitle.Text = "Positive Role Models && Representations";
                    pbCategory.Image = Properties.Resources.positive_role_models;
                    break;

                case ALotOrAlittleElements.ViolenceAndScariness:
                    lbSectionTitle.Text = "Violence && Scariness";
                    pbCategory.Image = Properties.Resources.violence;
                    break;

                case ALotOrAlittleElements.SexyStuff:
                    lbSectionTitle.Text = "Sexy Stuff";
                    pbCategory.Image = Properties.Resources.sexy_stuff;
                    break;

                case ALotOrAlittleElements.Language:
                    lbSectionTitle.Text = "Language";
                    pbCategory.Image = Properties.Resources.language;
                    break;

                case ALotOrAlittleElements.Consumerism:
                    lbSectionTitle.Text = "Consumerism";
                    pbCategory.Image = Properties.Resources.consumerism;
                    break;

                case ALotOrAlittleElements.DrinkingDrugsAndSmoking:
                    lbSectionTitle.Text = "Drinking, Drugs && Smoking";
                    pbCategory.Image = Properties.Resources.drinking_druge_smoking;
                    break;

                case ALotOrAlittleElements.DiverseRepresentations:
                    lbSectionTitle.Text = "Diverse Representations";
                    pbCategory.Image = Properties.Resources.positive_role_models;
                    break;
            }

            if (data.Rating > 0)
            {
                pbRating.Visible = true;
                GraphicsHelpers.DrawRating(data.Rating, pbRating, new Font("Microsoft Sans Serif", 20, FontStyle.Regular), "●");
            }

            lbExplanation.Text = data.Description;
        }
    }
}
