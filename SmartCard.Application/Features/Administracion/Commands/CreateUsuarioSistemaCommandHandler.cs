using AutoMapper;
using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Features.Administracion.Commands
{
    public record CreateUsuarioSistemaCommand(string NombreUsuario) : IRequest<int>;
    public class CreateUsuarioSistemaCommandHandler : IRequestHandler<CreateUsuarioSistemaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateUsuarioSistemaCommandHandler(IApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateUsuarioSistemaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UsuarioSistema>(request);
            entity.NombreUsuario = request.NombreUsuario;
            entity.FechaCreacion = DateTime.UtcNow;
            _context.UsuarioSistema.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdUsuarioSistema;
        }
    }
}
