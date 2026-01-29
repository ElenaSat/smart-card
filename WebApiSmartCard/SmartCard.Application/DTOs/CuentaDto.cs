using SmartCard.Domain.Entities;

namespace SmartCard.Application.DTOs;

public class CuentaDto
{
    public int IdCuenta { get; set; }
    public string? Numero { get; set; }
    public int? IdUsuario { get; set; }
    
    // Potentially include nested UsuarioDto if needed, for now keep it flat or simple
}
