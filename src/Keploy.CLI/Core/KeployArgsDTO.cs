namespace Keploy.CLI.Core {
    public class KeployArgsDTO {
        public KeployArgsDTO(string fileLocation) {
            FileLocation = fileLocation;
        }

        public string FileLocation { get; }
    }
}