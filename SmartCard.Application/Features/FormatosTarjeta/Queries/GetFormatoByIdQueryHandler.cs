using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;

namespace SmartCard.Application.Features.FormatosTarjeta.Queries
{
    public record GetFormatoByIdQuery(int Id) : IRequest<FormatoTarjetaDto?>;
    public class GetFormatoByIdQueryHandler : IRequestHandler<GetFormatoByIdQuery, FormatoTarjetaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetFormatoByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<FormatoTarjetaDto?> Handle(GetFormatoByIdQuery request, CancellationToken cancellationToken)
            => await _context.FormatosTarjeta.AsNoTracking().Where(e => e.IdFormato == request.Id).ProjectTo<FormatoTarjetaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}
