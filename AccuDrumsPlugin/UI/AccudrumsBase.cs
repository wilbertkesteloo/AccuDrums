using Accudrums.Objects;
using Jacobi.Vst.Framework;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Accudrums.UI {
    public partial class AccudrumsBase : UserControl {
        public AccudrumsBase() {
            InitializeComponent();
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

        public void LoadGrid(List<Button> buttons) {
            pnlButtonGrid.Controls.Clear();
            pnlButtonGrid.Controls.AddRange(buttons.ToArray());
        }

        public int GetPanelGridWidth() {
            return pnlButtonGrid.Width;
        }

        public int GetPanelGridHeight() {
            return pnlButtonGrid.Height;
        }
    }
}
