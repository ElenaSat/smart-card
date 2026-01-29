using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Tarjeta
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

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }

    public virtual Cuenta? Cuenta { get; set; }
    public virtual FormatoTarjeta? FormatoTarjeta { get; set; }
    public virtual TipoTarjeta? TipoTarjeta { get; set; }
    public virtual Pais? Pais { get; set; }
}
