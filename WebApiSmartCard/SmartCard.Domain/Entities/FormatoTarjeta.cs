using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class FormatoTarjeta
{
    [Key]
    public int IdFormato { get; set; }
    [Required, MaxLength(50)]
    public string? Nombre { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }
    
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
