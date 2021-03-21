using CommandLine;
using Keploy.CLI.Core;

namespace Keploy.CLI.ArgsParser
{
    public class KeployArgs
    {
        [Option('f', "file-location", Required = false, HelpText = "Set file location to use for generating Kubernetes config.")]
        public string FileLocation { get; set; }

        public KeployArgsDTO ToDTO() {
            return new KeployArgsDTO(
                fileLocation: FileLocation
            );
        } 
    }
}