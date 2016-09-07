using System.Collections.Generic;

namespace Accudrums.Objects {
    public class GridItem {
        public int ID { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte Note { get; set; }
        public List<Sample> Samples { get; set; }

        /// <summary>
        /// The Gain value of the samples, in dB values 
        /// </summary>
        public float Gain { get; set; } 

        /// <summary>
        /// Set the panning, is a value between -1 and 1
        /// -1 is total left, 1 is total right and 0.0 is center
        /// </summary>
        public float Panning { get; set; }
    }
}
