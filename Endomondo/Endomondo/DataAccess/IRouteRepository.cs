using System.Collections.Generic;
using System.Threading.Tasks;
using Endomondo.Models;

namespace Endomondo.DataAccess
{
    public interface IRouteRepository
    {
        Task<Route> GetAsync(int id);

        Task<IEnumerable<Route>> GetAllAsync();

        Task AddAsync(Route route);
    }
}
