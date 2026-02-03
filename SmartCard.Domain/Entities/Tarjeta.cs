using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCard.Domain.Entities;

public class Tarjeta
{
    [Key]
    public int IdTarjeta { get; set; }
    [ForeignKey("Cuenta")]
    public int? IdCuenta { get; set; }
    [ForeignKey("FormatoTarjeta")]
    public int? IdFormato { get; set; }

    [ForeignKey("TipoTarjeta")]
    public int? IdTipo { get; set; }
    [Required, MaxLength(32)]
    public string? Pan { get; set; }
    [MaxLength(20)]
    public string? Pin { get; set; }
    public DateTime? FechaEmision { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    [ForeignKey("Pais")]
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
