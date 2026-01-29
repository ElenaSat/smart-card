using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class UsuarioSistema
{
    public int IdUsuarioSistema { get; set; }
    public string? NombreUsuario { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
}
