using AutoMapper;
using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCard.Application.Features.Usuarios.Commands
{
    // Command: Create
    public record CreateUsuarioCommand : IRequest<int>
    {
        public string? Titulo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? InfoExtra { get; set; }
    }

    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUsuarioCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Usuario>(request);

            // Audit (Hardcoded for now as per instructions, ideally use a CurrentUserService)
            entity.FechaCreacion = DateTime.UtcNow;
            entity.UsuarioCreacion = 1;

            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdUsuario;
        }
    }

}
