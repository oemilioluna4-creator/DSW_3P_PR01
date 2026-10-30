using System.Text.Json;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public class MisionJsonRepository : IMisionRepository
{
    private readonly string _filePath = "misiones.json";
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<IEnumerable<Mision>> GetAllAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (!File.Exists(_filePath)) return new List<Mision>();
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Mision>>(json) ?? new List<Mision>();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Mision?> GetByIdAsync(int id)
    {
        var misiones = await GetAllAsync();
        return misiones.FirstOrDefault(m => m.Id == id);
    }

    public async Task AddAsync(Mision mision)
    {
        await _semaphore.WaitAsync();
        try
        {
            var misiones = (await GetAllAsyncInternal()).ToList();
            mision.Id = misiones.Any() ? misiones.Max(m => m.Id) + 1 : 1;
            misiones.Add(mision);
            await SaveAllInternalAsync(misiones);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task UpdateAsync(Mision mision)
    {
        await _semaphore.WaitAsync();
        try
        {
            var misiones = (await GetAllAsyncInternal()).ToList();
            var index = misiones.FindIndex(m => m.Id == mision.Id);
            if (index != -1)
            {
                misiones[index] = mision;
                await SaveAllInternalAsync(misiones);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _semaphore.WaitAsync();
        try
        {
            var misiones = (await GetAllAsyncInternal()).ToList();
            var mision = misiones.FirstOrDefault(m => m.Id == id);
            if (mision != null)
            {
                misiones.Remove(mision);
                await SaveAllInternalAsync(misiones);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // Métodos de apoyo internos para evitar bloqueos del SemaphoreSlim al llamarse recursivamente
    private async Task<IEnumerable<Mision>> GetAllAsyncInternal()
    {
        if (!File.Exists(_filePath)) return new List<Mision>();
        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Mision>>(json) ?? new List<Mision>();
    }

    private async Task SaveAllInternalAsync(List<Mision> misiones)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(misiones, options);
        await File.WriteAllTextAsync(_filePath, json);
    }
}