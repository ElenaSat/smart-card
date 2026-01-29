using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Paises.Commands
{
    public record UpdatePaisCommand(int IdPais, string Nombre, string CodigoIso2) : IRequest<bool>;
    public class UpdatePaisCommandHandler : IRequestHandler<UpdatePaisCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdatePaisCommand request, CancellationToken ct)
        {
            var e = await _context.Pais.FindAsync(new object[] { request.IdPais }, ct);
            if (e == null) return false;
            e.Nombre = request.Nombre; e.CodigoIso2 = request.CodigoIso2; e.FechaModificacion = DateTime.UtcNow; e.UsuarioModificacion = 1;
            await _context.SaveChangesAsync(ct); return true;
        }
    }


}
