using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Administracion.Queries
{
    public record GetUsuarioSistemasQuery : IRequest<List<UsuarioSistemaDto>>;
    public class GetUsuarioSistemasQueryHandler : IRequestHandler<GetUsuarioSistemasQuery, List<UsuarioSistemaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUsuarioSistemasQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<UsuarioSistemaDto>> Handle(GetUsuarioSistemasQuery request, CancellationToken cancellationToken)
            => await _context.UsuarioSistema.AsNoTracking().ProjectTo<UsuarioSistemaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
