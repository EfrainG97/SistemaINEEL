using Microsoft.EntityFrameworkCore;
using Proyecto_Ord.Models;

namespace Proyecto_Ord.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Models.Auditoria> Auditoria { get; set; }
        public DbSet<Models.Consecutivo> Consecutivos { get; set; }
        public DbSet<Models.Reporte> Reportes { get; set; }
        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Rol> Roles { get; set; }
        public DbSet<Models.Sistema> Sistema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed de Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolID = 1, NombreRol = "SuperAdmin" },
                new Rol { RolID = 2, NombreRol = "Admin" },
                new Rol { RolID = 3, NombreRol = "Usuario" }
            );

            // Seed de Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioID = 1, NumEmpleado = 1001, NombreUsuario = "Super Admin", Password = "superadminpass", IDRol = 1 },
                new Usuario { UsuarioID = 2, NumEmpleado = 1002, NombreUsuario = "Admin", Password = "adminpass", IDRol = 2 },
                new Usuario { UsuarioID = 3, NumEmpleado = 1003, NombreUsuario = "Usuario", Password = "userpass", IDRol = 3 }
            );

            modelBuilder.Entity<Sistema>().HasData(
                new Sistema { SistemaID = 1, Gerencia = "GER" }
            );

        }

    }
}
