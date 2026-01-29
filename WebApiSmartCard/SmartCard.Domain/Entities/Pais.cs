using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Pais
{
    public int IdPais { get; set; }
    public string? Nombre { get; set; }
    public string? CodigoIso2 { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }
    
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
