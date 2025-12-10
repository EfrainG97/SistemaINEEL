using ServiciosAPI.Interfaces;
using ServiciosAPI.Services;

#region Configuración de Servicios
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IUsuarioService, UsuarioService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new UsuarioService(httpClient, configuration);
});

builder.Services.AddScoped<IReporteService, ReporteService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new ReporteService(httpClient, configuration);
});

builder.Services.AddScoped<IConsecutivoService, ConsecutivoService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new ConsecutivoService(httpClient, configuration);
});

builder.Services.AddScoped<ISistemaService, SistemaService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new SistemaService(httpClient, configuration);
});

builder.Services.AddScoped<IRolService, RolService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new RolService(httpClient, configuration);
});

builder.Services.AddScoped<IAuditoriaService, AuditoriaService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new AuditoriaService(httpClient, configuration);
});
#endregion

#region Configuración de Middleware
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");
#endregion

app.Run();

