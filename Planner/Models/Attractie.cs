namespace Planner.Models;

public class Attractie
{
    public int Id { get; set; }
    public string Naam { get; set; } = default!;
    public List<Reservering> Reserveringen { get; set; } = default!;
}