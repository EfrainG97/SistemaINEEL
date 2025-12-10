using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaAPI.Data;
using LibreriaModelos;

namespace SistemaAPI.Commands
{
    public static class ClearDatabaseCommand
    {
        public static async Task ExecuteAsync(string[] args)
        {
            // Verificar si se pasó el argumento cleardb
            if (!args.Contains("cleardb"))
            {
                return;
            }

            Console.WriteLine("=== Comando: Vaciar Base de Datos ===");
            Console.WriteLine("Este proceso eliminará todos los datos excepto los datos iniciales.");
            Console.Write("¿Estás seguro de continuar? (s/n): ");
            
            var response = Console.ReadLine()?.ToLower().Trim();
            if (response != "s" && response != "si" && response != "y" && response != "yes")
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            // Configurar el DbContext usando appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("AppDBContext") 
                ?? throw new InvalidOperationException("Connection string 'AppDBContext' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlite(connectionString);

            using var context = new AppDBContext(optionsBuilder.Options);
            
            try
            {
                Console.WriteLine("\nIniciando proceso de vaciado de base de datos...");

                // Usar transacción para garantizar atomicidad
                using var transaction = await context.Database.BeginTransactionAsync();
                
                try
                {
                    // 1. Eliminar todos los registros de Auditoria
                    var auditoriaCount = await context.Auditoria.CountAsync();
                    if (auditoriaCount > 0)
                    {
                        context.Auditoria.RemoveRange(context.Auditoria);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {auditoriaCount} registros de Auditoria");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Auditoria ya está vacía");
                    }

                    // 2. Eliminar todos los registros de Reporte
                    var reporteCount = await context.Reporte.CountAsync();
                    if (reporteCount > 0)
                    {
                        context.Reporte.RemoveRange(context.Reporte);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {reporteCount} registros de Reporte");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Reporte ya está vacía");
                    }

                    // 3. Eliminar todos los registros de Consecutivo
                    var consecutivoCount = await context.Consecutivo.CountAsync();
                    if (consecutivoCount > 0)
                    {
                        context.Consecutivo.RemoveRange(context.Consecutivo);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {consecutivoCount} registros de Consecutivo");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Consecutivo ya está vacía");
                    }

                    // 4. Eliminar Usuarios excepto los seed data (ID 1 y 2)
                    var usuariosToDelete = await context.Usuario
                        .Where(u => u.UsuarioID != 1 && u.UsuarioID != 2)
                        .ToListAsync();
                    
                    if (usuariosToDelete.Any())
                    {
                        context.Usuario.RemoveRange(usuariosToDelete);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {usuariosToDelete.Count} registros de Usuario (mantenidos seed data: ID 1, 2)");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Usuario solo contiene datos iniciales");
                    }

                    // 5. Eliminar Sistemas excepto el seed data (ID 1)
                    var sistemasToDelete = await context.Sistema
                        .Where(s => s.SistemaID != 1)
                        .ToListAsync();
                    
                    if (sistemasToDelete.Any())
                    {
                        context.Sistema.RemoveRange(sistemasToDelete);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {sistemasToDelete.Count} registros de Sistema (mantenido seed data: ID 1)");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Sistema solo contiene datos iniciales");
                    }

                    // 6. Eliminar Roles excepto los seed data (ID 1 y 2)
                    var rolesToDelete = await context.Rol
                        .Where(r => r.RolID != 1 && r.RolID != 2)
                        .ToListAsync();
                    
                    if (rolesToDelete.Any())
                    {
                        context.Rol.RemoveRange(rolesToDelete);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"✓ Eliminados {rolesToDelete.Count} registros de Rol (mantenidos seed data: ID 1, 2)");
                    }
                    else
                    {
                        Console.WriteLine("✓ Tabla Rol solo contiene datos iniciales");
                    }

                    // Confirmar transacción
                    await transaction.CommitAsync();
                    Console.WriteLine("\n✓ Proceso completado exitosamente.");
                    Console.WriteLine("✓ Base de datos vaciada. Los datos iniciales se han preservado.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"\n✗ Error durante el proceso: {ex.Message}");
                    Console.WriteLine("✗ La transacción ha sido revertida. No se realizaron cambios.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error fatal: {ex.Message}");
                Console.WriteLine($"✗ Detalles: {ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }
}

