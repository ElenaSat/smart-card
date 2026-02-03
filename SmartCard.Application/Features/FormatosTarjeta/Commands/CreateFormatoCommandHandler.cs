using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Features.FormatosTarjeta.Commands
{
    public record CreateFormatoCommand(string Nombre) : IRequest<int>;
    public class CreateFormatoCommandHandler : IRequestHandler<CreateFormatoCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateFormatoCommandHandler(IApplicationDbContext context) { _context = context; }
        public async Task<int> Handle(CreateFormatoCommand request, CancellationToken ct)
        {
            var e = new FormatoTarjeta { Nombre = request.Nombre, FechaCreacion = DateTime.UtcNow, UsuarioCreacion = 1 };
            _context.FormatosTarjeta.Add(e); await _context.SaveChangesAsync(ct); return e.IdFormato;
        }
    }
}
