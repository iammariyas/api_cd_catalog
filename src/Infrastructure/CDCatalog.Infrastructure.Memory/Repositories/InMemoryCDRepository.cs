using CDCatalog.Domain.Entities;
using CDCatalog.Domain.Interfaces;
using Newtonsoft.Json;

namespace CDCatalog.Infrastructure.Repositories;

public class InMemoryCDRepository : ICDRepository
{
    private static List<CD> _cds = new();
    private static int _nextCdId = 1;
    private static int _nextSongId = 1;

    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cdcatalog.json");

    public InMemoryCDRepository()
    {
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var data = JsonConvert.DeserializeObject<RepositoryData>(json);
                if (data != null)
                {
                    _cds = data.CDs ?? new List<CD>();
                    _nextCdId = data.NextCdId;
                    _nextSongId = data.NextSongId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
        }
    }

    private void SaveToFile()
    {
        var data = new RepositoryData
        {
            CDs = _cds,
            NextCdId = _nextCdId,
            NextSongId = _nextSongId
        };

        try
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }
    
    public Task<List<CD>> GetAllAsync()
    {
        return Task.FromResult(_cds);
    }

    public Task<CD?> GetCDByIdAsync(int id)
    {
        var cd = _cds.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(cd);
    }

    public Task<CD?> GetByTitleAsync(string title)
    {
        var cd = _cds.FirstOrDefault(c => c.Title.ToLower() == title.ToLower());
        return Task.FromResult(cd);
    }

    public Task AddCDAsync(CD cd)
    {
        cd.Id = _nextCdId++;
        _cds.Add(cd);
        SaveToFile();
        return Task.CompletedTask;
    }

    public Task UpdateCDAsync(CD cd)
    {
        var index = _cds.FindIndex(c => c.Id == cd.Id);
        if (index != -1)
            _cds[index] = cd;
        SaveToFile();
        return Task.CompletedTask;
    }

    public Task DeleteCDAsync(int id)
    {
        var cd = _cds.FirstOrDefault(c => c.Id == id);
        if (cd != null)
            _cds.Remove(cd);
        SaveToFile();
        return Task.CompletedTask;
    }

    public Task<List<Song>> GetSongsByCDIdAsync(int CDId)
    {
        var cd = _cds.FirstOrDefault(c => c.Id == CDId);
        var songs = cd?.Songs ?? new List<Song>();
        return Task.FromResult(songs);
    }

    public Task<List<Song>> GetSongsByArtistAsync(string firstName, string lastName)
    {
        var allSongs = _cds.SelectMany(c => c.Songs);
        var result = allSongs.Where(s => 
            s.FirstName_Artist.ToLower() == firstName.ToLower() && 
            s.LastName_Artist.ToLower() == lastName.ToLower()
        ).ToList();
        return Task.FromResult(result);
    }

    public Task AddSongAsync(Song song)
    {
        song.Id = _nextSongId++;
        SaveToFile();
        return Task.CompletedTask;
    }

    public Task DeleteSongAsync(int songId)
    {
        foreach (var cd in _cds)
        {
            var song = cd.Songs.FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                cd.Songs.Remove(song);
                break;
            }
        }
        SaveToFile();
        return Task.CompletedTask;
    }
}

public class RepositoryData
{
    public List<CD> CDs { get; set; } = new();
    public int NextCdId { get; set; }
    public int NextSongId { get; set; }
}