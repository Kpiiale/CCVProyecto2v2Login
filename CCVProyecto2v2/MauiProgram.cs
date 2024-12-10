using CCVProyecto2v2.DataAccess;
using CCVProyecto2v2.ViewsAdmin;
using CCVProyecto2v2.ViewsClase;
using CCVProyecto2v2.ViewsEstudiante;
using CCVProyecto2v2.ViewsModels;
using CCVProyecto2v2.ViewsProfesor;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CCVProyecto2v2.Models;
namespace CCVProyecto2v2;

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
                fonts.AddFont("TheStudentsTeacher-Regular.ttf", "TheStudentsTeacherFont");
                fonts.AddFont("Schoolwork-Regular.ttf", "SchoolworkFont");
            });

    
        builder.Services.AddDbContext<DbbContext>(options =>
            options.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "CCVProyecto2.db")}"));

    
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DbbContext>()
            .AddDefaultTokenProviders();

    
        builder.Services.AddTransient<DataSeeder>();

      
        builder.Services.AddTransient<AuthService>(sp =>
        {
            var signInManager = sp.GetRequiredService<SignInManager<ApplicationUser>>();
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            return new AuthService(signInManager, userManager);
        });

        builder.Services.AddTransient<AgregarEstudianteView>();
        builder.Services.AddTransient<EstudianteViewModel>();
        builder.Services.AddTransient<EMainPage>();
        builder.Services.AddTransient<EMainViewModel>();

        builder.Services.AddTransient<AgregarProfesorView>();
        builder.Services.AddTransient<PMainPage>();
        builder.Services.AddTransient<PMainViewModel>();
        builder.Services.AddTransient<ProfesorViewModel>();

        builder.Services.AddTransient<CMainViewModel>();
        builder.Services.AddTransient<ClaseViewModel>();
        builder.Services.AddTransient<AgregarClaseView>();
        builder.Services.AddTransient<CMainPage>();

        builder.Services.AddTransient<UEMainViewModel>();
        builder.Services.AddTransient<UnirEViewModel>();
        builder.Services.AddTransient<UnirEstudianteView>();

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<DbbContext>();
            dbContext.Database.EnsureCreated();

            var seeder = services.GetRequiredService<DataSeeder>();
            Task.Run(async () => await seeder.SeedAsync()).Wait();
        }

        Routing.RegisterRoute(nameof(AgregarEstudianteView), typeof(AgregarEstudianteView));
        Routing.RegisterRoute(nameof(AgregarProfesorView), typeof(AgregarProfesorView));
        Routing.RegisterRoute(nameof(AgregarClaseView), typeof(AgregarClaseView));
        Routing.RegisterRoute(nameof(UnirEstudianteView), typeof(UnirEstudianteView));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}