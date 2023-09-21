
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;

namespace CamargoInmobiliaria
{

    public class Program{

private readonly IConfiguration configuration;

       
        public Program(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public static void Main(string[] args)
		{
			//En visual studio este el "run" recomendado:
			//CreateKestrel(args).Build().Run();
			//En VS Code este otro es el "run" recomendado:
			//CreateKestrel(args).Build().Run();
		
 var builder = WebApplication.CreateBuilder(args);
		


	builder.Services.AddControllersWithViews();	

		  builder.Services.AddAuthorization(options =>
			{	
				options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
				options.AddPolicy("Empleado" , policy => policy.RequireRole("Empleado"));	
			});

	
			builder.Services.AddMvc();
			builder.Services.AddSignalR();//añade signalR
			//IUserIdProvider permite cambiar el ClaimType usado para obtener el UserIdentifier en Hub
			//builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

			



         /*builder.Services.AddDbContext<DataContext>(
				options => options.UseMySql(
					configuration["ConnectionStrings:DefaultConnection"],
					ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"])
				)
				);*/
				
		
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	
		
		// Add services to the container.			

Microsoft.AspNetCore.Authentication.AuthenticationBuilder authenticationBuilder = builder.Services.AddAuthentication()
				.AddCookie(options =>//el sitio web valida con cookie
				{
					options.LoginPath = "/Usuarios/Login";
					options.LogoutPath = "/Usuarios/Logout";
					options.AccessDeniedPath = "/Home/Restringido";
				});


	var app = builder.Build();	
	if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
			// Uso de archivos estáticos (*.html, *.css, *.js, etc.)
			app.UseStaticFiles();
			app.UseRouting();
			// Permitir cookies
			app.UseCookiePolicy(new CookiePolicyOptions
			{
				MinimumSameSitePolicy = SameSiteMode.None,
			});
			// Habilitar autenticación
			app.UseAuthentication();
			app.UseAuthorization();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("login", "entrar/{**accion}", new { controller = "Usuarios", action = "Login" });
				endpoints.MapControllerRoute("rutaFija", "ruteo/{valor}", new { controller = "Home", action = "Ruta", valor = "defecto" });
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
				
			});
			app.Run();
		}

		public static IWebHostBuilder CreateKestrel(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();
			var host = new WebHostBuilder()
				.UseConfiguration(config)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				//.UseUrls("http://localhost:5000", "https://localhost:5001")//permite escuchar SOLO peticiones locales
				.UseUrls("http://*:5000", "https://*:5001")//permite escuchar peticiones locales y remotas
				.UseIISIntegration();
			return host;
		}
		
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var host = WebHost.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();//limpia los proveedores x defecto de log (consola+depuración)
					logging.AddConsole();//agrega log de consola
					//logging.AddConfigur(new LoggerConfiguration().WriteTo.File("serilog.txt").CreateLogger())
				});
			return host;
		}
	}
}