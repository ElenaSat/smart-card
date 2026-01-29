using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.FormatosTarjeta.Queries
{
    public record GetFormatosQuery : IRequest<List<FormatoTarjetaDto>>;
    public class GetFormatosQueryHandler : IRequestHandler<GetFormatosQuery, List<FormatoTarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetFormatosQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<FormatoTarjetaDto>> Handle(GetFormatosQuery request, CancellationToken cancellationToken)
            => await _context.FormatosTarjeta.AsNoTracking().ProjectTo<FormatoTarjetaDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
