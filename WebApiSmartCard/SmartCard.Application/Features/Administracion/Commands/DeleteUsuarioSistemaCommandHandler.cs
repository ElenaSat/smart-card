using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Administracion.Commands
{    public record DeleteUsuarioSistemaCommand(int Id) : IRequest<bool>;
    public class DeleteUsuarioSistemaCommandHandler : IRequestHandler<DeleteUsuarioSistemaCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteUsuarioSistemaCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteUsuarioSistemaCommand request, CancellationToken ct)
        {
            var e = await _context.UsuarioSistema.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.UsuarioSistema.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}
