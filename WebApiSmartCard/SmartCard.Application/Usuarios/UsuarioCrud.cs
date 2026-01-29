using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using SmartCard.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartCard.Application.Usuarios.Queries.GetUsuarioById
{
    // Query: GetById
    public record GetUsuarioByIdQuery(int Id) : IRequest<UsuarioDto?>;

    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsuarioByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsuarioDto?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(x => x.IdUsuario == request.Id)
                .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

namespace SmartCard.Application.Usuarios.Commands.CreateUsuario
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

namespace SmartCard.Application.Usuarios.Commands.UpdateUsuario
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

namespace SmartCard.Application.Usuarios.Commands.DeleteUsuario
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
