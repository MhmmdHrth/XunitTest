using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Controllers;
using SampleApp.IntegrationTesting.Mocks;
using SampleApp.Services;
using System.IO;

namespace SampleApp.IntegrationTesting.Extensions
{
    public static class OverrideIWebHostBuilder
    {
        private const string directoryName = "test_images";


        public static (IWebHostBuilder,string) addCustomeService(this IWebHostBuilder builder)
        {
            string path = "";
            builder.ConfigureServices(x =>
            {
                x.AddSingleton<IAnimalService, AnimalServiceMock>();

                x.Configure<FileSettings>(fs =>
                {
                    fs.Path = directoryName;
                });

                using (ServiceProvider serviceProvier = x.BuildServiceProvider())
                {
                    IWebHostEnvironment env = serviceProvier.CreateScope().ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                    path = Path.Combine(env.WebRootPath, directoryName);
                }
            });

            return (builder, path);
        }
    }
}
