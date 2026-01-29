using AutoMapper;
using SmartCard.Application.DTOs;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<SmartCard.Application.Usuarios.Commands.CreateUsuario.CreateUsuarioCommand, Usuario>();
        
        CreateMap<Cuenta, CuentaDto>();
        CreateMap<SmartCard.Application.Cuentas.Commands.CreateCuentaCommand, Cuenta>();

        CreateMap<Tarjeta, TarjetaDto>();
        CreateMap<SmartCard.Application.Tarjetas.Commands.CreateTarjetaCommand, Tarjeta>();

        CreateMap<Pais, PaisDto>();
        CreateMap<FormatoTarjeta, FormatoTarjetaDto>();
        CreateMap<TipoTarjeta, TipoTarjetaDto>();
        CreateMap<UsuarioSistema, UsuarioSistemaDto>();
        // Add other mappings
    }
}
