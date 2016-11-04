using System;

namespace Rx
{
    public interface IMessagePublisher
    {
        IObservable<TEvent> OfType<TEvent>();
        void Publish<TEvent>(TEvent sampleEvent);
    }
}