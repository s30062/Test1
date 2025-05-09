namespace WebApplication3;

using TavernSystem.Models;

namespace TavernSystem.Application;

public interface IAdventurerService
{
    Task<IEnumerable<Adventurer>> GetAllAsync();
    Task<Adventurer?> GetByIdAsync(int id);
    Task CreateAsync(Adventurer adventurer);
}
using System.Text.RegularExpressions;
using TavernSystem.Models;
using TavernSystem.Repositories;

namespace TavernSystem.Application;

public class AdventurerService : IAdventurerService
{
    private readonly IAdventurerRepository _repo;

    public AdventurerService(IAdventurerRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Adventurer>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Adventurer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Adventurer adventurer)
    {
        if (!ValidatePersonDataId(adventurer.PersonDataId))
            throw new ArgumentException("Invalid PersonDataId format.");

        var existing = await _repo.GetByPersonDataIdAsync(adventurer.PersonDataId);
        if (existing != null)
            throw new InvalidOperationException("Person already registered.");

        if (await _repo.HasBountiesAsync(adventurer.PersonDataId))
            throw new UnauthorizedAccessException("Cannot register adventurer with bounties.");

        await _repo.AddAsync(adventurer);
    }

    private bool ValidatePersonDataId(string id)
    {
        return Regex.IsMatch(id, @"^[A-Z]{2}(000[1-9]|[0-9]{4})(0[1-9]|1[0-1])(0[1-9]|1[0-9]|2[0-8])\d{4}[A-Z]{2}$");
    }
}
