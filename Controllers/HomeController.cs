using Microsoft.AspNetCore.Mvc;
using server.Services;
namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAndCheck([FromForm] FileUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
            return BadRequest("Файл не загружен");

        using var ms = new MemoryStream();
        await model.File.CopyToAsync(ms);
        ms.Position = 0;

        var service = new ExcelService(ms);
        service.Process(); // например, пропускаем 2 строки шапки

        return Ok(new { message = "Файл проверен. Смотри вывод в консоли." });
    }

    public class FileUploadModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
