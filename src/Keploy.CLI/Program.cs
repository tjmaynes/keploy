using Keploy.CLI.Builder;

namespace Keploy.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            KeployBuilder.Build().Run(args);
        }
    }
}
