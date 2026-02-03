using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Tarjetas.Commands
{
    // Command: Delete
    public record DeleteTarjetaCommand(int Id) : IRequest<bool>;

    public class DeleteTarjetaCommandHandler : IRequestHandler<DeleteTarjetaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTarjetaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tarjetas
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null) return false;

            _context.Tarjetas.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
