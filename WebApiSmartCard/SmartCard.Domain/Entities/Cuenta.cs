using System.ComponentModel.DataAnnotations;

namespace SmartCard.Domain.Entities;

public class Cuenta
{
    public int IdCuenta { get; set; }
    public string? Numero { get; set; }
    public int? IdUsuario { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }

    public virtual Usuario? Usuario { get; set; }
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
