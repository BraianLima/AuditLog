using AuditLog.Core.Domain.DTOs.Mapping;
using AuditLog.Core.Domain.Extensions;
using AuditLog.Core.Domain.Handlers;
using AuditLog.Core.Domain.Handlers.Interfaces;
using AuditLog.Core.Domain.Handlers.Validators;
using AuditLog.Core.Domain.Utils;
using AuditLog.Core.Infra.Data.Context;
using AuditLog.Core.Infra.Data.Repositories;
using AuditLog.Core.Infra.Data.Repositories.Interfaces;
using AuditLog.Core.Services;
using AuditLog.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AuditLog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAuditService, AuditService>();

            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IOrderHandler, OrderHandler>();
            services.AddTransient<IAuditHandler, AuditHandler>();

            services.AddTransient<UserValidator, UserValidator>();
            services.AddTransient<OrderValidator, OrderValidator>();
            services.AddTransient<AuditValidator, AuditValidator>();

            services.AddTransient<ObjectFactories, ObjectFactories>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IAuditRepository, AuditRepository>();

            services.AddScoped(provider =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection");

                var opts = new DbContextOptionsBuilder<AuditLogContext>();
                opts.UseSqlServer(connString);

                return new AuditLogContext(opts.Options, new HttpContextAccessor(), mapper);
            });

            services.AddControllers();
            services.AddSwaggerDocumentation();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenJwt.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
