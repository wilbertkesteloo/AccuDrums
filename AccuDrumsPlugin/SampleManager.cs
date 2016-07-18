﻿using System;
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
        private Dictionary<byte, StereoBuffer> _noteMap = new Dictionary<byte, StereoBuffer>();

        public SampleManager(Plugin plugin) {
            _plugin = plugin;
        }

        private StereoBuffer SetSampleBuffer(byte note, string file) {
            var kickbuffer = new StereoBuffer(note);

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

        internal void LoadSamples(List<GridItem> gridItems) {
            _noteMap.Clear();

            foreach (var gridItem in gridItems) {
                var buffer = SetSampleBuffer(gridItem.Note, gridItem.Samples.FirstOrDefault().File);
                _noteMap.Add(gridItem.Note, buffer);
            }
        }

        /// <summary>
        /// Starts recording the current audio or playing back the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOnEvent(byte noteNo) {
            if (_noteMap.ContainsKey(noteNo)) {
                _plugin.PluginEditor.SetItemActive(noteNo);
                _player = new SamplePlayer(_noteMap[noteNo]);
            }
        }

        /// <summary>
        /// Stops recording the current audio or playing back the the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOffEvent(byte noteNo) {
            _plugin.PluginEditor.SetItemInactive(noteNo);
            if (_player != null && _player.Buffer.NoteNo == noteNo) {
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

                //Add effects per gridItem here (gain, panning)
                //per effect waarschijnlijk class schrijven die de verschillende channels processed
                //Hier is ook informatie nodig van de CurrentKit uit de kitmanager

                //Gain



                //Panning

                _player.Play(channels[0], channels[1]);

                if (_player != null && _player.IsFinished) {
                    _player = null;
                }
            }
        }

        /// <summary>
        /// Manages playing back a sample buffer
        /// </summary>
        private class SamplePlayer {
            public SamplePlayer(StereoBuffer buffer) {
                Buffer = buffer;
            }

            private int _bufferIndex;
            public StereoBuffer Buffer { get; private set; }

            public void Play(VstAudioBuffer left, VstAudioBuffer right) {
                if (IsFinished) return;

                int count = Math.Min(left.SampleCount, Buffer.LeftSamples.Count - _bufferIndex);

                for (int index = 0; index < count; index++) {
                    left[index] = Buffer.LeftSamples[_bufferIndex + index];
                }

                for (int index = 0; index < count; index++) {
                    right[index] = Buffer.RightSamples[_bufferIndex + index];
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
            public StereoBuffer(byte noteNo) {
                NoteNo = noteNo;
            }

            public byte NoteNo;
            public List<float> LeftSamples = new List<float>();
            public List<float> RightSamples = new List<float>();
        }
    }
}
