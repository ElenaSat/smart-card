using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Paises.Queries
{
    public record GetPaisByIdQuery(int Id) : IRequest<PaisDto?>;
    public class GetPaisByIdQueryHandler : IRequestHandler<GetPaisByIdQuery, PaisDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPaisByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<PaisDto?> Handle(GetPaisByIdQuery request, CancellationToken cancellationToken)
            => await _context.Pais.AsNoTracking().Where(e => e.IdPais == request.Id).ProjectTo<PaisDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}
