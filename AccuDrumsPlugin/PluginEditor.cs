using System;
using System.Drawing;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Common;
using Accudrums.UI;
using System.Collections.Generic;
using Accudrums.Objects;
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

        #region Declarations

        private Plugin _plugin;
        private WinFormsControlWrapper<AccudrumsBase> _view;

        public Kit CurrentKit { get; set; }

        public GridItem SelectedGridItem {get;set;}

        public List<KitListItem> CurrentComboKits { get; set; }

        public Rectangle Bounds { get { return _view.Bounds; } }

        public VstKnobMode KnobMode { get; set; }

        #endregion

        #region Constructor & Inherited Methods

        public PluginEditor(Plugin plugin) {
            _plugin = plugin;
            _view = new WinFormsControlWrapper<AccudrumsBase>();
        }

        public void Open(IntPtr hWnd) {
            // make a list of parameters to pass to the dlg.
            var paramList = new List<VstParameterManager>()
                {
                    _plugin.MidiProcessor.Gain.GainMgr,
                    _plugin.MidiProcessor.Transpose.TransposeMgr,
                };

            _view.SafeInstance.InitializeParameters(paramList);

            _view.SafeInstance.SetListKitsIndexChanged(new EventHandler(lstKits_SelectedIndexChanged));

            LoadGrid(CurrentKit.Grid);
            LoadKitsCombobox();
            SetCurrentKitName(CurrentKit.Name);
            SetGridItemsDetails(SelectedGridItem);
            _view.SafeInstance.SetItemInKitListSelected(CurrentKit.Name);
            _view.Open(hWnd);
        }

        public void Close() {
            _view.Close();
        }

        public void ProcessIdle() {
            _view.SafeInstance.ProcessIdle();
        }

        #endregion

        #region Methods

        public void SetCurrentKitName(string name) {
            _view.SafeInstance.SetCurrentKitName(name);
        }

        public void SetCurrentKit(Kit kit) {
            _plugin.PluginEditor.CurrentKit = kit;
            LoadGrid(CurrentKit.Grid);
            SetCurrentKitName(CurrentKit.Name);

            //When new kit is loaded. automaticly set first grid item details
            SelectedGridItem = CurrentKit.Grid.GridItems.FirstOrDefault();
            SetGridItemsDetails(SelectedGridItem);
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
                     
                    if (gridItem != null) {
                        tmpButton.MouseDown += (s, e) => { ProcessGridButtonClick(gridItem.ID); };
                        tmpButton.MouseUp += (s, e) => { _plugin.SampleManager.ProcessNoteOffEvent(gridItem.Note); };

                        tmpButton.Text = gridItem.Name;
                        tmpButton.Note = gridItem.Note;
                    }

                    buttons.Add(tmpButton);
                }
            }

            _view.SafeInstance.LoadGrid(buttons);
        }

        public void SetComboBoxKitList(List<Kit> kits) {
            var items = new List<KitListItem>();
            foreach (var kit in kits) {
                items.Add(new KitListItem() { Name = kit.Name, Value = kit.ID });
            }
            CurrentComboKits = items;
        }

        private void LoadKitsCombobox() {
            _view.SafeInstance.LoadKitsCombobox(CurrentComboKits);
        }

        private void SetGridItemsDetails(GridItem griditem) {
            _view.SafeInstance.SetGridItemDetailsControl(griditem);
        }

        #endregion

        #region EventHandlers

        private void lstKits_SelectedIndexChanged(object sender, System.EventArgs e) {
            var kit = _view.SafeInstance.GetListKitsSelectedItem();
            _plugin.KitManager.LoadKitByID((int)kit.Value);
        }

        private void ProcessGridButtonClick(int id) {
            GridItem item = CurrentKit.Grid.GridItems.FirstOrDefault(i => i.ID == id);

            //if unused gridItem no Sample and details
            if (item == null) return;

            //Send note to play bij samplemanager
            _plugin.SampleManager.ProcessNoteOnEvent(item.Note);

            //Load grid item options panel
            SelectedGridItem = item;
            SetGridItemsDetails(item);
        }

        public void KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            // empty by design
        }

        public void KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            // empty by design
        }

        bool IVstPluginEditor.KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            throw new NotImplementedException();
        }

        bool IVstPluginEditor.KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
