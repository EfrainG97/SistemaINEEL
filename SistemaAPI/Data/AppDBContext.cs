using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibreriaModelos;

namespace SistemaAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {

        }

        public DbSet<Consecutivo> Consecutivo { get; set; }
        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Sistema> Sistema { get; set; }
        public DbSet<Reporte> Reporte { get; set; }
        public DbSet<Rol> Rol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rol>().HasKey(x => x.RolID);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolID = 1, NombreRol = "Admin" },
                new Rol { RolID = 2, NombreRol = "Usuario" }
            );

            modelBuilder.Entity<Sistema>().HasKey(x => x.SistemaID);

            modelBuilder.Entity<Sistema>().HasData(
                new Sistema { SistemaID = 1, Gerencia = "ALS" }
            );

            modelBuilder.Entity<Usuario>().HasKey(x => x.UsuarioID);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioID = 1, NumEmpleado = 3248, NombreUsuario = "Olga Vázquez", Password = "pass", IDRol = 1 },
                new Usuario { UsuarioID = 2, NumEmpleado = 1001, NombreUsuario = "Usuario Comun", Password = "userpass", IDRol = 2 }
            );
        }
    }
}
