using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSK.DB.Contexts;
using PSK.DB.SqlRepository;
using PSK.Model.Logging;
using PSK.Model.Repository;
using PSK.Model.Services;
using SimpleInjector;
using System.Configuration;

namespace PSK.UI
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            container.Options.ResolveUnregisteredConcreteTypes = false;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, TokenHandler>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            services.AddScoped<IEmployeesTokenRepository, EmployeesTokenSqlRepository>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Token", policy =>
                    policy.Requirements.Add(new TokenRequirement()));
            });

            services.AddMvc();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation();
                //.AddPageModelActivation()
                //.AddTagHelperActivation();

                options.AddLocalization();
            });

            services.AddDbContext<PSKDbContext>(options => options.UseMySql(ConfigurationManager.AppSettings["DBConnectionString"]));
            InitializeContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            container.Verify();
        }

        private void InitializeContainer()
        {
            string file = Configuration.GetSection("Logging").GetValue<string>("File");
            LogLevel level = Configuration.GetSection("Logging").GetValue<LogLevel>("Level");
            Model.ObjectContainer.InitializeContainer(container, file, level);
            container.Register<IIncomingEmployeeRepository, IncomingEmployeeSqlRepository>(Lifestyle.Scoped);
            container.Register<IEmployeeRepository, EmployeeSqlRepository>(Lifestyle.Scoped);
            container.Register<ITopicRepository, TopicSqlRepository>(Lifestyle.Scoped);
            container.Register<IRecommendationRepository, RecommendationSqlRepository>(Lifestyle.Scoped);
            container.Register<IEmployeesTokenRepository, EmployeesTokenSqlRepository>(Lifestyle.Scoped);
        }
    }
}
