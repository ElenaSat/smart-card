using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Usuarios.Queries;

public record GetUsuariosQuery : IRequest<List<UsuarioDto>>;

public class GetUsuariosQueryHandler : IRequestHandler<GetUsuariosQuery, List<UsuarioDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsuariosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UsuarioDto>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
