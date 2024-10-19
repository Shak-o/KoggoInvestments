﻿using KoggoInvestments.Application.ApiInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase.HttpClientFactory;

namespace KoggoInvestments.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
        
        var key = builder.Configuration["ApiKey"];
        
        builder.Services.AddHttpClient("AuthManagerClient",
            static client =>
            {
                client.BaseAddress = new("https+http://FinnApi");
            }).UseWithRestEaseClient(new UseWithRestEaseClientOptions<IFinnHubApi>() {InstanceConfigurer = x => x.ApiKey = key!});

        return builder;
    }
}