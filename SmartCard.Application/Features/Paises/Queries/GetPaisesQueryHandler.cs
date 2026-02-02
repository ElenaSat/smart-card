using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Paises.Queries
{
    public record GetPaisesQuery : IRequest<List<PaisDto>>;
    public class GetPaisesQueryHandler : IRequestHandler<GetPaisesQuery, List<PaisDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPaisesQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<List<PaisDto>> Handle(GetPaisesQuery request, CancellationToken cancellationToken)
            => await _context.Pais.AsNoTracking().ProjectTo<PaisDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

}
