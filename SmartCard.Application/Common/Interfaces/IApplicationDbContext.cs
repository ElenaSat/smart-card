using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCard.Domain.Entities;

namespace SmartCard.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Usuario> Usuarios { get; }
    DbSet<Cuenta> Cuentas { get; }
    DbSet<Tarjeta> Tarjetas { get; }
    DbSet<Pais> Pais { get; }
    DbSet<TipoTarjeta> TiposTarjeta { get; }
    DbSet<FormatoTarjeta> FormatosTarjeta { get; }
    DbSet<UsuarioSistema> UsuarioSistema { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
