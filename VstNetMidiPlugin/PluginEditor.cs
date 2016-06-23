using System;
using System.Drawing;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Common;
using Accudrums.UI;
using System.Collections.Generic;

namespace Accudrums {
    /// <summary>
    /// This object manages the custom editor (UI) for your plugin.
    /// </summary>
    /// <remarks>
    /// When you do not implement a custom editor, 
    /// your Parameters will be displayed in an editor provided by the host.
    /// </remarks>
    internal sealed class PluginEditor : IVstPluginEditor {
        private Plugin _plugin;
        private WinFormsControlWrapper<AccudrumsBase> _view;

        public PluginEditor(Plugin plugin) {
            _plugin = plugin;
            _view = new WinFormsControlWrapper<AccudrumsBase>();
        }

        public Rectangle Bounds {
            get { return _view.Bounds; }
        }

        public void Close() {
            _view.Close();
        }

        public void KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            // empty by design
        }

        public void KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            // empty by design
        }

        public void CurrentNote(string note) {
            _view.SafeInstance.SetNote(note);
        }

        public void PlayKick() {
            _view.SafeInstance.PlayKick();
        }

        public void StopKick() {
            _view.SafeInstance.StopKick();
        }

        public VstKnobMode KnobMode { get; set; }

        public void Open(IntPtr hWnd) {
            // make a list of parameters to pass to the dlg.
            var paramList = new List<VstParameterManager>()
                {
                    _plugin.MidiProcessor.Gain.GainMgr,
                    _plugin.MidiProcessor.Transpose.TransposeMgr,
                };

            _view.SafeInstance.InitializeParameters(paramList);

            _view.Open(hWnd);
        }

        public void ProcessIdle() {
            // keep your processing short!
            _view.SafeInstance.ProcessIdle();
        }

        bool IVstPluginEditor.KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            throw new NotImplementedException();
        }

        bool IVstPluginEditor.KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            throw new NotImplementedException();
        }
    }
}
