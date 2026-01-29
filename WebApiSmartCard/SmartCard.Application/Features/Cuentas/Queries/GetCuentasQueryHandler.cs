using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Cuentas.Queries
{
    // Query: GetAll
    public record GetCuentasQuery : IRequest<List<CuentaDto>>;

    public class GetCuentasQueryHandler : IRequestHandler<GetCuentasQuery, List<CuentaDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCuentasQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CuentaDto>> Handle(GetCuentasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cuentas
                .AsNoTracking()
                .ProjectTo<CuentaDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }


}
