namespace WebApplication2;


using TavernSystem.Models;

namespace TavernSystem.Repositories;

public interface IAdventurerRepository
{
    Task<IEnumerable<Adventurer>> GetAllAsync();
    Task<Adventurer?> GetByIdAsync(int id);
    Task<Adventurer?> GetByPersonDataIdAsync(string personDataId);
    Task AddAsync(Adventurer adventurer);
    Task<bool> HasBountiesAsync(string personDataId);
}

}