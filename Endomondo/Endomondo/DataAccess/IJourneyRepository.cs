using System.Collections.Generic;
using System.Threading.Tasks;
using Endomondo.Models;

namespace Endomondo.DataAccess
{
    public interface IJourneyRepository
    {
        Task<Journey> GetAsync(int id);

        Task<IEnumerable<Journey>> GetAllAsync();

        Task<IEnumerable<Journey>> GetAllWithLocationsAsync();

        Task AddAsync(Journey journey);

        Task UpdateAsync(Journey journey);

        Task RemoveAsync(Journey journey);
    }
}
