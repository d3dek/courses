using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CSharpFunctionalExtensions;
using Logic.Students;
using Logic.Utils;

namespace Logic.Decorators
{
    public sealed class DatabaseRetryDecorator<TCommand>: ICommandHandler<TCommand> where TCommand: ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly Config _config;

        public DatabaseRetryDecorator(ICommandHandler<TCommand> handler, Config config)
        {
            _handler = handler;
            _config = config;
        }

        public Result Handle(TCommand command)
        {
            for (int i = 0; i < _config.NoOfDatabaseRetry; i++)
            {
                try
                {
                    Result result = _handler.Handle(command);
                    return result;
                }
                catch (Exception e)
                {
                    if (i >= _config.NoOfDatabaseRetry || !IsDatabaseException(e))
                        throw;
                }
            }

            throw new InvalidOperationException("Should never ne here.");
        }

        public bool IsDatabaseException(Exception exception)
        {
            string message = exception.InnerException?.Message;

            if (message == null)
                return false;

            return message.Contains("This connection is broken") || message.Contains("error occurred while esta");
        }
    }
}
