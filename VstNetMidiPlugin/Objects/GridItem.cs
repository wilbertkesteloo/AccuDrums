using System.Collections.Generic;

namespace Accudrums.Objects {
    public class GridItem {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public byte Note { get; set; }
        public List<Sample> Samples { get; set; }
    }
}
