using System;
using CSharpFunctionalExtensions;
using Logic.Students;
using Newtonsoft.Json;

namespace Logic.Decorators
{
    public sealed class AuditLoginDecorator<TCommand>: ICommandHandler<TCommand> where TCommand: ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public AuditLoginDecorator(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public Result Handle(TCommand command)
        {
            string serializeObject = JsonConvert.SerializeObject(command);
            Console.WriteLine($"Command of type {command.GetType().Name}: {serializeObject}");
            return _handler.Handle(command);
        }
    }
}
