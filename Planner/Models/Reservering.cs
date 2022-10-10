using System.ComponentModel.DataAnnotations.Schema;

namespace Planner.Models;

public class Reservering
{
    public int Id { get; set; }
    public Gast Gast { get; set; } = default!;
    public Attractie Attractie { get; set; } = default!;
    public int Dag { get; set; }
    private DateOnly Datum => new DateOnly(1970, 1, 1).AddDays(Dag); 
    public int DagDeel { get; set; }
    private TimeOnly Tijd => new TimeOnly(8, 45).AddMinutes(15 * DagDeel); 
    [NotMapped] public DateTime DatumTijd => Datum.ToDateTime(Tijd); 
}