using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace Accudrums {
    /// <summary>
    /// Implements the audio processing of the plugin using the <see cref="SampleManager"/>.
    /// </summary>
    internal class AudioProcessor : VstPluginAudioProcessorBase {
        private Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(Plugin plugin)
            : base(2, 2, 0) {
            _plugin = plugin;
        }

        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels) {
            // check to see if we need to output midi here
            if (_plugin.MidiProcessor.SyncWithAudioProcessor) {
                _plugin.MidiProcessor.ProcessCurrentEvents();
            }

            if (_plugin.SampleManager.IsPlaying) {
                _plugin.SampleManager.PlayAudio(outChannels);
            } else // audio thru
              {
                VstAudioBuffer input = inChannels[0];
                VstAudioBuffer output = outChannels[0];

                for (int index = 0; index < output.SampleCount; index++) {
                    output[index] = input[index];
                }

                input = inChannels[1];
                output = outChannels[1];

                for (int index = 0; index < output.SampleCount; index++) {
                    output[index] = input[index];
                }
            }
        }
    }
}
