using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Repositories;

public interface IMisionRepository
{
    Task<IEnumerable<Mision>> GetAllAsync();
    Task<Mision?> GetByIdAsync(int id);
    Task AddAsync(Mision mision);
    Task UpdateAsync(Mision mision);
    Task DeleteAsync(int id);
}