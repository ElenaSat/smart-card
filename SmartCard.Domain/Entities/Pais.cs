using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Pais
{
    [Key]
    public int IdPais { get; set; }
    [Required, MaxLength(100)]
    public string? Nombre { get; set; }
    [MaxLength(2)]
    public string? CodigoIso2 { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }
    
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
