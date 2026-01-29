using AutoMapper;
using SmartCard.Application.DTOs;
using SmartCard.Application.Features.Cuentas.Commands;
using SmartCard.Application.Features.Tarjetas.Commands;
using SmartCard.Application.Features.Usuarios.Commands;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<CreateUsuarioCommand, Usuario>();
        
        CreateMap<Cuenta, CuentaDto>();
        CreateMap<CreateCuentaCommand, Cuenta>();

        CreateMap<Tarjeta, TarjetaDto>();
        CreateMap<CreateTarjetaCommand, Tarjeta>();

        CreateMap<Pais, PaisDto>();
        CreateMap<FormatoTarjeta, FormatoTarjetaDto>();
        CreateMap<TipoTarjeta, TipoTarjetaDto>();
        CreateMap<UsuarioSistema, UsuarioSistemaDto>();
        // Add other mappings
    }
}
