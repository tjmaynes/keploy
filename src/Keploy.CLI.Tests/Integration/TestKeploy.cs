using Xunit;
using Keploy.CLI.Core;

namespace Keploy.CLI.Tests.Integration
{
    public class TestKeploy
    {
        [Fact]
        public async void test_whenGivenAValidKeployJsonFile_shouldCreateSingleKubernetesConfigFile()
        {
            await IntegrationUtils.CreateKeployJsonFile(keployJsonManifest);

            var keployProcess = await IntegrationUtils.RunKeployCommand(new string[] {
                $"--file={IntegrationUtils.keployrcFilePath}"
             });
            Assert.Equal(0, keployProcess.ExitCode);

            string actualOutput = await IntegrationUtils.GetFileContent(IntegrationUtils.outputFilePath);
            Assert.Equal(defaultExpectedKubernetesDeploymentConfig, actualOutput);

            IntegrationUtils.Cleanup();
        }

        [Fact]
        public async void test_whenNotGivenAKeployJsonFile_andDefaultFileExists_shouldCreateSingleKubernetesConfigFile()
        {
            await IntegrationUtils.CreateKeployJsonFile(keployJsonManifest);

            var keployProcess = await IntegrationUtils.RunKeployCommand(new string[] {
              ""
            });
            Assert.Equal(0, keployProcess.ExitCode);

            string actualOutput = await IntegrationUtils.GetFileContent(IntegrationUtils.outputFilePath);
            Assert.Equal(defaultExpectedKubernetesDeploymentConfig, actualOutput);

            IntegrationUtils.Cleanup();
        }

        [Fact]
        public async void test_whenNotGivenAKeployJsonFile_andDefaultFileDoesNotExist_shouldExitWithFileNotFoundMessage()
        {
            IntegrationUtils.DeleteKeployJsonFileIfExists();

            var keployProcess = await IntegrationUtils.RunKeployCommand(new string[] {
                ""
            });
            Assert.Equal(1, keployProcess.ExitCode);
            Assert.Equal("File not found\nPlease provide an existing, valid Keployrc file.\n", keployProcess.StandardError.ReadToEnd());
        }

        private KeployJsonManifest keployJsonManifest = new KeployJsonManifest(
          name: "my-service",
          image: "nginx:alpine",
          port: 8080,
          numberOfInstances: 3
        );

        private string defaultExpectedKubernetesDeploymentConfig = @"
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
    }
}
