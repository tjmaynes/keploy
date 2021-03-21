using System;
using System.IO;
using Keploy.CLI.ArgsParser;

namespace Keploy.CLI.Builder
{
    public interface IAppRunnable
    {
        void Run(string[] args);
    }

    public class Keploy : IAppRunnable
    {
        private readonly IArgsValidator<KeployArgs, KeployValidationError> argsValidator;

        public Keploy(IArgsValidator<KeployArgs, KeployValidationError> argsValidator)
        {
            this.argsValidator = argsValidator;
        }

        public void Run(string[] args)
        {
            this.argsValidator.Validate(
                args: args,
                onSuccess: (keployArgs) => Console.WriteLine(keployArgs.ToDTO()),
                onFailure: HandleValidationError
            );
        }

        private void HandleValidationError(KeployValidationError error)
        {
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine(error.Description);
            errorWriter.WriteLine(error.Usage);

            Environment.Exit(1);
        }
    }
}