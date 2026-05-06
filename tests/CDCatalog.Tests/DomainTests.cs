namespace CDCatalog.Tests;

using CDCatalog.Domain.Entities;

public class DomainTests
{
    [Test]
    public void CreateCD_Works()
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        
        Assert.That(cd, Is.Not.Null);
        Assert.That(cd.Title, Is.EqualTo("Abbey Road"));
        Assert.That(cd.Artist, Is.EqualTo("The Beatles"));
        Assert.That(cd.Year, Is.EqualTo(1969));
    }

    [Test]
    public void CreateSong_Works()
    {
        var song = new Song("Come Together", "John", "Lennon", 259);
        
        Assert.That(song, Is.Not.Null);
        Assert.That(song.Title, Is.EqualTo("Come Together"));
        Assert.That(song.FirstName_Artist, Is.EqualTo("John"));
        Assert.That(song.LastName_Artist, Is.EqualTo("Lennon"));
        Assert.That(song.Duration, Is.EqualTo(259));
    }

    [Test]
    public void AddSongToCD_Works()
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        var song = new Song("Come Together", "John", "Lennon", 259);
        
        cd.Songs.Add(song);
        
        Assert.That(cd.Songs.Count, Is.EqualTo(1));
    }

    [Test]
    public void DeleteSongFromCD_Works()
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        var song = new Song("Come Together", "John", "Lennon", 259);
        cd.Songs.Add(song);
        
        cd.Songs.Remove(song);
        
        Assert.That(cd.Songs.Count, Is.EqualTo(0));
    }

    [TestCase("John", "Lennon", 2)]
    [TestCase("Paul", "McCartney", 1)]
    public void SearchSongsByArtist_Works(string firstName, string lastName, int expectedCount)
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        cd.Songs.Add(new Song("Come Together", "John", "Lennon", 259));
        cd.Songs.Add(new Song("Imagine", "John", "Lennon", 183));
        cd.Songs.Add(new Song("Hey Jude", "Paul", "McCartney", 431));
        
        var found = cd.Songs.Where(s => 
            s.FirstName_Artist == firstName && s.LastName_Artist == lastName).ToList();
        
        Assert.That(found.Count, Is.EqualTo(expectedCount));
    }

    [Test]
    public void SortSongsByTitle_Works()
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        cd.Songs.Add(new Song("Zebra", "A", "A", 100));
        cd.Songs.Add(new Song("Apple", "B", "B", 100));
        cd.Songs.Add(new Song("Music", "C", "C", 100));
        
        var sorted = cd.Songs.OrderBy(s => s.Title).ToList();
        
        Assert.That(sorted[0].Title, Is.EqualTo("Apple"));
        Assert.That(sorted[1].Title, Is.EqualTo("Music"));
        Assert.That(sorted[2].Title, Is.EqualTo("Zebra"));
    }

    [Test]
    public void SortSongsByArtist_Works()
    {
        var cd = new CD("Abbey Road", "The Beatles", 1969);
        cd.Songs.Add(new Song("Song1", "Paul", "McCartney", 100));
        cd.Songs.Add(new Song("Song2", "George", "Harrison", 100));
        cd.Songs.Add(new Song("Song3", "John", "Lennon", 100));
        
        var sorted = cd.Songs.OrderBy(s => s.LastName_Artist).ToList();
        
        Assert.That(sorted[0].LastName_Artist, Is.EqualTo("Harrison"));
        Assert.That(sorted[1].LastName_Artist, Is.EqualTo("Lennon"));
        Assert.That(sorted[2].LastName_Artist, Is.EqualTo("McCartney"));
    }
}
