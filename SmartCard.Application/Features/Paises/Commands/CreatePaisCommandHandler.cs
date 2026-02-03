using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Paises.Commands
{
    public record CreatePaisCommand(string Nombre, string CodigoIso2) : IRequest<int>;
    public class CreatePaisCommandHandler : IRequestHandler<CreatePaisCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreatePaisCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreatePaisCommand request, CancellationToken ct)
        {
            var e = new Domain.Entities.Pais { Nombre = request.Nombre, CodigoIso2 = request.CodigoIso2, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.Pais.Add(e); await _context.SaveChangesAsync(ct); return e.IdPais;
        }
    }
}
