using Jacobi.Vst.Framework;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System;

namespace Accudrums.UI {
    public partial class AccudrumsBase : UserControl {

        private Color activeColor;

        private Color inactiveColor;

        public object DataSource { get; private set; }

        public AccudrumsBase() {
            InitializeComponent();
            inactiveColor = pnlButtonGrid.BackColor;
            activeColor = Color.Orange;
        }

        internal bool InitializeParameters(List<VstParameterManager> parameters) {
            return true;
        }

        internal void ProcessIdle() {
            // TODO: short idle processing here
        }

        public void LoadGrid(List<GridButton> buttons) {
            pnlButtonGrid.Controls.Clear();
            pnlButtonGrid.Controls.AddRange(buttons.ToArray());
        }

        public void ColorGridItem(byte note) {
            var controls = this.pnlButtonGrid.Controls.OfType<GridButton>();
            foreach (var button in controls) {
                if (button.Note == note) {
                    button.BackColor = inactiveColor;
                }
            }
        }

        public void SetItemActive(byte note) {
            var controls = this.pnlButtonGrid.Controls.OfType<GridButton>();
            foreach (var button in controls) {
                if (button.Note == note) {
                    button.BackColor = activeColor;
                }
            }
        }

        public void SetItemInactive(byte note) {
            var controls = this.pnlButtonGrid.Controls.OfType<GridButton>();
            foreach (var button in controls) {
                if (button.Note == note) {
                    button.BackColor = inactiveColor;
                }
            }
        }

        public void LoadKitsCombobox(List<KitListItem> kits) {
            lstKits.DisplayMember = "Name";
            lstKits.ValueMember = "Value";

            foreach (var kit in kits) {
                lstKits.Items.Add(kit);
            }
        }

        public int GetPanelGridWidth() {
            return pnlButtonGrid.Width;
        }

        public int GetPanelGridHeight() {
            return pnlButtonGrid.Height;
        }

        public void SetListKitsIndexChanged(EventHandler eventhandler) {
            lstKits.SelectedIndexChanged += eventhandler;
        }

        public KitListItem GetListKitsSelectedItem() {
            return (KitListItem)lstKits.SelectedItem;
        }

        public void SetCurrentKitName(string name) {
            lblCurrentKit.Text = string.Concat("Kit: ", name);
        }

        public void SetItemInKitListSelected(string name) {
            foreach (KitListItem item in lstKits.Items) {
                if (item.Name == name) {
                    lstKits.SelectedItem = item;
                }
            }
        }

        public void SetGridItemDetailsControl(Objects.GridItem gridItem) {
            gridItemDetails.SetGridItem(gridItem);
        }

    }

    public class GridButton : Button {
        public byte Note { get; set; }
    }

    public class KitListItem {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
