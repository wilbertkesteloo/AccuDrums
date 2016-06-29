using Accudrums.Objects;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using System.Collections.Generic;

namespace Accudrums {
    /// <summary>
    /// The Plugin root object.
    /// </summary>
    internal sealed class Plugin : VstPluginWithInterfaceManagerBase {
        /// <summary>
        /// TODO: assign a unique plugin.
        /// </summary>
        private static readonly int UniquePluginId = new FourCharacterCode("1234").ToInt32();
        /// <summary>
        /// TODO: assign a plugin name.
        /// </summary>
        private const string PluginName = "AccuDrums";
        /// <summary>
        /// TODO: assign a product name.
        /// </summary>
        private const string ProductName = "AccuDrums";
        /// <summary>
        /// TODO: assign a vendor name.
        /// </summary>
        private const string VendorName = "Kesteloo Engineering Inc.";
        /// <summary>
        /// TODO: assign a plugin version.
        /// </summary>
        private const int PluginVersion = 0000;

        private Kit TR909 = new Kit() {
            Name = "Roland Tr909",
            Grid = new Grid() {
                GridItems = new List<GridItem>() {
                    new GridItem() { X=0,Y=0,Name="Bass Drum",Note=35, Samples= new List<Sample>() { new Sample(35, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_BD01.wav") } },
                    new GridItem() { X=1,Y=0,Name="E-Bass Drum",Note=36, Samples= new List<Sample>() { new Sample(36, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_BD01.wav") } },
                    new GridItem() { X=2,Y=0,Name="Pedal-HH",Note=44, Samples= new List<Sample>() { new Sample(44, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HH01.wav") } },
                    new GridItem() { X=3,Y=0,Name="Closed-HH",Note=42, Samples= new List<Sample>() { new Sample(42, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HH07.wav") } },
                    new GridItem() { X=0,Y=1,Name="Open-HH",Note=46, Samples= new List<Sample>() { new Sample(46, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHO1.wav") } },
                    new GridItem() { X=1,Y=1,Name="Crash 1",Note=49, Samples= new List<Sample>() { new Sample(49, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ CRSH1.wav") } },
                    new GridItem() { X=2,Y=1,Name="Crash 2",Note=57, Samples= new List<Sample>() { new Sample(57, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ CRSH2.wav") } },
                    new GridItem() { X=3,Y=1,Name="Ride 1",Note=51, Samples= new List<Sample>() { new Sample(51, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ RIDE1.wav") } },
                    new GridItem() { X=0,Y=2,Name="Snare Drum",Note=38, Samples= new List<Sample>() { new Sample(38, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_SD 01.wav") } },
                    new GridItem() { X=1,Y=2,Name="Handclap",Note=40, Samples= new List<Sample>() { new Sample(40, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_SD CLAP1.wav") } },
                    new GridItem() { X=2,Y=2,Name="High Tom",Note=50, Samples= new List<Sample>() { new Sample(50, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM HI1.wav") } },
                    new GridItem() { X=3,Y=2,Name="Mid Tom",Note=48, Samples= new List<Sample>() { new Sample(48, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM ME1.wav") } },
                    new GridItem() { X=0,Y=3,Name="Low Tom",Note=45, Samples= new List<Sample>() { new Sample(45, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM LO1.wav") } },
                },
                XSize = 4,
                YSize = 4,
            }
        };

        /// <summary>
        /// Initializes the one an only instance of the Plugin root object.
        /// </summary>
        public Plugin()
            : base(PluginName,
            new VstProductInfo(ProductName, VendorName, PluginVersion),
                // TODO: what type of plugin are your making?
                VstPluginCategory.Effect,
                VstPluginCapabilities.NoSoundInStop,
                // initial delay: number of samples your plugin lags behind.
                0,
                UniquePluginId) {
            SampleManager = new SampleManager();

            //Load basic Tr909 drumkit
            LoadKit(TR909);
        }

        /// <summary>
        /// Gets the sample manager.
        /// </summary>
        public SampleManager SampleManager { get; private set; }

        ///// <summary>
        ///// Gets the audio processor object.
        ///// </summary>
        //public DummyAudioProcessor AudioProcessor {
        //    get { return GetInstance<DummyAudioProcessor>(); }
        //}

        /// <summary>
        /// Gets the audio processor object.
        /// </summary>
        public AudioProcessor AudioProcessor {
            get { return GetInstance<AudioProcessor>(); }
        }

        /// <summary>
        /// Gets the midi processor object.
        /// </summary>
        public MidiProcessor MidiProcessor {
            get { return GetInstance<MidiProcessor>(); }
        }

        /// <summary>
        /// Gets the plugin editor object.
        /// </summary>
        public PluginEditor PluginEditor {
            get { return GetInstance<PluginEditor>(); }
        }

        /// <summary>
        /// Gets the plugin programs object.
        /// </summary>
        public PluginPrograms PluginPrograms {
            get { return GetInstance<PluginPrograms>(); }
        }

        /// <summary>
        /// Implement this when you do audio processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance) {
            // Dont expose an AudioProcessor if Midi is output in the MidiProcessor
            if (!MidiProcessor.SyncWithAudioProcessor) return null;

            //if (instance == null) {
            //    return new DummyAudioProcessor(this);
            //}

            if (instance == null) {
                return new AudioProcessor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateAudioProcessor(instance);
        }

        /// <summary>
        /// Implement this when you do midi processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance) {
            if (instance == null) {
                return new MidiProcessor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateMidiProcessor(instance);
        }

        /// <summary>
        /// Implement this when you output midi events to the host.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginMidiSource CreateMidiSource(IVstPluginMidiSource instance) {
            // we implement this interface on out midi processor.
            return (IVstPluginMidiSource)MidiProcessor;
        }

        /// <summary>
        /// Implement this when you need a custom editor (UI).
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginEditor CreateEditor(IVstPluginEditor instance) {
            if (instance == null) {
                return new PluginEditor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateEditor(instance);
        }

        /// <summary>
        /// Implement this when you implement plugin programs or presets.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance) {
            if (instance == null) {
                return new PluginPrograms(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreatePrograms(instance);
        }

        /// <summary>
        /// Sets up kit in entire application
        /// </summary>
        /// <param name="kit"></param>
        private void LoadKit(Kit kit) {
            //Load visual items in PluginEditor
            PluginEditor.CurrentKit = kit;

            //Load samples into samplemanager
            SampleManager.LoadSamples(kit.Grid.GridItems);
        }

    }
}
