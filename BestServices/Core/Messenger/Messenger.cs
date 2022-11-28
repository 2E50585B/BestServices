using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BestServices.Core.Messenger
{
    /// <summary>
    /// Базовый класс, реализующий методы для <b>потокобезопасной</b> передачи сообщений
    /// </summary>
    internal class Messenger : IMessenger
    {
        /// <summary>
        /// Потокобезопасная коллекция подписок
        /// </summary>
        private ConcurrentDictionary<Type, SynchronizedCollection<Subscription>> Subscriptions =
            new ConcurrentDictionary<Type, SynchronizedCollection<Subscription>>();

        /// <summary>
        /// Потокобезопасная коллекция состояний сообщений
        /// </summary>
        private ConcurrentDictionary<Type, object> CurrentState = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Отправляет сообщение всем подписчикам
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения</typeparam>
        /// <param name="message">Объект сообщения</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Send<TMessage>(TMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());
            }

            CurrentState.AddOrUpdate(typeof(TMessage), (t) => message, (t, o) => message);

            foreach (Subscription subscription in Subscriptions[typeof(TMessage)])
            {
                subscription.Action(message);
            }
        }

        /// <summary>
        /// Создаёт нового подписчика на определённое сообщение с действием, которое нужно выполнить при получении сообщения
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения</typeparam>
        /// <param name="subscriber">Подписчик</param>
        /// <param name="action">Действие на получение сообщения</param>
        public void Subscribe<TMessage>(object subscriber, Action<object> action)
        {
            if (!Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());
            }

            Subscription newSubscriber = new Subscription(subscriber, action);

            Subscriptions[typeof(TMessage)].Add(newSubscriber);

            if (CurrentState.ContainsKey(typeof(TMessage)))
            {
                newSubscriber.Action(CurrentState[typeof(TMessage)]);
            }
        }

        /// <summary>
        /// Отписывает подписчика от сообщения
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения</typeparam>
        /// <param name="subscriber">Подписчик</param>
        public void Unsubscribe<TMessage>(object subscriber)
        {
            if (Subscriptions.ContainsKey(typeof(TMessage)))
            {
                Subscription subscription = Subscriptions[typeof(TMessage)].FirstOrDefault(s => s.Subscriber == subscriber);
                if (subscription != null)
                {
                    Subscriptions[typeof(TMessage)].Remove(subscription);
                }
            }
        }

        /// <summary>
        /// Класс подписки
        /// </summary>
        private protected class Subscription
        {
            public object Subscriber { get; private set; }
            public Action<object> Action { get; private set; }

            /// <summary>
            /// Создаёт новый экземпляр подписки с подписчиком и действием
            /// </summary>
            /// <param name="subscriber">Подписчик</param>
            /// <param name="action">Действие</param>
            public Subscription(object subscriber, Action<object> action)
            {
                Subscriber = subscriber;
                Action = action;
            }
        }
    }
}