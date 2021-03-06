using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSK.DB.Contexts;
using PSK.DB.SqlRepository;
using PSK.Model.Logging;
using PSK.Model.Repository;
using PSK.Model.Authorization;
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

            services.AddMvc(config =>
{
                config.Filters.Add(new AuthorizeFilter("Token"));
            });

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
                options.AddLocalization();
            });
            //services.AddDbContext<PSKDbContext>(options => options
            //    .UseLazyLoadingProxies()
            //    .UseMySql("Server=localhost;Database=DB;Password=PASS;User=USER"));
            services.AddDbContext<PSKDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseMySql(ConfigurationManager.AppSettings["DBConnectionString"]));
            InitializeContainer();
            InjectRepositories();
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
            string repositoryPluginDllName = Configuration.GetSection("Plugins")
                .GetValue<string>("RepositoriesDllPath");
            string servicePluginDllName = Configuration.GetSection("Plugins")
                .GetValue<string>("ServicesDllPath");
            string[] pluginsDirectoriesNames = { servicePluginDllName, repositoryPluginDllName};
            string file = Configuration.GetSection("Logging").GetValue<string>("File");
            LogLevel level = Configuration.GetSection("Logging").GetValue<LogLevel>("Level");
            Model.ObjectContainer.InitializeContainer(container, file, level, pluginsDirectoriesNames);
        }

        private void InjectRepositories()
        {
            string repositoryPluginDllName = Configuration.GetSection("Plugins")
                .GetValue<string>("RepositoriesDllPath");
            if (repositoryPluginDllName == "")
            {
                container.Register<IIncomingEmployeeRepository, IncomingEmployeeSqlRepository>(Lifestyle.Scoped);
                container.Register<IEmployeeRepository, EmployeeSqlRepository>(Lifestyle.Scoped);
                container.Register<ITopicRepository, TopicSqlRepository>(Lifestyle.Scoped);
                container.Register<IRecommendationsRepository, RecommendationsSqlRepository>(Lifestyle.Scoped);
                container.Register<IDayRepository, DaySqlRepository>(Lifestyle.Scoped);
                container.Register<IEmployeesTokenRepository, EmployeesTokenSqlRepository>(Lifestyle.Scoped);
                container.Register<ITopicCompletionRepository, TopicCompletionSqlRepository>(Lifestyle.Scoped);
                container.Register<IRestrictionRepository, RestrictionSqlRepository>(Lifestyle.Scoped);
            }
        }
    }
}
