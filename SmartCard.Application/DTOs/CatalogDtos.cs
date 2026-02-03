namespace SmartCard.Application.DTOs;

public class PaisDto
{
    public int IdPais { get; set; }
    public string? Nombre { get; set; }
    public string? CodigoIso2 { get; set; }
}

public class FormatoTarjetaDto
{
    public int IdFormato { get; set; }
    public string? Nombre { get; set; }
}

public class TipoTarjetaDto
{
    public int IdTipo { get; set; }
    public string? Nombre { get; set; }
}

public class UsuarioSistemaDto
{
    public int IdUsuarioSistema { get; set; }
    public string? NombreUsuario { get; set; }
}
