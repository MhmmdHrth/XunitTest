﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.IntegrationTesting.Mocks;
using SampleApp.Services;
using System.Linq;

namespace SampleApp.IntegrationTesting
{
    public static class OverrideIWebHostBuilder
    {
        public static IWebHostBuilder addCustomeService(this IWebHostBuilder builder)
        {
            builder.ConfigureServices(x =>
            {
                x.AddSingleton<IAnimalService, AnimalServiceMock>();
            });

            return builder;
        }
    }
}
