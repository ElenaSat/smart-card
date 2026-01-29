using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Cuentas.Queries
{
    // Query: GetAll
    public record GetCuentasQuery : IRequest<List<CuentaDto>>;

    public class GetCuentasQueryHandler : IRequestHandler<GetCuentasQuery, List<CuentaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCuentasQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CuentaDto>> Handle(GetCuentasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cuentas
                .AsNoTracking()
                .ProjectTo<CuentaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

    // Query: GetById
    public record GetCuentaByIdQuery(int Id) : IRequest<CuentaDto?>;

    public class GetCuentaByIdQueryHandler : IRequestHandler<GetCuentaByIdQuery, CuentaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCuentaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CuentaDto?> Handle(GetCuentaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cuentas
                .AsNoTracking()
                .Where(x => x.IdCuenta == request.Id)
                .ProjectTo<CuentaDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

namespace SmartCard.Application.Cuentas.Commands
{
    // Command: Create
    public record CreateCuentaCommand : IRequest<int>
    {
        public string? Numero { get; set; }
        public int? IdUsuario { get; set; }
    }

    public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCuentaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Cuenta>(request);
            
            // Audit
            entity.FechaCreacion = DateTime.UtcNow;
            entity.UsuarioCreacion = 1;

            _context.Cuentas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdCuenta;
        }
    }

    // Command: Update
    public record UpdateCuentaCommand : IRequest<bool>
    {
        public int IdCuenta { get; set; }
        public string? Numero { get; set; }
        public int? IdUsuario { get; set; }
    }

    public class UpdateCuentaCommandHandler : IRequestHandler<UpdateCuentaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCuentaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cuentas
                .FindAsync(new object[] { request.IdCuenta }, cancellationToken);

            if (entity == null) return false;

            entity.Numero = request.Numero;
            entity.IdUsuario = request.IdUsuario;
            
            // Audit
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = 1;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    // Command: Delete
    public record DeleteCuentaCommand(int Id) : IRequest<bool>;

    public class DeleteCuentaCommandHandler : IRequestHandler<DeleteCuentaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCuentaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cuentas
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) return false;

            _context.Cuentas.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
