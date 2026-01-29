using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.Cuentas.Queries
{
    // Query: GetById
    public record GetCuentaByIdQuery(int Id) : IRequest<CuentaDto?>;

    public class GetCuentaByIdQueryHandler : IRequestHandler<GetCuentaByIdQuery, CuentaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCuentaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CuentaDto?> Handle(GetCuentaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cuentas
                .AsNoTracking()
                .Where(x => x.IdCuenta == request.Id)
                .ProjectTo<CuentaDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
