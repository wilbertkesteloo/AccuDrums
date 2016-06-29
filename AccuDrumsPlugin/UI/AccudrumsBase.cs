using Jacobi.Vst.Framework;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace Accudrums.UI {
    public partial class AccudrumsBase : UserControl {

        private Color activeColor;
        private Color inactiveColor;
            
        public AccudrumsBase() {
            InitializeComponent();
            inactiveColor = pnlButtonGrid.BackColor;
            activeColor = Color.Orange;
        }

        internal bool InitializeParameters(List<VstParameterManager> parameters) {
            return true;
        }

        internal void SetNote(string note) {
            lblNote.Text = "Note: " + note;
        }

        internal void ProcessIdle() {
            // TODO: short idle processing here
        }

        public void SetCurrentKitName(string name) {
            lblCurrentKit.Text = string.Concat("Kit: ", name);
        }

        public void LoadGrid(List<GridButton> buttons) {
            pnlButtonGrid.Controls.Clear();
            pnlButtonGrid.Controls.AddRange(buttons.ToArray());
        }

        public void ColorGridItem(byte note) {
            var controls = this.pnlButtonGrid.Controls.OfType<GridButton>();
            foreach(var button in controls) {
                if(button.Note == note) {
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

        public int GetPanelGridWidth() {
            return pnlButtonGrid.Width;
        }

        public int GetPanelGridHeight() {
            return pnlButtonGrid.Height;
        }
    }

    public class GridButton : Button {
        public byte Note { get; set; }
    }
}
