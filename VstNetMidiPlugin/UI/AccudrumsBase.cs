using Jacobi.Vst.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VstNetMidiPlugin.UI {
    public partial class AccudrumsBase : UserControl {
        public AccudrumsBase() {
            InitializeComponent();
        }

        internal bool InitializeParameters(List<VstParameterManager> parameters) {
            pluginEditorView.InitializeParameters(parameters);
            return true;
        }

        internal void ProcessIdle() {
            // TODO: short idle processing here
        }
    }
}
