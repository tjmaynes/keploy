using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

class KeployArgs {
    public KeployArgs(string name, string image, int port, int numberOfInstances) {
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

namespace Keploy.CLI.Tests.Integration
{
    public class TestKeploy
    {
        [Fact]
        public async void test_whenNoFileArgumentIsGiven_shouldUseDefaultFile()
        {
            await IntegrationUtils.CreateKeployJsonFile(new KeployArgs(
                name: "my-service",
                image: "nginx:alpine",
                port: 8080,
                numberOfInstances: 3
            ));

            var keployProcess = await IntegrationUtils.RunKeployCommand();
            Assert.Equal(0, keployProcess.ExitCode);

            string expectedOutput = @"
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-service-deployment
  labels:
    app: my-service
spec:
  replicas: 3
  selector:
    matchLabels:
      app: my-service
  template:
    metadata:
      labels:
        app: my-service
    spec:
      containers:
      - image: nginx:alpine
        ports:
        - containerPort: 8080
            ";

            string actualOutput = await IntegrationUtils.GetFileContent(IntegrationUtils.outputFilePath);
            Assert.Equal(expectedOutput, actualOutput);

            IntegrationUtils.Cleanup();
        }
    }
}
