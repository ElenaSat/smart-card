using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Administracion.Commands
{    public record UpdateUsuarioSistemaCommand(int IdUsuarioSistema, string NombreUsuario) : IRequest<bool>;
    public class UpdateUsuarioSistemaCommandHandler : IRequestHandler<UpdateUsuarioSistemaCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUsuarioSistemaCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(UpdateUsuarioSistemaCommand request, CancellationToken ct)
        {
            var e = await _context.UsuarioSistema.FindAsync(new object[] { request.IdUsuarioSistema }, ct);
            if (e == null) return false;
            e.NombreUsuario = request.NombreUsuario;
            await _context.SaveChangesAsync(ct); return true;
        }
    }
}
