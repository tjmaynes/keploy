using System;

namespace Keploy.CLI.ArgsParser
{
    public interface IArgsValidator<T, E>
    {
        void Validate(string[] args, Action<T> onSuccess, Action<E> onFailure);
    }
}