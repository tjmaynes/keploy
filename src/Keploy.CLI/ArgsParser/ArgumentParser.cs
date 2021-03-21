using System;
using CommandLine;

namespace Keploy.CLI.ArgsParser
{
    public class ArgumentParser<T> : IArgumentParser<T>
    {
        private Parser parser;

        public ArgumentParser(Parser parser)
        {
            this.parser = parser;
        }

        public void Parse(string[] args, Action<T> onParse)
        {
            this.parser
                .ParseArguments<T>(args)
                .WithParsed<T>(onParse);
        }
    }
}