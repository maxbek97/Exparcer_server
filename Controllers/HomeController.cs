using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Models;
using server.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IGuideService _guideService;

    public HomeController(IGuideService guideService)
    {
        _guideService = guideService;
    }
    /// <summary>
    /// Позволяет создать запись об инструкции и отправить её в БД
    /// </summary>
    /// <param name="dto">Объект, описывающий пользовательские данные для ввода</param>
    /// <returns>Сообщение об удачном сохранении записи, или об ошибке дубликации записи(Надо проверить на другие ошибки)</returns>
    [HttpPost("create_record")]
    public async Task<IActionResult> Create([FromBody] CreateRecordDTO dto)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _guideService.CreateGuideAsync(dto);
            return Ok(new { message = "Запись сохранена" });
        }
        catch (DuplicateGuideException e)
        {
            return Ok(new { message = e.Message });
        }
    }

    /// <summary>
    /// Позволяет получить все структурные единицы в БД
    /// </summary>
    /// <returns>Список объектов структурных единиц</returns>
    [HttpGet("get_units")]
    public async Task<IActionResult> GetStructUnits()
    {
        var units = await _guideService.GetUnitsList();
        return Ok(units);
    }
    /// <summary>
    /// Реализует функцию поиска записи об инструкции в БД
    /// </summary>
    /// <param name="guide_name">Полное или частичное название Инструкции или её обозначения(номера)</param>
    /// <param name="structUnitId">Список идентификаторов структурных единиц, по которым будет производиться поиск Инструкций</param>
    /// <param name="sortType">Тип сортировки</param>
    /// <returns>Список Инструкций, удовлетворяющих условиям</returns>
    [HttpGet("smart_guidefinder")]
    public async Task<IActionResult> GetSortedFilteredGudes([FromQuery] string guide_name = null, [FromQuery] List<int> structUnitId = null, [FromQuery] string sortType = "asc")
    {
        var result = await _guideService.GetFilteredGuides(guide_name, structUnitId, sortType);
        return Ok(result);
    }
}

