using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Cuentas.Commands
{
    // Command: Update
    public record UpdateCuentaCommand : IRequest<bool>
    {
        public int IdCuenta { get; set; }
        public string? Numero { get; set; }
        public int? IdUsuario { get; set; }
    }

    public class UpdateCuentaCommandHandler : IRequestHandler<UpdateCuentaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCuentaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCuentaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cuentas
                .FindAsync(new object[] { request.IdCuenta }, cancellationToken);

            if (entity == null) return false;

            entity.Numero = request.Numero;
            entity.IdUsuario = request.IdUsuario;

            // Audit
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = 1;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
