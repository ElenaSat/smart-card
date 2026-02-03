using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.TiposTarjeta.Commands
{    public record DeleteTipoCommand(int Id) : IRequest<bool>;
    public class DeleteTipoCommandHandler : IRequestHandler<DeleteTipoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteTipoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteTipoCommand request, CancellationToken ct)
        {
            var e = await _context.TiposTarjeta.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.TiposTarjeta.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}
