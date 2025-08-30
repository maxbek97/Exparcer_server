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
    //[HttpPost("upload")]
    //[Consumes("multipart/form-data")]
    //public async Task<IActionResult> UploadAndCheck([FromForm] FileUploadModel model)
    //{
    //    if (model.File == null || model.File.Length == 0)
    //        return BadRequest("Файл не загружен");

    //    using var ms = new MemoryStream();
    //    await model.File.CopyToAsync(ms);
    //    ms.Position = 0;

    //    var service = new ExcelService(ms);
    //    var mes = service.Process();

    //    return Ok(new { message = mes });
    //}
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

    [HttpGet("get_units")]
    public async Task<IActionResult> GetStructUnits()
    {
        var units = await _guideService.GetUnitsList();
        return Ok(units);
    }

    [HttpGet("get_smart_units")]
    public async Task<IActionResult> GetSortedFilteredGudes([FromQuery] List<int> structUnitId, [FromQuery] string sortType = "asc")
    {
        var result = await _guideService.GetFilteredGuides(structUnitId, sortType);
        return Ok(result);
    }
}

