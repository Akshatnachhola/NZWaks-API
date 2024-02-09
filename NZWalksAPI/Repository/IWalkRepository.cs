using NZWalksAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Repository
{
    public interface IWalkRepository
    {
        Task<Walk>CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?>updateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
        
    }
}
