using AutoMapper;
using MediatR;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Application.Features.Usuarios.Commands
{
    // Command: Update
    public record UpdateUsuarioCommand : IRequest<bool>
    {
        public int IdUsuario { get; set; }
        public string? Titulo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? InfoExtra { get; set; }
    }

    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUsuarioCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Usuarios
                .FindAsync(new object[] { request.IdUsuario }, cancellationToken);

            if (entity == null) return false;

            // Map updates
            entity.Titulo = request.Titulo;
            entity.Nombre = request.Nombre;
            entity.Apellido = request.Apellido;
            entity.InfoExtra = request.InfoExtra;

            // Audit
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = 1;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
