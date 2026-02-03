using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Usuarios.Commands
{
    // Command: Delete
    public record DeleteUsuarioCommand(int Id) : IRequest<bool>;

    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUsuarioCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Usuarios
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) return false;

            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
