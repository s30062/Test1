namespace TavernSystem.Models;

public class Adventurer
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string PersonDataId { get; set; }

    public ICollection<Bounty> Bounties { get; set; } = new List<Bounty>();
}