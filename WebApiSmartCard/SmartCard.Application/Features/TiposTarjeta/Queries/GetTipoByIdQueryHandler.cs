using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.TiposTarjeta.Queries
{    public record GetTipoByIdQuery(int Id) : IRequest<TipoTarjetaDto?>;
    public class GetTipoByIdQueryHandler : IRequestHandler<GetTipoByIdQuery, TipoTarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTipoByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<TipoTarjetaDto?> Handle(GetTipoByIdQuery request, CancellationToken cancellationToken)
            => await _context.TiposTarjeta.AsNoTracking().Where(e => e.IdTipo == request.Id).ProjectTo<TipoTarjetaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}
