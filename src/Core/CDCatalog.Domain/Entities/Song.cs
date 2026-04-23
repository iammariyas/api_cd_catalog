using CDCatalog.Domain.Entities;
namespace CDCatalog.Domain.Entities
{
    public class Song
    {
        public string Title { get; set; } = "";
        public string FirstName_Artist { get; set; } = "";
        public string LastName_Artist { get; set; } = "";
        public int Id { get; set; }
        public int Duration { get; set; }
        public int CDId { get; set; }
        
        public Song() { }

        public Song(string title, string firstName_Artist, string lastName_Artist, int duration) 
        {
            Title = title;
            FirstName_Artist = firstName_Artist;
            LastName_Artist = lastName_Artist;
            Duration = duration;
        }
    }
}