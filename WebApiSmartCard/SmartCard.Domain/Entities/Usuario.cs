using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    [MaxLength(20)]
    public string? Titulo { get; set; }
    [Required, MaxLength(40)]
    public string? Nombre { get; set; }
    [Required, MaxLength(40)]
    public string? Apellido { get; set; }
    [Required, MaxLength(500)]
    public string? InfoExtra { get; set; }
    
    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }

    public virtual ICollection<Cuenta>? Cuentas { get; set; }
}
