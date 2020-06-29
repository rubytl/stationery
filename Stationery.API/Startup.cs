using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Stationery.API.Configuration;
using Stationery.Common.Entities;
using Stationery.Common.Helpers;
using Stationery.Data;
using Stationery.Data.Configuration;
using Stationery.Manager.Configuration;
using Stationery.Membership.Data;
using Stationery.Membership.Data.Configuration;
using System;
using System.Text;

namespace Stationery.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add catalog db context
            EFConfiguration.ConfigureService(services, Configuration);

            // IOC containers / Entity Framework
            StationeryConfiguration.ConfigureService(services);
            CatalogConfiguration.ConfigureService(services);

            // config connections options
            ConfigurationOptions.ConfigureService(services, this.Configuration);

            ManagerConfiguration.ConfigureService(services);

            // config jwtfactory and authenticatioin
            this.ConfigAuthentication(services);

            // Add framework services.
            services.AddLogging();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            RoleManager<AppIdentityRole> roleManager, UserManager<AppIdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net(Configuration.GetValue<string>("Log4NetConfigFile:Name"));
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            CatalogDbInitializer.Seed(app);
            MonitorDbInitializer.Seed(app, roleManager, userManager, loggerFactory);
        }

        /// <summary>
        /// Configurations the authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        private void ConfigAuthentication(IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Get options from app settings
            var jwtAppSettingOptions = this.Configuration.GetSection(nameof(JwtIssuerOptions));

            string validFor = jwtAppSettingOptions[nameof(JwtIssuerOptions.ValidFor)];
            double dbValidFor;
            double.TryParse(validFor, out dbValidFor);

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.ValidFor = TimeSpan.FromMinutes(dbValidFor);
                options.SigningKey = jwtAppSettingOptions[nameof(JwtIssuerOptions.SigningKey)];
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions[nameof(JwtIssuerOptions.SigningKey)])),

                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
        }
    }
}
