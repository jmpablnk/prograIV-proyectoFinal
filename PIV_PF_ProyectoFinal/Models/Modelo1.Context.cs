﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIV_PF_ProyectoFinal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FARMACIA_PROGRA_FINALEntities : DbContext
    {
        public FARMACIA_PROGRA_FINALEntities()
            : base("name=FARMACIA_PROGRA_FINALEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_CLIENTES> T_CLIENTES { get; set; }
        public virtual DbSet<T_FACTURAS> T_FACTURAS { get; set; }
        public virtual DbSet<T_PRODUCTOS> T_PRODUCTOS { get; set; }
        public virtual DbSet<T_ROLES> T_ROLES { get; set; }
        public virtual DbSet<T_TIPO_DE_PRODUCTOS> T_TIPO_DE_PRODUCTOS { get; set; }
        public virtual DbSet<T_USUARIOS> T_USUARIOS { get; set; }
    }
}