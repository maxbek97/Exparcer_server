using server.Models;
using Microsoft.EntityFrameworkCore;

namespace server.Repositories
{
    public interface IGuideRepository
    {
        Task<bool> ExistsAsync(string idDocument, byte? idStructUnit);
        Task AddAsync(Guide guide);
        Task<List<GetRecordDTO>> GetGuides(string guide_name, List<int> structUnitIds);
    }

    public class GuideRepository : IGuideRepository
    {
        private readonly ExcelParcerContext _context;

        public GuideRepository(ExcelParcerContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Делает запрос в БД существует ли Инструкция с таким Номером, в таком структурном подразделении.
        /// </summary>
        /// <param name="idDocument">Обозначение(номер) инструкции</param>
        /// <param name="idStructUnit">Идентификатор структурного подразделения</param>
        /// <returns>Если есть совпадение возвращает True</returns>
        public async Task<bool> ExistsAsync(string idDocument, byte? idStructUnit)
        {
            return await _context.Guides
                .AnyAsync(g => g.IdDocument == idDocument && g.IdStructUnit == idStructUnit);
        }

        /// <summary>
        /// Добавляет переданную инструкцию в БД
        /// </summary>
        /// <param name="guide">Данные об инструкции</param>
        /// <returns>Я так понял, кол-во записей добавленных в БД</returns>
        public async Task AddAsync(Guide guide)
        {
            _context.Guides.Add(guide);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Возвращает список инструкций, с частично или полностью совпадающем названием ии обозначением(номером) инструкции в указанных структурных подразделениях
        /// </summary>
        /// <param name="guide_name">Название или обозначение(номер) инструкции</param>
        /// <param name="structUnitIds">Список идентификаторов структурных единиц</param>
        /// <returns>Список инструкций</returns>
        public async Task<List<GetRecordDTO>> GetGuides(string guide_name, List<int> structUnitIds)
        {
            IQueryable<Guide> query = _context.Guides;

            // фильтр по структурным подразделениям
            if (structUnitIds != null && structUnitIds.Any())
            {
                query = query.Where(i => structUnitIds.Contains(i.IdStructUnit));
            }

            // поиск по имени инструкции или по IdDocument
            if (!string.IsNullOrEmpty(guide_name))
            {
                query = query.Where(j =>
                    EF.Functions.Like(j.NameGuid, $"%{guide_name}%") ||
                    EF.Functions.Like(j.IdDocument.ToString(), $"%{guide_name}%")
                );
            }

            var guides_list = await query
                .Select(j => new GetRecordDTO
                {
                    id_guid = j.IdDocument,
                    name_guid = j.NameGuid,
                    check_day = j.CheckDate,
                    struct_name = j.IdStructUnitNavigation.NameStruct,
                    acceptance_day = j.AcceptanceDate,
                    days_remaining = EF.Functions.DateDiffDay(DateTime.Now,
                        j.CheckDate.ToDateTime(TimeOnly.MinValue)
                    )
                })
                .ToListAsync();

            return guides_list;
        }

    }

}
