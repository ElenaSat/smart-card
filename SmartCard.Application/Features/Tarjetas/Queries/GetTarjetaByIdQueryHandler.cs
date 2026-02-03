using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCard.Application.Features.Tarjetas.Queries
{
    // Query: GetById
    public record GetTarjetaByIdQuery(int Id) : IRequest<TarjetaDto?>;

    public class GetTarjetaByIdQueryHandler : IRequestHandler<GetTarjetaByIdQuery, TarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTarjetaByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TarjetaDto?> Handle(GetTarjetaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tarjetas
                .AsNoTracking()
                .Where(x => x.IdTarjeta == request.Id)
                .ProjectTo<TarjetaDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
