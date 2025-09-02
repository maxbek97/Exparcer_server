using server.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace server.Repositories
{
    public interface IStructUnitRepository
    {
        Task<StructUnit> GetOrCreateAsync(string structName);
        Task<List<StructureUnitsDTO>> GetUnitsAsync();
    }

    public class StructUnitRepository : IStructUnitRepository
    {
        private readonly ExcelParcerContext _context;

        public StructUnitRepository(ExcelParcerContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Делает запрос к БД. Если переданного структурного подразделения нет в БД, то он добавляет информаию о нем
        /// </summary>
        /// <param name="structName">Название структурного подразделения</param>
        /// <returns>Объект структурного подразделения, найденного по названия, или новосформированного</returns>
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
        /// <summary>
        /// Ищет все в БД Структурные подразделения
        /// </summary>
        /// <returns>Список структурных подразделений</returns>
        public async Task<List<StructureUnitsDTO>> GetUnitsAsync()
        {
            //Наверное всё-таки можно воспользоваться классом StructUnit, а не прибегать к новому
            var units = await _context.StructUnits
            .Select(u => new StructureUnitsDTO { NameStruct = u.NameStruct, IdUnit = u.IdUnit  })
            .ToListAsync();

            return units;
        }
    }
}
