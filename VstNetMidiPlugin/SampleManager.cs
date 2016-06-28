using System;
using System.Collections.Generic;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using System.IO;
using System.Linq;
using Accudrums.Objects;
using NAudio.Wave;

namespace Accudrums {
    /// <summary>
    /// Manages playback, recording and storing audio samples.
    /// </summary>
    internal class SampleManager {
        private Dictionary<byte, StereoBuffer> _noteMap = new Dictionary<byte, StereoBuffer>();

        //private const string strKickdrum = @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\bd2.wav";
        //private const string strCrash1 = @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\Crash-Cymbal-1.wav";

        private Kit TR909 = new Kit() {
            Name = "Roland Tr909",
            Samples = new List<Sample>() {
                new Sample(35, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_BD01.wav"),
                new Sample(36, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_BD01.wav"),
                new Sample(44, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HH01.wav"),
                new Sample(42, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HH07.wav"),
                new Sample(46, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HHO1.wav"),
                new Sample(49, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HHZ CRSH1.wav"),
                new Sample(57, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HHZ CRSH2.wav"),
                new Sample(51, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_HHZ RIDE1.wav"),
                new Sample(38, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_SD 01.wav"),
                new Sample(40, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_SD CLAP1.wav"),
                new Sample(50, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_TOM HI1.wav"),
                new Sample(48, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_TOM ME1.wav"),
                new Sample(45, @"C:\Github\repositories\AccuDrums\VstNetMidiPlugin\Kits\TR-909\TR909_TOM LO1.wav"),
           }
        };

        public SampleManager() {
            //Tr909 kit laden
            //           LoadKit(TR909);

            //    //Add kickdrum to Note 36
            //    var kickbuffer = SetSampleBuffer(36, strKickdrum);
            //    _noteMap.Add(36, kickbuffer);

            //    //Add chrash to note 49
            //    var crashbuffer = SetSampleBuffer(49, strCrash1);
            //    _noteMap.Add(49, crashbuffer);
        }

        private void LoadKit(Kit kit) {
            //Empty notemap before loading new kit
            _noteMap.Clear();

            foreach (var sample in kit.Samples) {
                //Add sample to note
                var crashbuffer = SetSampleBuffer(sample.Note, sample.File);
                _noteMap.Add(sample.Note, crashbuffer);
            }

        }

        public float[] ConvertByteToFloat(byte[] array) {
            float[] floatArr = new float[array.Length / 4];
            for (int i = 0; i < floatArr.Length; i++) {
                if (BitConverter.IsLittleEndian) {
                    Array.Reverse(array, i * 4, 4);
                }
                floatArr[i] = BitConverter.ToSingle(array, i * 4);
            }
            return floatArr;
        }

        private bool readWav(string filename, out float[] L, out float[] R) {
            L = R = null;
            //float [] left = new float[1];
            using (FileStream fs = File.Open(filename, FileMode.Open)) {
                BinaryReader reader = new BinaryReader(fs);

                // chunk 0
                int chunkID = reader.ReadInt32();
                int fileSize = reader.ReadInt32();
                int riffType = reader.ReadInt32();
                int fmtID = reader.ReadInt32();
                int fmtSize = reader.ReadInt32();
                int fmtCode = reader.ReadInt16();
                int channels = reader.ReadInt16();
                int sampleRate = reader.ReadInt32();
                int fmtAvgBPS = reader.ReadInt32();
                int fmtBlockAlign = reader.ReadInt16();
                int bitDepth = reader.ReadInt16();
                if (fmtSize == 18) {
                    int fmtExtraSize = reader.ReadInt16();
                    reader.ReadBytes(fmtExtraSize);
                }

                int dataID = reader.ReadInt32();
                int dataSize = reader.ReadInt32();
                byte[] byteArray = reader.ReadBytes(dataSize);

                int bytesForSamp = bitDepth / 8;
                int samps = dataSize / bytesForSamp;


                float[] asFloat = null;

                switch (bitDepth) {
                    case 64:
                        double[]
                        asDouble = new double[samps];
                        Buffer.BlockCopy(byteArray, 0, asDouble, 0, dataSize);
                        asFloat = Array.ConvertAll(asDouble, e => (float)e);
                        break;
                    case 32:
                        asFloat = new float[samps];
                        Buffer.BlockCopy(byteArray, 0, asFloat, 0, dataSize);
                        break;
                    case 16:
                        Int16[]
                        asInt16 = new Int16[samps];
                        Buffer.BlockCopy(byteArray, 0, asInt16, 0, dataSize);
                        asFloat = Array.ConvertAll(asInt16, e => e / (float)Int16.MaxValue);
                        break;
                    default:
                        return false;
                }

                switch (channels) {
                    case 1:
                        L = asFloat;
                        R = asFloat;
                        //R = null; //is hier null gezet omdat audioprocessor niet tegen lege buffers kan
                        return true;
                    case 2:
                        L = asFloat;
                        R = asFloat;
                        return true;
                    default:
                        return false;
                }
            }
        }

        unsafe private StereoBuffer SetSampleBuffer(byte note, string file) {
            var kickbuffer = new StereoBuffer(note);

            float[] leftList = null;

            using (var reader = new WaveFileReader(file)) {
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
        /// Starts recording the current audio or playing back the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOnEvent(byte noteNo) {

            LoadKit(TR909);

            if (_noteMap.ContainsKey(noteNo)) {
                _player = new SamplePlayer(_noteMap[noteNo]);
            }
        }

        /// <summary>
        /// Stops recording the current audio or playing back the the sample buffer.
        /// </summary>
        /// <param name="noteNo">The midi note number.</param>
        public void ProcessNoteOffEvent(byte noteNo) {
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

        private int playnumber = 0;

        /// <summary>
        /// Plays back the current sample buffer
        /// </summary>
        /// <param name="channels">Output buffers. Must not be null.</param>
        public void PlayAudio(VstAudioBuffer[] channels) {

            if (IsPlaying) {
                _player.Play(channels[0], channels[1]);
                playnumber++;

                if (_player.IsFinished) {
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
