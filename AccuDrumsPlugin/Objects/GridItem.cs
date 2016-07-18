using System.Collections.Generic;

namespace Accudrums.Objects {
    public class GridItem {
        public int ID { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte Note { get; set; }
        public List<Sample> Samples { get; set; }

        public float Gain { get; set; } 
        public int Panning { get; set; }
    }
}
