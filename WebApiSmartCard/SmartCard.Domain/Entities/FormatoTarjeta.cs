using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class FormatoTarjeta
{
    public int IdFormato { get; set; }
    public string? Nombre { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }
    
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
