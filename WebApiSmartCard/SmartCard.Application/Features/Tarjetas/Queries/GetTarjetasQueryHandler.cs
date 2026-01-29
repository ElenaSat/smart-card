using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Tarjetas.Queries
{
    // Query: GetAll
    public record GetTarjetasQuery : IRequest<List<TarjetaDto>>;

    public class GetTarjetasQueryHandler : IRequestHandler<GetTarjetasQuery, List<TarjetaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTarjetasQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TarjetaDto>> Handle(GetTarjetasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tarjetas
                .AsNoTracking()
                .ProjectTo<TarjetaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

}
