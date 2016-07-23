using System;
using System.Collections.Generic;
using Jacobi.Vst.Core;
using System.Linq;
using Accudrums.Objects;
using NAudio.Wave;

namespace Accudrums {
    /// <summary>
    /// Manages playback, recording and storing audio samples.
    /// </summary>
    internal class SampleManager {
        private Plugin _plugin;
        private Dictionary<byte, GridItem> _noteMap = new Dictionary<byte, GridItem>();
        private List<StereoBuffer> _stereoBuffers = new List<StereoBuffer>();

        public SampleManager(Plugin plugin) {
            _plugin = plugin;
        }

        internal void LoadSamples(List<GridItem> gridItems) {
            _noteMap.Clear();

            foreach (var gridItem in gridItems) {
                _noteMap.Add(gridItem.Note, gridItem);
                _stereoBuffers.Add(CreateSampleBuffer(gridItem.ID, gridItem.Samples.FirstOrDefault().File));
            }
        }

        /// <summary>
        /// Starts recording the current audio or playing back the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOnEvent(byte noteNo) {
            if (_noteMap.ContainsKey(noteNo)) {
                _plugin.PluginEditor.SetItemActive(noteNo);

                var gridItem = _noteMap[noteNo];
                var stereobuffer = _stereoBuffers.FirstOrDefault(b => b.GridItemId == gridItem.ID);
                
                _player = new SamplePlayer(gridItem, stereobuffer);
            }
        }

        /// <summary>
        /// Stops recording the current audio or playing back the the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOffEvent(byte noteNo) {
            _plugin.PluginEditor.SetItemInactive(noteNo);

            var gridItem = _noteMap[noteNo];
            if (_player != null && _player.Buffer.GridItemId == gridItem.ID) {
                _player = null;
            }
        }

        private SamplePlayer _player;
        /// <summary>
        /// Indicates if a recorded sample buffer is being played back.
        /// </summary>
        public bool IsPlaying {
            get { return _player != null; }
        }

        /// <summary>
        /// Plays back the current sample buffer
        /// </summary>
        /// <param name="channels">Output buffers. Must not be null.</param>
        public void PlayAudio(VstAudioBuffer[] channels) {

            if (IsPlaying) {
                _player.Play(channels[0], channels[1]);

                if (_player != null && _player.IsFinished) {
                    _player = null;
                }
            }
        }

        /// <summary>
        /// Returns a Stereobuffer class by input of a file
        /// </summary>
        /// <param name="note">note (nog nodig?)</param>
        /// <param name="file">location of audio file</param>
        /// <returns></returns>
        private StereoBuffer CreateSampleBuffer(int gridItemId, string file) {
            var kickbuffer = new StereoBuffer(gridItemId);

            float[] leftList = null;

            using (var reader = new AudioFileReader(file)) {
                leftList = new float[reader.Length];
                reader.ToSampleProvider().Read(leftList, 0, leftList.Length);
            }

            //readWav(file, out leftList, out richtList);
            if (leftList != null) {
                kickbuffer.LeftSamples = leftList.ToList();
                kickbuffer.RightSamples = leftList.ToList();
            }

            return kickbuffer;
        }

        /// <summary>
        /// Manages playing back a sample buffer
        /// </summary>
        private class SamplePlayer {

            private int _bufferIndex;
            public StereoBuffer Buffer { get; private set; }
            public GridItem GridItem { get; private set; }

            public SamplePlayer(GridItem gridItem, StereoBuffer buffer) {
                GridItem = gridItem;
                Buffer = buffer;
            }

            public void Play(VstAudioBuffer left, VstAudioBuffer right) {
                if (IsFinished) return;

                //Add effects per gridItem here (gain, panning)
                //per effect waarschijnlijk class schrijven die de verschillende channels processed
                //Hier is ook informatie nodig van de CurrentKit uit de kitmanager

                int count = Math.Min(left.SampleCount, Buffer.LeftSamples.Count - _bufferIndex);
                double gain_factor = Math.Pow(10.0, GridItem.Gain / 20.0);


                for (int index = 0; index < count; index++) {
                    float signal = Buffer.LeftSamples[_bufferIndex + index];

                    //Add Gain
                    signal = signal * (float)gain_factor;

                    //Panning


                    left[index] = signal;
                }

                for (int index = 0; index < count; index++) {
                    float signal = Buffer.RightSamples[_bufferIndex + index];
                    //Add Gain
                    signal = signal * (float)gain_factor;


                    //Panning

                    right[index] = signal;
                }

                _bufferIndex += left.SampleCount;
            }


            public bool IsFinished {
                get { return (_bufferIndex >= Buffer.LeftSamples.Count); }
            }
        }


        private byte[] adjustVolume(byte[] audioSamples, float volume) {
            byte[] array = new byte[audioSamples.Length];
            for (int i = 0; i < array.Length; i += 2) {
                // convert byte pair to int
                short buf1 = audioSamples[i + 1];
                short buf2 = audioSamples[i];

                buf1 = (short)((buf1 & 0xff) << 8);
                buf2 = (short)(buf2 & 0xff);

                short res = (short)(buf1 | buf2);
                res = (short)(res * volume);

                // convert back
                array[i] = (byte)res;
                array[i + 1] = (byte)(res >> 8);

            }
            return array;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Manages a stereo sample buffer for a specific note number.
        /// </summary>
        private class StereoBuffer {
            public StereoBuffer(int gridItemID) {
                GridItemId = gridItemID;
            }

            public int GridItemId;
            public List<float> LeftSamples = new List<float>();
            public List<float> RightSamples = new List<float>();
        }
    }
}
