using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repositories;
using server.Exceptions;
using System;

namespace server.Services
{
    public interface IGuideService
    {
        Task CreateGuideAsync(CreateRecordDTO dto);
        Task<List<StructureUnitsDTO>> GetUnitsList();
        Task<List<GetRecordDTO>> GetFilteredGuides(List<int> structUnitIds, string sortOrder = "asc");
    }

    public class GuideService : IGuideService
    {
        private readonly IStructUnitRepository _unitRepository;
        private readonly IGuideRepository _guideRepository;
        private readonly ExcelParcerContext _context;

        public GuideService(IStructUnitRepository unitRepository, ExcelParcerContext context, IGuideRepository guideRepository)
        {
            _unitRepository = unitRepository;
            _context = context;
            _guideRepository = guideRepository;
        }

        public async Task CreateGuideAsync(CreateRecordDTO dto)
        {
            var structUnit = await _unitRepository.GetOrCreateAsync(dto.struct_name);

            bool exists = await _guideRepository.ExistsAsync(dto.id_guid, structUnit.IdUnit);
            if (exists)
            {
                throw new DuplicateGuideException(
                    $"Документ '{dto.name_guid}' с id '{dto.id_guid}' " +
                    $"уже существует в подразделении '{structUnit.NameStruct}'.");
            }

            var guide = new Guide
            {
                NameGuid = dto.name_guid,
                AcceptanceDate = dto.acceptance_day,
                CheckDate = dto.check_day,
                IdDocument = dto.id_guid,
                IdStructUnit = structUnit.IdUnit
            };

            _context.Guides.Add(guide);

            await _context.SaveChangesAsync();
        }
        public async Task<List<StructureUnitsDTO>> GetUnitsList()
        {
            var structures = await _unitRepository.GetUnitsAsync();
            return structures;
        }
        public async Task<List<GetRecordDTO>> GetFilteredGuides(List<int> structUnitIds, string sortOrder)
        {
            var filteredList = await _guideRepository.GetGuides(structUnitIds);

            var sortedGuides = sortOrder == "asc" ? filteredList.OrderByDescending(i => i.days_remaining) : filteredList.OrderBy(i => i.days_remaining);

            return sortedGuides.ToList();
        }

    }
}
