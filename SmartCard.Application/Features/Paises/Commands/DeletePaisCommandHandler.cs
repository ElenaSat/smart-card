using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Paises.Commands
{
    public record DeletePaisCommand(int Id) : IRequest<bool>;
    public class DeletePaisCommandHandler : IRequestHandler<DeletePaisCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeletePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<bool> Handle(DeletePaisCommand request, CancellationToken ct)
        {
            var e = await _context.Pais.FindAsync(new object[] { request.Id }, ct);
            if (e == null) return false;
            _context.Pais.Remove(e); await _context.SaveChangesAsync(ct); return true;
        }
    }
}
