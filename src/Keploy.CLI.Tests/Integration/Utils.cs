using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Keploy.CLI.Tests.Integration
{
    class IntegrationUtils
    {
        public static string keployrcFilePath = Path.Combine(Path.GetTempPath(), ".keployrc.json");
        public static string outputFilePath = Path.Combine(Path.GetTempPath(), "output.yaml");


        public static async Task<Process> RunKeployCommand()
        {
            return await RunKeployCommand(new string[] {
                $"--file={keployrcFilePath}"
             });
        }

        public static async Task<Process> RunKeployCommand(string[] args)
        {
            var keployPath = Path.GetFullPath("../../../../Keploy.CLI/bin/Debug/net5.0/keploy");
            return await RunCommand(keployPath, args);
        }

        public static async Task<Process> RunCommand(string keployPath, string[] args)
        {
            if (!File.Exists(keployPath))
                throw new FileNotFoundException(keployPath);

            Console.WriteLine(keployPath);

            var process = new Process();

            process.StartInfo = new ProcessStartInfo
            {
                FileName = keployPath,
                Arguments = String.Join(" ", args),
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.Start();

            await process.WaitForExitAsync();

            return process;
        }

        public static async Task CreateKeployJsonFile(KeployArgs args)
        {
            DeleteFileIfExists(keployrcFilePath);

            using FileStream createStream = File.Create(keployrcFilePath);

            var keployJsonFileContent = JsonSerializer.Serialize(args);
            await JsonSerializer.SerializeAsync(createStream, keployJsonFileContent);
        }

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        public static async Task<string> GetFileContent(string filePath)
        {
            string fileContent = "";
            try
            {
                fileContent = await File.ReadAllTextAsync(filePath);
            }
            catch (Exception) { }

            return fileContent;
        }

        public static void Cleanup()
        {
            DeleteFileIfExists(keployrcFilePath);
            DeleteFileIfExists(outputFilePath);
        }
    }
}