using System;

namespace Keploy.CLI.ArgsParser
{
    public interface IArgumentParser<T>
    {
        public void Parse(string[] args, Action<T> onParse);
    }
}