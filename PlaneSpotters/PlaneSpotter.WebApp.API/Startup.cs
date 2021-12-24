using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PlaneSpotters.Core.Configuration;
using PlaneSpotters.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Interfaces;
using PlaneSpotters.Services.UserManagment;
using AutoMapper;
using PlaneSpotters.Core.Mapping;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlaneSpotters.Services.SpotterManagment;
using PlaneSpotters.DataAccess.Repository;
using PlaneSpotters.DataAccess;

namespace PlaneSpotter.WebApp.API
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
            services.Configure<DBConfiguration>(Configuration.GetSection("ConnectionString"));
            services.AddDbContext<PlaneSpotterDBContext>((provider, options) =>
            options.UseSqlServer(provider.GetRequiredService<IOptions<DBConfiguration>>().Value.DefaultConnectionString));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        RequireSignedTokens = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<PlaneSpotterDBContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "spotter.api",
                    ValidAudience = "planespotter",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlaneSpotter.WebApp.API", Version = "v1" });
            });

            services.AddScoped<IBaseRepository<PlaneSpotters.Core.Entities.PlaneSpotter>, BaseRepository<PlaneSpotters.Core.Entities.PlaneSpotter>>((provider) =>
                new BaseRepository<PlaneSpotters.Core.Entities.PlaneSpotter>(provider.GetService<PlaneSpotterDBContext>().Set<PlaneSpotters.Core.Entities.PlaneSpotter>()));

            services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => new UnitOfWork(provider.GetService<PlaneSpotterDBContext>()));

            services.AddSingleton((provider) => new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISpotterService, SpotterService>();
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PlaneSpotterDBContext>())
                {
                    try
                    {
                        //context.Database.EnsureCreated();
                        context.Database.Migrate();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaneSpotter.WebApp.API v1"));
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseStaticFiles();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
