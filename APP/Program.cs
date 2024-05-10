using APP.Services;
using APP.Services.IService;
using APP.Utils;

namespace APP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<IConfigService, ConfigService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:API"]));
            builder.Services.AddHttpClient<IClientService, ClientService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:API"]));
            builder.Services.AddHttpClient<IProductServices, ProductService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:API"]));
            builder.Services.AddHttpClient<IRecipesService, RecipeService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:API"]));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);

            });

            var app = builder.Build();
            app.UseSession();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}