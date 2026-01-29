using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Tarjetas.Queries
{
    // Query: GetAll
    public record GetTarjetasQuery : IRequest<List<TarjetaDto>>;

    public class GetTarjetasQueryHandler : IRequestHandler<GetTarjetasQuery, List<TarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTarjetasQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TarjetaDto>> Handle(GetTarjetasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tarjetas
                .AsNoTracking()
                .ProjectTo<TarjetaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    // Query: GetById
    public record GetTarjetaByIdQuery(int Id) : IRequest<TarjetaDto?>;

    public class GetTarjetaByIdQueryHandler : IRequestHandler<GetTarjetaByIdQuery, TarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTarjetaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TarjetaDto?> Handle(GetTarjetaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tarjetas
                .AsNoTracking()
                .Where(x => x.IdTarjeta == request.Id)
                .ProjectTo<TarjetaDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

namespace SmartCard.Application.Tarjetas.Commands
{
    // Command: Create
    public record CreateTarjetaCommand : IRequest<int>
    {
        public int? IdCuenta { get; set; }
        public int? IdFormato { get; set; }
        public int? IdTipo { get; set; }
        public string? Pan { get; set; }
        public string? Pin { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int? IdPaisEmision { get; set; }
        public bool? DdaHabilitado { get; set; }
        public bool? ArqcHabilitado { get; set; }
        public string? Track1 { get; set; }
        public string? Track2 { get; set; }
        public string? Track3 { get; set; }
    }

    public class CreateTarjetaCommandHandler : IRequestHandler<CreateTarjetaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTarjetaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Tarjeta>(request);
            
            // Audit
            entity.FechaCreacion = DateTime.UtcNow;
            entity.UsuarioCreacion = 1;

            _context.Tarjetas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdTarjeta;
        }
    }

    // Command: Update
    public record UpdateTarjetaCommand : IRequest<bool>
    {
        public int IdTarjeta { get; set; }
        public int? IdCuenta { get; set; }
        public int? IdFormato { get; set; }
        public int? IdTipo { get; set; }
        public string? Pan { get; set; }
        public string? Pin { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int? IdPaisEmision { get; set; }
        public bool? DdaHabilitado { get; set; }
        public bool? ArqcHabilitado { get; set; }
        public string? Track1 { get; set; }
        public string? Track2 { get; set; }
        public string? Track3 { get; set; }
    }

    public class UpdateTarjetaCommandHandler : IRequestHandler<UpdateTarjetaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTarjetaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tarjetas
                .FindAsync(new object[] { request.IdTarjeta }, cancellationToken);

            if (entity == null) return false;

            entity.IdCuenta = request.IdCuenta;
            entity.IdFormato = request.IdFormato;
            entity.IdTipo = request.IdTipo;
            entity.Pan = request.Pan;
            entity.Pin = request.Pin;
            entity.FechaEmision = request.FechaEmision;
            entity.FechaExpiracion = request.FechaExpiracion;
            entity.IdPaisEmision = request.IdPaisEmision;
            entity.DdaHabilitado = request.DdaHabilitado;
            entity.ArqcHabilitado = request.ArqcHabilitado;
            entity.Track1 = request.Track1;
            entity.Track2 = request.Track2;
            entity.Track3 = request.Track3;
            
            // Audit
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = 1;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    // Command: Delete
    public record DeleteTarjetaCommand(int Id) : IRequest<bool>;

    public class DeleteTarjetaCommandHandler : IRequestHandler<DeleteTarjetaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTarjetaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tarjetas
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) return false;

            _context.Tarjetas.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
