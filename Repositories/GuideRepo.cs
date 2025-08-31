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
