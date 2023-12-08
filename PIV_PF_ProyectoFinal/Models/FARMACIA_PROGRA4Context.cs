using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PIV_PF_PROYECTOFINAL.Models
{
    public partial class FARMACIA_PROGRA4Context : DbContext
    {
        public FARMACIA_PROGRA4Context()
        {
        }

        public FARMACIA_PROGRA4Context(DbContextOptions<FARMACIA_PROGRA4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TCliente> TCliente { get; set; }
        public virtual DbSet<TFactura> TFactura { get; set; }
        public virtual DbSet<TProducto> TProducto { get; set; }
        public virtual DbSet<TRole> TRole { get; set; }
        public virtual DbSet<TTipoDeProducto> TTipoDeProducto { get; set; }
        public virtual DbSet<TUsuario> TUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("T_CLIENTES", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACION");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CLIENTE");
            });

            modelBuilder.Entity<TFactura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("T_FACTURAS", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdFactura)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_FACTURA");

                entity.Property(e => e.Estadoactivo)
                    .IsRequired()
                    .HasColumnName("ESTADOACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_COMPRA");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.MontoTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("MONTO_TOTAL");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TFacturas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_ID_CLIENTE");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TFacturas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_ID_PRODUCTO");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TFacturas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_ID_USUARIO");
            });

            modelBuilder.Entity<TProducto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("T_PRODUCTOS", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdTipoProducto).HasColumnName("ID_TIPO_PRODUCTO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PRECIO");

                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.TProductos)
                    .HasForeignKey(d => d.IdTipoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_PRODUCTO_TIPO_PRODUCTO");
            });

            modelBuilder.Entity<TRole>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("T_ROLES", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdRole)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_ROLE");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<TTipoDeProducto>(entity =>
            {
                entity.HasKey(e => e.IdTipoProducto);

                entity.ToTable("T_TIPO_DE_PRODUCTOS", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdTipoProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_TIPO_PRODUCTO");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.NombreTipoProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_TIPO_PRODUCTO");
            });

            modelBuilder.Entity<TUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("T_USUARIOS", "SCH_FARMACIA_PROGRA4");

                entity.HasIndex(e => e.Identificacion, "UQ__T_USUARI__6F9F6A3A59BC04DC")
                    .IsUnique();

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CLAVE");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.IdRole).HasColumnName("ID_ROLE");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.TUsuarios)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_USUARIO_T_ROLE_USUARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
