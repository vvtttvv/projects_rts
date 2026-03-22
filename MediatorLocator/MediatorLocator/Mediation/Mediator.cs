using MediatorLocator.Command;
using MediatorLocator.Exceptions;
using MediatorLocator.Handler;
namespace MediatorLocator.Mediation;

using System;
using System.Collections.Generic;

public class Mediator : IMediator
{
    private readonly Dictionary<Type, Delegate> _dict = new();

    public void Register<TCommand, TParam, TResult>(IHandler<TCommand, TParam, TResult> handler) where TCommand : ICommand<TParam, TResult>
    {
        if (_dict.ContainsKey(typeof(TCommand))) throw new HandlerAlreadyRegisteredException();

        Func<TParam, TResult> fnc = (param) => handler.Handle(param);
        _dict.Add(typeof(TCommand), fnc);
        Console.WriteLine("Registered successfully!");
    }

    public TResult Send<TParam, TResult>(ICommand<TParam, TResult> command, TParam param)
    {
        if (!_dict.ContainsKey(command.GetType())) throw new HandlerNotRegisteredException();

        var fnc = _dict[command.GetType()] as Func<TParam, TResult>;
        if (fnc is null) throw new HandlerNotRegisteredException();

        return fnc.Invoke(param);
    }
    
}