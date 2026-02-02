using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.FormatosTarjeta.Commands
{
    public record DeleteFormatoCommand(int Id) : IRequest<bool>;
    public class DeleteFormatoCommandHandler : IRequestHandler<DeleteFormatoCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeleteFormatoCommand request, CancellationToken ct)
        {
            var e = await _context.FormatosTarjeta.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.FormatosTarjeta.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}
