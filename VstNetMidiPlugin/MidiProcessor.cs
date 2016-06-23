using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using Accudrums.Dmp;
using System.Linq;
using System.Media;

namespace Accudrums {
    /// <summary>
    /// This object performs midi processing for your plugin.
    /// </summary>
    internal sealed class MidiProcessor : IVstMidiProcessor, IVstPluginMidiSource {
        private Plugin _plugin;

        /// <summary> 
        /// Constructs a new Midi Processor.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public MidiProcessor(Plugin plugin) {
            _plugin = plugin;
            Gain = new Gain(plugin);
            Transpose = new Transpose(plugin);

            // for most hosts, midi output is expected during the audio processing cycle.
            SyncWithAudioProcessor = true;
        }

        internal Gain Gain { get; private set; }
        internal Transpose Transpose { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating to sync with audio processing.
        /// </summary>
        /// <remarks>
        /// False: will output midi to the host in the MidiProcessor.
        /// True: will output midi to the host in the AudioProcessor.
        /// </remarks>
        public bool SyncWithAudioProcessor { get; set; }

        public int ChannelCount {
            get { return 16; }
        }

        /// <summary>
        /// Midi events are received from the host on this method.
        /// </summary>
        /// <param name="events">A collection with midi events. Never null.</param>
        /// <remarks>
        /// Note that some hosts will only receieve midi events during audio processing.
        /// See also <see cref="IVstPluginAudioProcessor"/>.
        /// </remarks>
        public void Process(VstEventCollection events) {
            CurrentEvents = events;

            if (!SyncWithAudioProcessor) {
                ProcessCurrentEvents();
            }
        }

        // cache of events (for when syncing up with the AudioProcessor).
        public VstEventCollection CurrentEvents { get; private set; }

        public void ProcessCurrentEvents() {
            if (CurrentEvents == null || CurrentEvents.Count == 0) return;

            // a plugin must implement IVstPluginMidiSource or this call will throw an exception.
            IVstMidiProcessor midiHost = _plugin.Host.GetInstance<IVstMidiProcessor>();

            //Hieronder een implementatie van een Audio-Uit of audioprocessor opvragen


            // always expect some hosts not to support this.
            if (midiHost != null) {
                VstEventCollection outEvents = new VstEventCollection();

                // NOTE: other types of events could be in the collection!
                foreach (VstEvent evnt in CurrentEvents) {
                    switch (evnt.EventType) {
                        case VstEventTypes.MidiEvent:
                            VstMidiEvent midiEvent = (VstMidiEvent)evnt;

                            //General midi effects for all inputs 
                            midiEvent = Gain.ProcessEvent(midiEvent);
                            midiEvent = Transpose.ProcessEvent(midiEvent);

                            //Hier ergens midi noot koppelen aan audio output
                            if (MidiHelper.IsNoteOn(midiEvent.Data)) {
                                _plugin.PluginEditor.CurrentNote("ON " + midiEvent.Data[1].ToString() + " " + midiEvent.Data[2].ToString() + " " + midiEvent.Data[3].ToString());

                                if (midiEvent.Data[1] == 60) {
                                    //kickdrum afspelen
                                    _plugin.PluginEditor.PlayKick();

                                    //Audio event hier uitsturen?? hoe??
                                    SoundPlayer player = new SoundPlayer(@"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Sounds\bd2.wav");
                                    player.Play();
                                }

                            } else if (MidiHelper.IsNoteOff(midiEvent.Data)) {
                                _plugin.PluginEditor.CurrentNote("OFF " + midiEvent.Data[1].ToString() + " " + midiEvent.Data[2].ToString() + " " + midiEvent.Data[3].ToString());

                                if (midiEvent.Data[1] == 60) {
                                    //kickdrum stoppen
                                    _plugin.PluginEditor.StopKick();
                                }
                            }

                            outEvents.Add(midiEvent);
                            break;
                        default:
                            // non VstMidiEvent
                            outEvents.Add(evnt);
                            break;
                    }
                }

                midiHost.Process(outEvents);

                //Hier events naar een audiohost sturen (denk ik??)

            }

            // Clear the cache, we've processed the events.
            CurrentEvents = null;
        }

        #region IVstPluginMidiSource Members

        int IVstPluginMidiSource.ChannelCount {
            get { return 16; }
        }

        #endregion
    }
}
