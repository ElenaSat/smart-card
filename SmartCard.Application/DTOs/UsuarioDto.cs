using SmartCard.Domain.Entities;

namespace SmartCard.Application.DTOs;

public class UsuarioDto
{
    public int IdUsuario { get; set; }
    public string? Titulo { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? InfoExtra { get; set; }   

}
