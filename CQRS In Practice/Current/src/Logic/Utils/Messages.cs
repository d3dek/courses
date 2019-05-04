using System;
using CSharpFunctionalExtensions;
using Logic.Students;

namespace Logic.Utils
{
    public sealed class Messages
    {
        private readonly IServiceProvider serviceProvider;

        public Messages(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Result Dispatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = this.serviceProvider.GetService(handlerType);
            Result result = handler.Handle((dynamic)command);

            return result;
        }

        public T Dispatch<T>(IQuery<T> query)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(T) };
            Type handlerType = type.MakeGenericType(typeArgs);
            dynamic handler = this.serviceProvider.GetService(handlerType);
            T result = handler.Handle((dynamic)query);

            return result;
        }
    }
}
