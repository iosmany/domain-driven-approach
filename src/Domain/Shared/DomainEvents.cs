﻿using System.Reflection;

namespace App.Domain.Shared;
public static class DomainEvents
{
    private static List<Type>? _handlers;

    public static void Init()
    {
        _handlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
            .ToList();
    }

    public static void Dispatch(IDomainEvent domainEvent)
    {
        if (_handlers == null)
            throw new InvalidOperationException("DomainEvents.Init() must be called before dispatching events.");

        foreach (Type handlerType in _handlers)
        {
            bool canHandleEvent = handlerType.GetInterfaces()
                .Any(x => x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IHandler<>)
                    && x.GenericTypeArguments[0] == domainEvent.GetType());

            if (canHandleEvent)
            {
                dynamic handler = Activator.CreateInstance(handlerType);
                handler.Handle((dynamic)domainEvent);
            }
        }
    }
}
