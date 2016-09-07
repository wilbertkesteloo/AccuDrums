using System.Windows.Forms;

namespace Accudrums.UI {
    public partial class GridItemDetails : UserControl {

        private Objects.GridItem currentItem;

        public GridItemDetails() {
            InitializeComponent();
        }

        public void SetGridItem(Objects.GridItem gridItem) {
            currentItem = gridItem;
            LoadGridItemDetails();
        }

        private void LoadGridItemDetails() {
            if (currentItem == null) return;

            lblCurrentItem.Text = "Current Item: " + currentItem.Name;

            //Set Gain
            tbGain.Value = (int)(currentItem.Gain * 100);

            //Set Panning
            tbPanning.Value = (int)currentItem.Panning;
            this.label1.Text = "panning: " + currentItem.Panning;
        }

        private void tbGain_ValueChanged(object sender, System.EventArgs e) {
            currentItem.Gain = tbGain.Value / 100;
        }

        private void tbPanning_ValueChanged(object sender, System.EventArgs e) {
            currentItem.Panning = tbPanning.Value;
            this.label1.Text = "panning: " + currentItem.Panning;
        }
    }
}
