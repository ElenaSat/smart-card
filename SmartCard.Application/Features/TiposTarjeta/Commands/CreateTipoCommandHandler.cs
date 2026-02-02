using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Features.TiposTarjeta.Commands
{    
    public record CreateTipoCommand(string Nombre) : IRequest<int>;
    public class CreateTipoCommandHandler : IRequestHandler<CreateTipoCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreateTipoCommand request, CancellationToken ct)
        {
            var e = new TipoTarjeta { Nombre = request.Nombre, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.TiposTarjeta.Add(e); await _context.SaveChangesAsync(ct); return e.IdTipo;
        }
    }
}
