using Microsoft.EntityFrameworkCore;
using SmartCard.Domain.Entities;
using SmartCard.Application.Common.Interfaces;

namespace SmartCard.Infrastructure.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Cuenta> Cuentas { get; set; } = null!;
    public DbSet<Tarjeta> Tarjetas { get; set; } = null!;
    public DbSet<Pais> Pais { get; set; } = null!;
    public DbSet<TipoTarjeta> TiposTarjeta { get; set; } = null!;
    public DbSet<FormatoTarjeta> FormatosTarjeta { get; set; } = null!;
    public DbSet<UsuarioSistema> UsuarioSistema { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================================================
        // USUARIO → usuario (TABLA + TODAS LAS COLUMNAS)
        // =====================================================
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario", tb => tb.HasTrigger("trg_usuario_update"));

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Titulo).HasColumnName("titulo");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(40);
            entity.Property(e => e.Apellido).HasColumnName("apellido").HasMaxLength(40);
            entity.Property(e => e.InfoExtra).HasColumnName("info_extra");

            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
        });

        // =====================================================
        // CUENTA → cuenta
        // =====================================================
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.ToTable("cuenta", tb => tb.HasTrigger("trg_cuenta_update"));

            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.Numero).HasColumnName("numero").HasMaxLength(50);
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");

            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Cuentas)
                  .HasForeignKey(e => e.IdUsuario);
        });

        // =====================================================
        // TARJETA → tarjeta (más importante)
        // =====================================================
        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.ToTable("tarjeta", tb => tb.HasTrigger("trg_tarjeta_update"));

            entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");
            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.IdFormato).HasColumnName("id_formato");
            entity.Property(e => e.IdTipo).HasColumnName("id_tipo");
            entity.Property(e => e.Pan).HasColumnName("pan").HasMaxLength(32);
            entity.Property(e => e.Pin).HasColumnName("pin").HasMaxLength(20);
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaExpiracion).HasColumnName("fecha_expiracion");
            entity.Property(e => e.IdPaisEmision).HasColumnName("id_pais_emision");
            entity.Property(e => e.DdaHabilitado).HasColumnName("dda_habilitado");
            entity.Property(e => e.ArqcHabilitado).HasColumnName("arqc_habilitado");
            entity.Property(e => e.Track1).HasColumnName("track1");
            entity.Property(e => e.Track2).HasColumnName("track2");
            entity.Property(e => e.Track3).HasColumnName("track3");

            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");

            // FOREIGN KEYS
            entity.HasOne(e => e.Cuenta).WithMany(c => c.Tarjetas).HasForeignKey(e => e.IdCuenta);
            entity.HasOne(e => e.FormatoTarjeta).WithMany(f => f.Tarjetas).HasForeignKey(e => e.IdFormato);
            entity.HasOne(e => e.TipoTarjeta).WithMany(t => t.Tarjetas).HasForeignKey(e => e.IdTipo);
            entity.HasOne(e => e.Pais).WithMany(p => p.Tarjetas).HasForeignKey(e => e.IdPaisEmision);
        });

        // =====================================================
        // CATÁLOGOS (rápido)
        // =====================================================
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.ToTable("pais", tb => tb.HasTrigger(" trg_pais_update"));
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.CodigoIso2).HasColumnName("codigo_iso2");
            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
        });

        modelBuilder.Entity<TipoTarjeta>(entity =>
        {
            entity.ToTable("tipo_tarjeta", tb => tb.HasTrigger("trg_tipo_tarjeta_update"));
            entity.Property(e => e.IdTipo).HasColumnName("id_tipo");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
        });

        modelBuilder.Entity<FormatoTarjeta>(entity =>
        {
            entity.ToTable("formato_tarjeta", tb => tb.HasTrigger("trg_formato_tarjeta_update"));
            entity.Property(e => e.IdFormato).HasColumnName("id_formato");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuario_creacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuario_modificacion");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.ToTable("usuario_sistema");
            entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");
            entity.Property(e => e.NombreUsuario).HasColumnName("nombre_usuario");

            // AUDITORÍA
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");           
        });
    }

}
