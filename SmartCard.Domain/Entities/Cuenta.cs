using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCard.Domain.Entities;

public class Cuenta
{
    [Key]
    public int IdCuenta { get; set; }
    [Required, MaxLength(50)]
    public string? Numero { get; set; }
    [ForeignKey("Usuario")]
    public int? IdUsuario { get; set; }

    // Audit
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public int? UsuarioCreacion { get; set; }
    public int? UsuarioModificacion { get; set; }

    public virtual Usuario? Usuario { get; set; }
    public virtual ICollection<Tarjeta>? Tarjetas { get; set; }
}
