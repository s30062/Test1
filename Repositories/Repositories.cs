namespace WebApplication2;

using Microsoft.EntityFrameworkCore;
using TavernSystem.Models;

namespace TavernSystem.Repositories;

public class AdventurerRepository : IAdventurerRepository
{
    private readonly TavernDbContext _context;

    public AdventurerRepository(TavernDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Adventurer>> GetAllAsync() =>
        await _context.Adventurers.Select(a => new Adventurer
        {
            Id = a.Id,
            Nickname = a.Nickname
        }).ToListAsync();

    public async Task<Adventurer?> GetByIdAsync(int id) =>
        await _context.Adventurers.Include(a => a.Bounties).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Adventurer?> GetByPersonDataIdAsync(string personDataId) =>
        await _context.Adventurers.FirstOrDefaultAsync(a => a.PersonDataId == personDataId);

    public async Task AddAsync(Adventurer adventurer)
    {
        _context.Adventurers.Add(adventurer);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasBountiesAsync(string personDataId) =>
        await _context.Bounties.AnyAsync(b => b.Adventurer.PersonDataId == personDataId);
}
