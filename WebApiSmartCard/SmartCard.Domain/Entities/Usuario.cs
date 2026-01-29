using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string? Titulo { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? InfoExtra { get; set; }
    
    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }

    public virtual ICollection<Cuenta>? Cuentas { get; set; }
}
