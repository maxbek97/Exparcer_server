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
    /// ��������� ������� ������ �� ���������� � ��������� � � ��
    /// </summary>
    /// <param name="dto">������, ����������� ���������������� ������ ��� �����</param>
    /// <returns>��������� �� ������� ���������� ������, ��� �� ������ ���������� ������(���� ��������� �� ������ ������)</returns>
    [HttpPost("create_record")]
    public async Task<IActionResult> Create([FromBody] CreateRecordDTO dto)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _guideService.CreateGuideAsync(dto);
            return Ok(new { message = "������ ���������" });
        }
        catch (DuplicateGuideException e)
        {
            return Ok(new { message = e.Message });
        }
    }

    /// <summary>
    /// ��������� �������� ��� ����������� ������� � ��
    /// </summary>
    /// <returns>������ �������� ����������� ������</returns>
    [HttpGet("get_units")]
    public async Task<IActionResult> GetStructUnits()
    {
        var units = await _guideService.GetUnitsList();
        return Ok(units);
    }
    /// <summary>
    /// ��������� ������� ������ ������ �� ���������� � ��
    /// </summary>
    /// <param name="guide_name">������ ��� ��������� �������� ���������� ��� � �����������(������)</param>
    /// <param name="structUnitId">������ ��������������� ����������� ������, �� ������� ����� ������������� ����� ����������</param>
    /// <param name="sortType">��� ����������</param>
    /// <returns>������ ����������, ��������������� ��������</returns>
    [HttpGet("smart_guidefinder")]
    public async Task<IActionResult> GetSortedFilteredGudes([FromQuery] string guide_name = null, [FromQuery] List<int> structUnitId = null, [FromQuery] string sortType = "asc")
    {
        var result = await _guideService.GetFilteredGuides(guide_name, structUnitId, sortType);
        return Ok(result);
    }
}

