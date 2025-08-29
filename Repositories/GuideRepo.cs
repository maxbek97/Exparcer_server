using server.Models;
using Microsoft.EntityFrameworkCore;

namespace server.Repositories
{
    public interface IGuideRepository
    {
        Task<bool> ExistsAsync(string idDocument, byte? idStructUnit);
        Task AddAsync(Guide guide);
    }

    public class GuideRepository : IGuideRepository
    {
        private readonly ExcelParcerContext _context;

        public GuideRepository(ExcelParcerContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string idDocument, byte? idStructUnit)
        {
            return await _context.Guides
                .AnyAsync(g => g.IdDocument == idDocument && g.IdStructUnit == idStructUnit);
        }

        public async Task AddAsync(Guide guide)
        {
            _context.Guides.Add(guide);
            await _context.SaveChangesAsync();
        }
    }

}
