using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class UsuarioSistema
{
    [Key]
    public int IdUsuarioSistema { get; set; }
    [Required, MaxLength(100)]
    public string? NombreUsuario { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
}
