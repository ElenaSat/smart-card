using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.FormatosTarjeta.Commands
{
    public record UpdateFormatoCommand(int IdFormato, string Nombre) : IRequest<bool>;
    public class UpdateFormatoCommandHandler : IRequestHandler<UpdateFormatoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateFormatoCommand request, CancellationToken ct)
        {
            var e = await _context.FormatosTarjeta.FindAsync(new object[] { request.IdFormato }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
}
