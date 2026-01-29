using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using SmartCard.Domain.Entities;

// ======================= PAIS =======================
namespace SmartCard.Application.Paises.Queries
{
    public record GetPaisesQuery : IRequest<List<PaisDto>>;
    public class GetPaisesQueryHandler : IRequestHandler<GetPaisesQuery, List<PaisDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPaisesQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<PaisDto>> Handle(GetPaisesQuery request, CancellationToken cancellationToken)
            => await _context.Pais.AsNoTracking().ProjectTo<PaisDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public record GetPaisByIdQuery(int Id) : IRequest<PaisDto?>;
    public class GetPaisByIdQueryHandler : IRequestHandler<GetPaisByIdQuery, PaisDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPaisByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<PaisDto?> Handle(GetPaisByIdQuery request, CancellationToken cancellationToken)
            => await _context.Pais.AsNoTracking().Where(e => e.IdPais == request.Id).ProjectTo<PaisDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}

namespace SmartCard.Application.Paises.Commands
{
    public record CreatePaisCommand(string Nombre, string CodigoIso2) : IRequest<int>;
    public class CreatePaisCommandHandler : IRequestHandler<CreatePaisCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreatePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreatePaisCommand request, CancellationToken ct) {
            var e = new Domain.Entities.Pais { Nombre = request.Nombre, CodigoIso2 = request.CodigoIso2, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.Pais.Add(e); await _context.SaveChangesAsync(ct); return e.IdPais;
        }
    }
    public record UpdatePaisCommand(int IdPais, string Nombre, string CodigoIso2) : IRequest<bool>;
    public class UpdatePaisCommandHandler : IRequestHandler<UpdatePaisCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdatePaisCommand request, CancellationToken ct) {
            var e = await _context.Pais.FindAsync(new object[] { request.IdPais }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.CodigoIso2 = request.CodigoIso2; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
    public record DeletePaisCommand(int Id) : IRequest<bool>;
    public class DeletePaisCommandHandler : IRequestHandler<DeletePaisCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeletePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeletePaisCommand request, CancellationToken ct) {
            var e = await _context.Pais.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.Pais.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}

// ======================= FORMATO TARJETA =======================
namespace SmartCard.Application.Formatos.Queries
{
    public record GetFormatosQuery : IRequest<List<FormatoTarjetaDto>>;
    public class GetFormatosQueryHandler : IRequestHandler<GetFormatosQuery, List<FormatoTarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetFormatosQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<FormatoTarjetaDto>> Handle(GetFormatosQuery request, CancellationToken cancellationToken)
            => await _context.FormatosTarjeta.AsNoTracking().ProjectTo<FormatoTarjetaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public record GetFormatoByIdQuery(int Id) : IRequest<FormatoTarjetaDto?>;
    public class GetFormatoByIdQueryHandler : IRequestHandler<GetFormatoByIdQuery, FormatoTarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetFormatoByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<FormatoTarjetaDto?> Handle(GetFormatoByIdQuery request, CancellationToken cancellationToken)
            => await _context.FormatosTarjeta.AsNoTracking().Where(e => e.IdFormato == request.Id).ProjectTo<FormatoTarjetaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}

namespace SmartCard.Application.Formatos.Commands
{
    public record CreateFormatoCommand(string Nombre) : IRequest<int>;
    public class CreateFormatoCommandHandler : IRequestHandler<CreateFormatoCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreateFormatoCommand request, CancellationToken ct) {
            var e = new FormatoTarjeta { Nombre = request.Nombre, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.FormatosTarjeta.Add(e); await _context.SaveChangesAsync(ct); return e.IdFormato;
        }
    }
    public record UpdateFormatoCommand(int IdFormato, string Nombre) : IRequest<bool>;
    public class UpdateFormatoCommandHandler : IRequestHandler<UpdateFormatoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateFormatoCommand request, CancellationToken ct) {
            var e = await _context.FormatosTarjeta.FindAsync(new object[] { request.IdFormato }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
    public record DeleteFormatoCommand(int Id) : IRequest<bool>;
    public class DeleteFormatoCommandHandler : IRequestHandler<DeleteFormatoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteFormatoCommand request, CancellationToken ct) {
            var e = await _context.FormatosTarjeta.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.FormatosTarjeta.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}

// ======================= TIPO TARJETA =======================
namespace SmartCard.Application.Tipos.Queries
{
    public record GetTiposQuery : IRequest<List<TipoTarjetaDto>>;
    public class GetTiposQueryHandler : IRequestHandler<GetTiposQuery, List<TipoTarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTiposQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<TipoTarjetaDto>> Handle(GetTiposQuery request, CancellationToken cancellationToken)
            => await _context.TiposTarjeta.AsNoTracking().ProjectTo<TipoTarjetaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public record GetTipoByIdQuery(int Id) : IRequest<TipoTarjetaDto?>;
    public class GetTipoByIdQueryHandler : IRequestHandler<GetTipoByIdQuery, TipoTarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTipoByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<TipoTarjetaDto?> Handle(GetTipoByIdQuery request, CancellationToken cancellationToken)
            => await _context.TiposTarjeta.AsNoTracking().Where(e => e.IdTipo == request.Id).ProjectTo<TipoTarjetaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}

namespace SmartCard.Application.Tipos.Commands
{
    public record CreateTipoCommand(string Nombre) : IRequest<int>;
    public class CreateTipoCommandHandler : IRequestHandler<CreateTipoCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreateTipoCommand request, CancellationToken ct) {
            var e = new TipoTarjeta { Nombre = request.Nombre, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.TiposTarjeta.Add(e); await _context.SaveChangesAsync(ct); return e.IdTipo;
        }
    }
    public record UpdateTipoCommand(int IdTipo, string Nombre) : IRequest<bool>;
    public class UpdateTipoCommandHandler : IRequestHandler<UpdateTipoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateTipoCommand request, CancellationToken ct) {
            var e = await _context.TiposTarjeta.FindAsync(new object[] { request.IdTipo }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
    public record DeleteTipoCommand(int Id) : IRequest<bool>;
    public class DeleteTipoCommandHandler : IRequestHandler<DeleteTipoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteTipoCommand request, CancellationToken ct) {
            var e = await _context.TiposTarjeta.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.TiposTarjeta.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}

// ======================= USUARIO SISTEMA =======================
namespace SmartCard.Application.UsuarioSistemas.Queries
{
    public record GetUsuarioSistemasQuery : IRequest<List<UsuarioSistemaDto>>;
    public class GetUsuarioSistemasQueryHandler : IRequestHandler<GetUsuarioSistemasQuery, List<UsuarioSistemaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUsuarioSistemasQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<UsuarioSistemaDto>> Handle(GetUsuarioSistemasQuery request, CancellationToken cancellationToken)
            => await _context.UsuarioSistema.AsNoTracking().ProjectTo<UsuarioSistemaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public record GetUsuarioSistemaByIdQuery(int Id) : IRequest<UsuarioSistemaDto?>;
    public class GetUsuarioSistemaByIdQueryHandler : IRequestHandler<GetUsuarioSistemaByIdQuery, UsuarioSistemaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUsuarioSistemaByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<UsuarioSistemaDto?> Handle(GetUsuarioSistemaByIdQuery request, CancellationToken cancellationToken)
            => await _context.UsuarioSistema.AsNoTracking().Where(e => e.IdUsuarioSistema == request.Id).ProjectTo<UsuarioSistemaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}

namespace SmartCard.Application.UsuarioSistemas.Commands
{
    public record CreateUsuarioSistemaCommand(string NombreUsuario) : IRequest<int>;
    public class CreateUsuarioSistemaCommandHandler : IRequestHandler<CreateUsuarioSistemaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateUsuarioSistemaCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreateUsuarioSistemaCommand request, CancellationToken ct) {
            var e = new UsuarioSistema { NombreUsuario = request.NombreUsuario, FechaCreacion = DateTime.UtcNow };
            _context.UsuarioSistema.Add(e); await _context.SaveChangesAsync(ct); return e.IdUsuarioSistema;
        }
    }
    public record UpdateUsuarioSistemaCommand(int IdUsuarioSistema, string NombreUsuario) : IRequest<bool>;
    public class UpdateUsuarioSistemaCommandHandler : IRequestHandler<UpdateUsuarioSistemaCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUsuarioSistemaCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateUsuarioSistemaCommand request, CancellationToken ct) {
            var e = await _context.UsuarioSistema.FindAsync(new object[] { request.IdUsuarioSistema }, ct);
            if (e == null) return false;
            e.NombreUsuario = request.NombreUsuario;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
    public record DeleteUsuarioSistemaCommand(int Id) : IRequest<bool>;
    public class DeleteUsuarioSistemaCommandHandler : IRequestHandler<DeleteUsuarioSistemaCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUsuarioSistemaCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteUsuarioSistemaCommand request, CancellationToken ct) {
            var e = await _context.UsuarioSistema.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.UsuarioSistema.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}
