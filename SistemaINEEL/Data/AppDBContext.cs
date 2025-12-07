using Microsoft.EntityFrameworkCore;
using LibreriaModelos;

namespace SistemaINEEL.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<Consecutivo> Consecutivos { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Sistema> Sistema { get; set; }

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
