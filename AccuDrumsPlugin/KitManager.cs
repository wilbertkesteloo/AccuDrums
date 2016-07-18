using Accudrums.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Accudrums {
    internal class KitManager {
        private Plugin _plugin;

        private Kit TR909 = new Kit() {
            Name = "Basic Roland Tr909",
            ID = 0,
            Grid = new Grid() {
                GridItems = new List<GridItem>() {
                    new GridItem() { ID=1, X=0,Y=0,Name="Bass Drum",Note=35, Samples= new List<Sample>() { new Sample(35, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_BD01.wav") } },
                    new GridItem() { ID=2, X=1,Y=0,Name="E-Bass Drum",Note=36, Samples= new List<Sample>() { new Sample(36, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_BD01.wav") } },
                    new GridItem() { ID=3, X=2,Y=0,Name="Pedal-HH",Note=44, Samples= new List<Sample>() { new Sample(44, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HH01.wav") } },
                    new GridItem() { ID=4, X=3,Y=0,Name="Closed-HH",Note=42, Samples= new List<Sample>() { new Sample(42, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HH07.wav") } },
                    new GridItem() { ID=5, X=0,Y=1,Name="Open-HH",Note=46, Samples= new List<Sample>() { new Sample(46, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHO1.wav") } },
                    new GridItem() { ID=6, X=1,Y=1,Name="Crash 1",Note=49, Samples= new List<Sample>() { new Sample(49, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ CRSH1.wav") } },
                    new GridItem() { ID=7, X=2,Y=1,Name="Crash 2",Note=57, Samples= new List<Sample>() { new Sample(57, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ CRSH2.wav") } },
                    new GridItem() { ID=8, X=3,Y=1,Name="Ride 1",Note=51, Samples= new List<Sample>() { new Sample(51, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_HHZ RIDE1.wav") } },
                    new GridItem() { ID=9, X=0,Y=2,Name="Snare Drum",Note=38, Samples= new List<Sample>() { new Sample(38, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_SD 01.wav") } },
                    new GridItem() { ID=10, X=1,Y=2,Name="Handclap",Note=40, Samples= new List<Sample>() { new Sample(40, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_SD CLAP1.wav") } },
                    new GridItem() { ID=11, X=2,Y=2,Name="High Tom",Note=50, Samples= new List<Sample>() { new Sample(50, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM HI1.wav") } },
                    new GridItem() { ID=12, X=3,Y=2,Name="Mid Tom",Note=48, Samples= new List<Sample>() { new Sample(48, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM ME1.wav") } },
                    new GridItem() { ID=13, X=0,Y=3,Name="Low Tom",Note=45, Samples= new List<Sample>() { new Sample(45, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-909\TR909_TOM LO1.wav") } },
                },
                XSize = 4,
                YSize = 4,
            }
        };

        private Kit TR808 = new Kit() {
            Name = "Basic Roland Tr-808",
            ID = 1,
            Grid = new Grid() {
                GridItems = new List<GridItem>() {
                    new GridItem() { ID=1,  X=0,Y=0,Name="Bass Drum",Note=35, Samples= new List<Sample>() { new Sample(35, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Kick1.mp3") } },
                    new GridItem() { ID=2,  X=1,Y=0,Name="Clap",Note=39, Samples= new List<Sample>() { new Sample(39, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Clap.mp3") } },
                    new GridItem() { ID=3,  X=2,Y=0,Name="Clav",Note=75, Samples= new List<Sample>() { new Sample(75, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Clav.mp3") } },
                    new GridItem() { ID=4,  X=3,Y=0,Name="Closed-HH",Note=42, Samples= new List<Sample>() { new Sample(42, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Closed Hat.mp3") } },
                    new GridItem() { ID=5,  X=0,Y=1,Name="Cowbell",Note=56, Samples= new List<Sample>() { new Sample(46, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Cow Bell.mp3") } },
                    new GridItem() { ID=6,  X=1,Y=1,Name="Cymbal short",Note=55, Samples= new List<Sample>() { new Sample(55, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Cymbal Short.mp3") } },
                    new GridItem() { ID=7,  X=2,Y=1,Name="Cymbal",Note=49, Samples= new List<Sample>() { new Sample(49, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Cymbal.mp3") } },
                    new GridItem() { ID=8,  X=3,Y=1,Name="Ride 1",Note=63, Samples= new List<Sample>() { new Sample(63, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Hi Conga.mp3") } },
                    new GridItem() { ID=9,  X=0,Y=2,Name="Snare Drum",Note=70, Samples= new List<Sample>() { new Sample(70, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Maracas.mp3") } },
                    new GridItem() { ID=10, X=1,Y=2,Name="Handclap",Note=64, Samples= new List<Sample>() { new Sample(40, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Mid Conga.mp3") } },
                    new GridItem() { ID=11, X=2,Y=2,Name="High Tom",Note=46, Samples= new List<Sample>() { new Sample(46, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Open Hat.mp3") } },
                    new GridItem() { ID=12, X=3,Y=2,Name="Rim Shot",Note=38, Samples= new List<Sample>() { new Sample(38, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Rim Shot.mp3") } },
                    new GridItem() { ID=13, X=0,Y=3,Name="Snare Tom 1",Note=50, Samples= new List<Sample>() { new Sample(50, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Snare Tom1.mp3") } },
                    new GridItem() { ID=14, X=1,Y=3,Name="Snare Tom 2",Note=48, Samples= new List<Sample>() { new Sample(48, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Snare Tom2.mp3") } },
                    new GridItem() { ID=15, X=2,Y=3,Name="Snare 1",Note=40, Samples= new List<Sample>() { new Sample(40, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Snare1.mp3") } },
                    new GridItem() { ID=16, X=3,Y=3,Name="Snare 2",Note=41, Samples= new List<Sample>() { new Sample(41, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Snare2.mp3") } },
                    new GridItem() { ID=17, X=0,Y=4,Name="Tom",Note=47, Samples= new List<Sample>() { new Sample(47, @"C:\Github\repositories\AccuDrums\AccuDrumsPlugin\Kits\TR-808\Tom.mp3") } },
                },
                XSize = 4,
                YSize = 4,
            }
        };

        public List<Kit> Kits { get; set; }

        public KitManager(Plugin plugin) {
            _plugin = plugin;

            //Load basic Tr909 drumkit
            LoadKit(TR909);

            //Create KitList
            Kits = new List<Kit>() { TR808, TR909 };

            //Load Kits in UI
            _plugin.PluginEditor.SetComboBoxKitList(Kits);
        }

        /// <summary>
        /// Sets up kit in entire application
        /// </summary>
        /// <param name="kit"></param>
        public void LoadKit(Kit kit) {
            //Load visual items in PluginEditor
            _plugin.PluginEditor.SetCurrentKit(kit);

            //Load samples into samplemanager
            _plugin.SampleManager.LoadSamples(kit.Grid.GridItems);
        }

        /// <summary>
        /// Sets up kit in entire application
        /// </summary>
        /// <param name="kit"></param>
        public void LoadKitByID(int id) {
            var kit = Kits.FirstOrDefault(k => k.ID == id);

            LoadKit(kit);
        }

    }
}
