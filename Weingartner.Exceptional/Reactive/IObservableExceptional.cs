﻿using System;

namespace Weingartner.Reactive
{
    public interface IObservableExceptional<out T>
    {
        IDisposable Subscribe(IObserverExceptional<T> observer);

        IObservable<IExceptional<T>> Observable { get; }
    }
}
