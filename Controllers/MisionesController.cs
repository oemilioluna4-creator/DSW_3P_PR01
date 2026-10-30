using DSW_3P_PR01.DTOs;
using DSW_3P_PR01.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSW_3P_PR01.Controllers;

[Route("api/misiones")]
[ApiController]
public class MisionesController : ControllerBase
{
    private readonly IMisionService _service;

    public MisionesController(IMisionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var misiones = await _service.GetAllMisionesAsync();
        return Ok(misiones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mision = await _service.GetMisionByIdAsync(id);
        if (mision == null) return NotFound(new { mensaje = "La misión solicitada no existe." });
        return Ok(mision);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MisionCreateDto dto)
    {
        var nuevaMision = await _service.CreateMisionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = nuevaMision.Id }, nuevaMision);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MisionUpdateDto dto)
    {
        var actualizado = await _service.UpdateMisionAsync(id, dto);
        if (!actualizado) return NotFound(new { mensaje = "No se puede modificar una misión inexistente." });
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.DeleteMisionAsync(id);
        if (!eliminado) return NotFound(new { mensaje = "No se puede eliminar una misión inexistente." });

        return NoContent();
    }
}