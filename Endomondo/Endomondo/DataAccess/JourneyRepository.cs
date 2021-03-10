using System.Collections.Generic;
using System.Threading.Tasks;
using Endomondo.Models;
using Microsoft.EntityFrameworkCore;

namespace Endomondo.DataAccess
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly DataContext _context;

        public JourneyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Journey> GetAsync(int id)
        {
            return await _context.Journeys
                .Include(j => j.Locations)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Journey>> GetAllAsync()
        {
            return await _context.Journeys.ToListAsync();
        }

        public async Task AddAsync(Journey journey)
        {
            await _context.AddAsync(journey);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Journey journey)
        {
            _context.Update(journey);
            await _context.SaveChangesAsync();
        }
    }
}
