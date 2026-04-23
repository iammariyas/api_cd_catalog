using CDCatalog.Domain.Entities;
namespace CDCatalog.Domain.Interfaces
{
    public interface ICDRepository
    {
        Task<CD?> GetCDByIdAsync(int id);
        Task<CD?> GetByTitleAsync(string title);
        Task<List<CD>> GetAllAsync();
        Task AddCDAsync(CD cd);
        Task UpdateCDAsync(CD cd);
        Task DeleteCDAsync(int id);

        Task<List<Song>> GetSongsByCDIdAsync(int CDId);
        Task<List<Song>> GetSongsByArtistAsync(string firstName, string lastName);
        Task AddSongAsync(Song song);
        Task DeleteSongAsync(int songId);
    }
}