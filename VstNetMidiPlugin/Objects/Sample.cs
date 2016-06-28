namespace Accudrums.Objects {
    public class Sample {
        public Sample(byte note, string file) {
            Note = note;
            File = file;
        }

        public byte Note { get; set; }
        public string File { get; set; }
    }
}
