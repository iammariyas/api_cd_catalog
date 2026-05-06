namespace CDCatalog.Tests;

using NUnit.Framework;
using Moq;
using CDCatalog.Application.AddCD;
using CDCatalog.Application.DeleteCD;
using CDCatalog.Application.AddSong;
using CDCatalog.Application.DeleteSong;
using CDCatalog.Application.Search;
using CDCatalog.Application.ViewersCatalog;
using CDCatalog.Application.ViewersCD;
using CDCatalog.Domain.Entities;
using CDCatalog.Domain.Interfaces;

public class ApplicationTests
{
    [Test]
    public async Task AddCD_ShouldWork()
    {
        var mockRepo = new Mock<ICDRepository>();
        var handler = new AddCDCommandHandler(mockRepo.Object);
        
        var command = new AddCDCommand
        {
            Title = "Test CD",
            Artist = "Test Artist",
            Year = 2024
        };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Test CD"));
        
        mockRepo.Verify(x => x.AddCDAsync(It.IsAny<CD>()), Times.Once);
    }

    [Test]
    public async Task DeleteCD_ShouldWork()
    {
        var mockRepo = new Mock<ICDRepository>();
        var handler = new DeleteCDCommandHandler(mockRepo.Object);
        
        var command = new DeleteCDCommand { Id = 1 };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
        mockRepo.Verify(x => x.DeleteCDAsync(1), Times.Once);
    }

    [Test]
    public async Task AddSong_ShouldWork()
    {
        var cd = new CD("Test CD", "Test Artist", 2024);
        
        var mockRepo = new Mock<ICDRepository>();
        mockRepo.Setup(x => x.GetCDByIdAsync(It.IsAny<int>())).ReturnsAsync(cd);
        
        var handler = new AddSongCommandHandler(mockRepo.Object);
        
        var command = new AddSongCommand
        {
            CDId = 1,
            Title = "Test Song",
            FirstName_Artist = "John",
            LastName_Artist = "Doe",
            Duration = 180
        };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Title, Is.EqualTo("Test Song"));
        mockRepo.Verify(x => x.AddSongAsync(It.IsAny<Song>()), Times.Once);
    }

    [Test]
    public async Task DeleteSong_ShouldWork()
    {
        var cd = new CD("Test CD", "Test Artist", 2024);
        var song = new Song("Test Song", "John", "Doe", 180);
        song.Id = 1;
        cd.Songs.Add(song);
        
        var mockRepo = new Mock<ICDRepository>();
        mockRepo.Setup(x => x.GetCDByIdAsync(It.IsAny<int>())).ReturnsAsync(cd);
        
        var handler = new DeleteSongCommandHandler(mockRepo.Object);
        var command = new DeleteSongCommand { CDId = 1, SongId = 1 };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
        mockRepo.Verify(x => x.DeleteSongAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetAllCDs_ShouldWork()
    {
        var expectedCDs = new List<CD>
        {
            new CD("CD1", "Artist1", 2020),
            new CD("CD2", "Artist2", 2021)
        };
        
        var mockRepo = new Mock<ICDRepository>();
        mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedCDs);
        
        var handler = new GetAllCDsQueryHandler(mockRepo.Object);
        var query = new GetAllCDsQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        mockRepo.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetCDById_ShouldWork()
    {
        var expectedCD = new CD("Test CD", "Test Artist", 2024);
        
        var mockRepo = new Mock<ICDRepository>();
        mockRepo.Setup(x => x.GetCDByIdAsync(1)).ReturnsAsync(expectedCD);
        
        var handler = new GetCDByIdQueryHandler(mockRepo.Object);
        var query = new GetCDByIdQuery { Id = 1 };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        mockRepo.Verify(x => x.GetCDByIdAsync(1), Times.Once);
    }

    [Test]
    public async Task SearchSongs_ShouldWork()
    {
        var expectedSongs = new List<Song>
        {
            new Song("Song1", "John", "Lennon", 180),
            new Song("Song2", "John", "Lennon", 200)
        };
        
        var mockRepo = new Mock<ICDRepository>();
        mockRepo.Setup(x => x.GetSongsByArtistAsync("John", "Lennon"))
                .ReturnsAsync(expectedSongs);
        
        var handler = new SearchSongsQueryHandler(mockRepo.Object);
        var query = new SearchSongsQuery { FirstName = "John", LastName = "Lennon" };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
        mockRepo.Verify(x => x.GetSongsByArtistAsync("John", "Lennon"), Times.Once);
    }
}