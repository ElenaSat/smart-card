using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Cuentas.Commands
{
    // Command: Delete
    public record DeleteCuentaCommand(int Id) : IRequest<bool>;

    public class DeleteCuentaCommandHandler : IRequestHandler<DeleteCuentaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCuentaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cuentas
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) return false;

            _context.Cuentas.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
