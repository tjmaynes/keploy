using Microsoft.Extensions.DependencyInjection;
using Keploy.CLI.ArgsParser;
using CommandLine;

namespace Keploy.CLI.Builder {
    public class KeployBuilder {
        public static Keploy Build() {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<Keploy>();
        }
        
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<Parser>(Parser.Default);
            services.AddTransient<IArgumentParser<KeployArgs>, ArgumentParser<KeployArgs>>();
            services.AddTransient<IArgsValidator<KeployArgs, KeployValidationError>, KeployArgsValidator>();

            services.AddTransient<Keploy>();

            return services;
        }
    }
}