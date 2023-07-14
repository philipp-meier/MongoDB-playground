using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly SensorDataService _sensorDataService;

    public SensorDataController(SensorDataService sensorDataService) =>
        _sensorDataService = sensorDataService;

    [HttpGet]
    public async Task<List<SensorData>> Get() =>
        await _sensorDataService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<SensorData>> Get(string id)
    {
        var sensorData = await _sensorDataService.GetAsync(id);

        if (sensorData is null)
        {
            return NotFound();
        }

        return sensorData;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SensorData sensorData)
    {
        await _sensorDataService.CreateAsync(sensorData);

        return CreatedAtAction(nameof(Get), new { id = sensorData.Id }, sensorData);
    }

    [HttpPost("import"), DisableRequestSizeLimit]
    public async Task<IActionResult> Import(SensorData[] sensorData, CancellationToken cancellationToken)
    {
        await _sensorDataService.BulkInsertAsync(sensorData, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, SensorData updatedSensorData)
    {
        var sensorData = await _sensorDataService.GetAsync(id);

        if (sensorData is null)
        {
            return NotFound();
        }

        updatedSensorData.Id = sensorData.Id;

        await _sensorDataService.UpdateAsync(id, updatedSensorData);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var sensorData = await _sensorDataService.GetAsync(id);

        if (sensorData is null)
        {
            return NotFound();
        }

        await _sensorDataService.RemoveAsync(id);

        return NoContent();
    }
}