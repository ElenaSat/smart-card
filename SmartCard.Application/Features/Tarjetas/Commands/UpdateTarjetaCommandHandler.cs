using MediatR;
using SmartCard.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCard.Application.Features.Tarjetas.Commands
{

    // Command: Update
    public record UpdateTarjetaCommand : IRequest<bool>
    {
        public int IdTarjeta { get; set; }
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

    public class UpdateTarjetaCommandHandler : IRequestHandler<UpdateTarjetaCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTarjetaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTarjetaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tarjetas
                .FindAsync(new object[] { request.IdTarjeta }, cancellationToken);

            if (entity == null) return false;

            entity.IdCuenta = request.IdCuenta;
            entity.IdFormato = request.IdFormato;
            entity.IdTipo = request.IdTipo;
            entity.Pan = request.Pan;
            entity.Pin = request.Pin;
            entity.FechaEmision = request.FechaEmision;
            entity.FechaExpiracion = request.FechaExpiracion;
            entity.IdPaisEmision = request.IdPaisEmision;
            entity.DdaHabilitado = request.DdaHabilitado;
            entity.ArqcHabilitado = request.ArqcHabilitado;
            entity.Track1 = request.Track1;
            entity.Track2 = request.Track2;
            entity.Track3 = request.Track3;

            // Audit
            entity.FechaModificacion = DateTime.UtcNow;
            entity.UsuarioModificacion = 1;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
