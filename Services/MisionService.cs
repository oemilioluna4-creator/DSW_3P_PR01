using DSW_3P_PR01.DTOs;
using DSW_3P_PR01.Models;
using DSW_3P_PR01.Repositories;

namespace DSW_3P_PR01.Services;

public class MisionService : IMisionService
{
    private readonly IMisionRepository _repository;

    public MisionService(IMisionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Mision>> GetAllMisionesAsync() => await _repository.GetAllAsync();

    public async Task<Mision?> GetMisionByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<Mision> CreateMisionAsync(MisionCreateDto dto)
    {
        var nuevaMision = new Mision
        {
            Nombre = dto.Nombre,
            Dificultad = dto.Dificultad,
            RecompensaXP = dto.RecompensaXP,
            Completada = false // Regla: inicia como no completada
        };

        await _repository.AddAsync(nuevaMision);
        return nuevaMision;
    }

    public async Task<bool> UpdateMisionAsync(int id, MisionUpdateDto dto)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente == null) return false; // Regla: no permitir modificaciones de inexistentes

        existente.Nombre = dto.Nombre;
        existente.Dificultad = dto.Dificultad;
        existente.RecompensaXP = dto.RecompensaXP;
        existente.Completada = dto.Completada;

        await _repository.UpdateAsync(existente);
        return true;
    }

    public async Task<bool> DeleteMisionAsync(int id)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente == null) return false; // Regla: no permitir eliminaciones de inexistentes

        await _repository.DeleteAsync(id);
        return true;
    }
}