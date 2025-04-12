using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskMaster.Data;

namespace Taskmaster
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Ajout du DbContext à l'injection de dépendance
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    "Server=localhost;Port=3306;Database=taskmanagerdb;User=root;Password=;",
                    new MySqlServerVersion(new Version(8, 0, 30))
                ));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
