//namespace DailyRoutine.Data
//{
//    public class ApplicationDbContextFactory
//    {
//    }
//}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DailyRoutine.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 1️⃣ Cargar configuración desde appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // 2️⃣ Obtener la cadena de conexión
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 3️⃣ Configurar las opciones del contexto
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // 4️⃣ Crear y devolver el contexto
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
