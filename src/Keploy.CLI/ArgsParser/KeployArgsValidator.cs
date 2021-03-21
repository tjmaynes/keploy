using System;
using System.IO;
using System.Collections.Generic;

namespace Keploy.CLI.ArgsParser
{
    public class KeployValidationError
    {
        public static KeployValidationError FileNotFound { get; } = new KeployValidationError(
            "File not found",
            "Please provide an existing, valid Keployrc file."
        );

        public string Description { get; }
        public string Usage { get; }

        private KeployValidationError(string description, string usage)
        {
            Description = description;
            Usage = usage;
        }

        public static IEnumerable<KeployValidationError> List()
        {
            return new[] { FileNotFound };
        }
    }

    public class KeployArgsValidator : IArgsValidator<KeployArgs, KeployValidationError>
    {
        private IArgumentParser<KeployArgs> argumentParser;

        public KeployArgsValidator(IArgumentParser<KeployArgs> argumentParser)
        {
            this.argumentParser = argumentParser;
        }

        public void Validate(
            string[] args,
            Action<KeployArgs> onSuccess,
            Action<KeployValidationError> onFailure
        )
        {
            this.argumentParser.Parse(args, keployArgs =>
            {
                if (File.Exists(keployArgs.FileLocation))
                {
                    onSuccess(keployArgs);
                }
                else
                {
                    onFailure(KeployValidationError.FileNotFound);
                }
            });
        }
    }
}