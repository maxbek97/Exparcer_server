using server.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace server.Repositories
{
    public interface IStructUnitRepository
    {
        Task<StructUnit> GetOrCreateAsync(string structName);
    }

    public class StructUnitRepository : IStructUnitRepository
    {
        private readonly ExcelParcerContext _context;

        public StructUnitRepository(ExcelParcerContext context)
        {
            _context = context;
        }

        public async Task<StructUnit> GetOrCreateAsync(string structName)
        {
            var unit = await _context.StructUnits
                .FirstOrDefaultAsync(s => s.NameStruct == structName);

            if (unit == null)
            {
                unit = new StructUnit { NameStruct = structName };
                _context.StructUnits.Add(unit);
                await _context.SaveChangesAsync();
            }

            return unit;
        }
    }
}
