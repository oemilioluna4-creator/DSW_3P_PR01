using DSW_3P_PR01.DTOs;
using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Services;

public interface IMisionService
{
    Task<IEnumerable<Mision>> GetAllMisionesAsync();
    Task<Mision?> GetMisionByIdAsync(int id);
    Task<Mision> CreateMisionAsync(MisionCreateDto dto);
    Task<bool> UpdateMisionAsync(int id, MisionUpdateDto dto);
    Task<bool> DeleteMisionAsync(int id);
}