using AutoMapper;
using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Features.Cuentas.Commands
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

}
