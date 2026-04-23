using CDCatalog.Domain.Entities;

namespace CDCatalog.Domain.Entities {
    public sealed class CD
    {
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public int Year { get; set; }
        public int Id { get; set; }
        public List<Song> Songs { get; set; } = new();


        public CD() { }

        public CD(string title, string artist, int year) 
        {
            Title = title;
            Artist = artist;
            Year = year;
            Songs = new List<Song>();
        }
    }
}
