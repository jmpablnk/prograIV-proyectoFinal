using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PIV_PF_ProyectoFinal.Models
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

        public virtual DbSet<Cliente> Cliente { get; set; } 
        public virtual DbSet<DetallesFactura> DetallesFactura { get; set; } 
        public virtual DbSet<Factura> Factura { get; set; } 
        public virtual DbSet<Producto> Producto { get; set; } 
        public virtual DbSet<TiposProducto> TiposProducto { get; set; } 
        public virtual DbSet<Usuario> Usuario { get; set; } 

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=MSI\\SQL2019_DEV;Initial Catalog=FARMACIA_PROGRA4;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False; Trusted_Connection=True; TrustServerCertificate=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdClientes);

                entity.ToTable("Clientes", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdClientes).HasColumnName("ID_CLIENTES");

                entity.Property(e => e.Correo).HasMaxLength(100);

                entity.Property(e => e.Identificacion).HasMaxLength(20);

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Cliente");
            });

            modelBuilder.Entity<DetallesFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetallesFactura);

                entity.ToTable("DetallesFactura", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdDetallesFactura).HasColumnName("Id_DETALLES_FACTURA");

                entity.Property(e => e.CodigoFactura)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_FACTURA");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_PRODUCTO");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CodigoFacturaNavigation)
                    .WithMany(p => p.DetallesFacturas)
                    .HasForeignKey(d => d.CodigoFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLES_FACTURAS_CODIGO");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.DetallesFacturas)
                    .HasForeignKey(d => d.CodigoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLES_PRODUCTOS_CODIGO");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.CodigoFactura);

                entity.ToTable("Facturas", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.CodigoFactura)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_FACTURA");

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.IdClientes).HasColumnName("ID_CLIENTES");

                entity.Property(e => e.MetodoPago).HasMaxLength(20);

                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdClientes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURAS_CLIENTES_ID");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.CodigoProducto);

                entity.ToTable("Productos", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_PRODUCTO");

                entity.Property(e => e.CodigoTipoProducto)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_TIPO_Producto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(255)
                    .HasColumnName("Descripcion_PRODUCTO");

                entity.Property(e => e.Estado).HasMaxLength(20);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.CodigoTipoProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CodigoTipoProducto)
                    .HasConstraintName("FK_PRODUCTOS_TIPO_PRODUCTO_CODIGO");
            });

            modelBuilder.Entity<TiposProducto>(entity =>
            {
                entity.HasKey(e => e.CodigoTipoProducto);

                entity.ToTable("TiposProductos", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.CodigoTipoProducto)
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_TIPO_Producto");

                entity.Property(e => e.DescripcionTipoProducto)
                    .HasMaxLength(255)
                    .HasColumnName("Descripcion_TIPO_Producto");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIOS", "SCH_FARMACIA_PROGRA4");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_USUARIO");

                entity.Property(e => e.ContrasenaUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("Contrasena_USUARIO");

                entity.Property(e => e.CorreoUsuario)
                    .HasMaxLength(100)
                    .HasColumnName("CORREO_USUARIO");

                entity.Property(e => e.EstadoUsuario)
                    .HasMaxLength(10)
                    .HasColumnName("Estado_USUARIO");

                entity.Property(e => e.IdentificacionUsuario)
                    .HasMaxLength(20)
                    .HasColumnName("IDENTIFICACION_USUARIO");

                entity.Property(e => e.NombreCompletoUsuario)
                    .HasMaxLength(100)
                    .HasColumnName("NOMBRE_COMPLETO_USUARIO");

                entity.Property(e => e.TipoUsuario)
                    .HasMaxLength(20)
                    .HasColumnName("Tipo_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
