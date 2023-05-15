
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplicationLibrary.Repositories;

namespace WebApplicationLibrary
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Library"));

            services.AddTransient<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = serviceProvider.GetService<LibraryContext>();
            var initializer = new LibraryContextInitializer(context);
            initializer.SeedAsync();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "books",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                 name: "books",
                pattern: "api/[controller]/{action=GetBooks}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
