using System;

namespace BestServices.Core.Messenger
{
    /// <summary>
    /// Определяет базовые методы для реализации передачи сообщений
    /// </summary>
    internal interface IMessenger
    {
        void Send<TMessage>(TMessage message);
        void Subscribe<TMessage>(object subscriber, Action<object> action);
        void Unsubscribe<TMessage>(object subscriber);
    }
}