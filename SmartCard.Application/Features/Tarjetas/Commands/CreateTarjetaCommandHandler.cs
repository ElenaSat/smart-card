using AutoMapper;
using MediatR;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCard.Application.Features.Tarjetas.Commands
{
    // Command: Create
    public record CreateTarjetaCommand : IRequest<int>
    {
        public int? IdCuenta { get; set; }
        public int? IdFormato { get; set; }
        public int? IdTipo { get; set; }
        public string? Pan { get; set; }
        public string? Pin { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int? IdPaisEmision { get; set; }
        public bool? DdaHabilitado { get; set; }
        public bool? ArqcHabilitado { get; set; }
        public string? Track1 { get; set; }
        public string? Track2 { get; set; }
        public string? Track3 { get; set; }
    }

    public class CreateTarjetaCommandHandler : IRequestHandler<CreateTarjetaCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTarjetaCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Tarjeta>(request);

            // Audit
            entity.FechaCreacion = DateTime.UtcNow;
            entity.UsuarioCreacion = 1;

            _context.Tarjetas.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdTarjeta;
        }
    }

}
