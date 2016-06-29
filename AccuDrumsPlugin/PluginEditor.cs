using System;
using System.Drawing;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Common;
using Accudrums.UI;
using System.Collections.Generic;
using Accudrums.Objects;
using System.Windows.Forms;
using System.Linq;

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

        public Kit CurrentKit { get; set; }

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

        public void SetCurrentKitName(string name) {
            _view.SafeInstance.SetCurrentKitName(name);
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

            SetCurrentKitName(CurrentKit.Name);
            LoadGrid(CurrentKit.Grid);

            _view.Open(hWnd);
        }

        public void SetItemActive(byte note) {
            _view.SafeInstance.SetItemActive(note);
        }

        public void SetItemInactive(byte note) {
            _view.SafeInstance.SetItemInactive(note);
        }

        /// <summary>
        /// Loads Grid from the current Kit
        /// </summary>
        /// <param name="grid">Contains all the grid information</param>
        public void LoadGrid(Grid grid) {
            List<GridButton> buttons = new List<GridButton>();

            int ButtonHeight = 40;
            int Distance = 20;
            int start_x = 10;
            int start_y = 10;
            int ButtonWidth = (_view.SafeInstance.GetPanelGridWidth() - (Distance * grid.XSize)) / grid.XSize;

            for (int y = 0; y < grid.YSize; y++) {
                for (int x = 0; x < grid.XSize; x++) {
                    var gridItem = grid.GridItems.FirstOrDefault(i => i.X == x && i.Y == y);
                    GridButton tmpButton = new GridButton() {
                        Top = start_x + (x * ButtonHeight + Distance),
                        Left = start_y + (y * ButtonWidth + Distance),
                        Width = ButtonWidth,
                        Height = ButtonHeight,
                    };

                    tmpButton.MouseDown += (s, e) => { _plugin.SampleManager.ProcessNoteOnEvent(gridItem.Note); };
                    tmpButton.MouseUp += (s, e) => { _plugin.SampleManager.ProcessNoteOffEvent(gridItem.Note); };

                    if (gridItem != null) {
                        tmpButton.Text = gridItem.Name;
                        tmpButton.Note = gridItem.Note;
                    }

                    buttons.Add(tmpButton);
                }
            }

            _view.SafeInstance.LoadGrid(buttons);

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
