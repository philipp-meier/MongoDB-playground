using Server.Models;
using Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly SensorDataService _sensorDataService;

    public SensorDataController(SensorDataService sensorDataService)
        => _sensorDataService = sensorDataService;

    [HttpGet("{sensorId}")]
    public async Task<ActionResult<SensorStatistic>> GetStatistic(string sensorId, DateTime from, DateTime to, CancellationToken cancellationToken)
        => await _sensorDataService.GetStatistic(sensorId, from, to, cancellationToken);

    [HttpPost("import"), DisableRequestSizeLimit]
    public async Task<IActionResult> Import(SensorData[] sensorData, CancellationToken cancellationToken)
    {
        await _sensorDataService.ImportAsync(sensorData, cancellationToken);
        return NoContent();
    }
}