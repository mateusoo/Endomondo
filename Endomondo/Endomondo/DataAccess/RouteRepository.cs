using System.Collections.Generic;
using System.Threading.Tasks;
using Endomondo.Models;
using Microsoft.EntityFrameworkCore;

namespace Endomondo.DataAccess
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DataContext _context;

        public RouteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Route> GetAsync(int id)
        {
            return await _context.Routes.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task AddAsync(Route route)
        {
            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
        }
    }
}
