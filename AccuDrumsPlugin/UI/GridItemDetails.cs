using System;
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
            tbPanning.Value = (int)(currentItem.Panning * 100);
            this.label1.Text = "panning: " + currentItem.Panning;
        }

        private void tbGain_ValueChanged(object sender, System.EventArgs e) {
            currentItem.Gain = tbGain.Value / 100;
        }

        private void tbPanning_ValueChanged(object sender, System.EventArgs e) {
            float value = tbPanning.Value;
            currentItem.Panning = value / 100;
            this.label1.Text = "panning: " + currentItem.Panning;
        }
    }
}
