﻿using Application.Common.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Notification.Web.SignalR;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.ZeebeServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    configure =>
                    {
                        configure.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        configure.EnableRetryOnFailure();
                    }));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddSingleton<IServerEventService, ServerEventService>();
            services.AddSingleton<IClientEventService, ClientEventService>();
            services.AddSingleton<IZeebeService, ZeebeService>();
            services.AddSingleton<IContractApprovalService, ContractApprovalService>();

            services.AddSignalR();

            builder.ConfigureLog();

            return services;
        }
    }
}