using System.Collections.Generic;
using System.Threading.Tasks;
using Endomondo.Models;

namespace Endomondo.DataAccess
{
    public interface IJourneyRepository
    {
        Task<Journey> GetAsync(int id);

        Task<IEnumerable<Journey>> GetAllAsync();

        Task AddAsync(Journey journey);

        Task UpdateAsync(Journey journey);
    }
}
