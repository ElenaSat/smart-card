using SmartCard.Domain.Entities;

namespace SmartCard.Application.DTOs;

public class TarjetaDto
{
    public int IdTarjeta { get; set; }
    public int? IdCuenta { get; set; }
    public int? IdFormato { get; set; }
    public int? IdTipo { get; set; }
    public string? Pan { get; set; }
    public string? Pin { get; set; }
    public DateTime? FechaEmision { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    public int? IdPaisEmision { get; set; }
    public bool? DdaHabilitado { get; set; }
    public bool? ArqcHabilitado { get; set; }
    public string? Track1 { get; set; }
    public string? Track2 { get; set; }
    public string? Track3 { get; set; }
}
