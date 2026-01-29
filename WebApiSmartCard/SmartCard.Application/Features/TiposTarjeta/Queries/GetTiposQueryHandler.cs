using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.TiposTarjeta.Queries
{
    public record GetTiposQuery : IRequest<List<TipoTarjetaDto>>;
    public class GetTiposQueryHandler : IRequestHandler<GetTiposQuery, List<TipoTarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTiposQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<TipoTarjetaDto>> Handle(GetTiposQuery request, CancellationToken cancellationToken)
            => await _context.TiposTarjeta.AsNoTracking().ProjectTo<TipoTarjetaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
