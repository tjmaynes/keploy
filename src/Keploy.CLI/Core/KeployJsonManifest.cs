namespace Keploy.CLI.Core
{
    public class KeployJsonManifest
    {
        public KeployJsonManifest(string name, string image, int port, int numberOfInstances)
        {
            Name = name;
            Image = image;
            Port = port;
            Instances = numberOfInstances;
        }

        public string Name { get; }
        public string Image { get; }
        public int Port { get; }
        public int Instances { get; }
    }
}