using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.TiposTarjeta.Commands
{
    public record UpdateTipoCommand(int IdTipo, string Nombre) : IRequest<bool>;
    public class UpdateTipoCommandHandler : IRequestHandler<UpdateTipoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateTipoCommand request, CancellationToken ct)
        {
            var e = await _context.TiposTarjeta.FindAsync(new object[] { request.IdTipo }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
}
