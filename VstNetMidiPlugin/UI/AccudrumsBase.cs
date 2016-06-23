using Jacobi.Vst.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Accudrums.UI {
    public partial class AccudrumsBase : UserControl {
        public AccudrumsBase() {
            InitializeComponent();
        }

        internal bool InitializeParameters(List<VstParameterManager> parameters) {
            pluginEditorView.InitializeParameters(parameters);
            return true;
        }

        internal void SetNote(string note) {
            lblNote.Text = "Note: " + note;
        }
        
        internal void PlayKick() {
            lblKick.Text = "KICK: ON";
        }

        internal void StopKick() {
            lblKick.Text = "KICK: OFF";
        }

        internal void ProcessIdle() {
            // TODO: short idle processing here
        }
    }
}
