using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wakening.Messages
{
    public class ValueChanged<T> {
        public T oldVal;
        public T newVal;

        public ValueChanged(T oldVal, T newVal)
        {
            this.oldVal = oldVal;
            this.newVal = newVal;
        }
    }

    public class EventManager : Singleton<EventManager>
    {

        Action<object, object> eventActions;

        public Subscription Subscribe<TSource, TArgs>(Action<TSource, TArgs> action, Func<TSource, TArgs, bool> filter = null, params TSource[] sources)
        {
            Action<object, object> subscribeAction = (object source, object args) =>
            {

                if (source is TSource s &&
                    args is TArgs a &&
                    (filter == null || filter(s, a)))
                {
                    
                    if (Array.IndexOf(sources, s) != -1)
                    {
                        action(s, a);
                    }
                }
            };

            eventActions += subscribeAction;
            return new Subscription(() => { eventActions -= subscribeAction; });
        }

        public void Publish<TSource, TArgs>(TSource source, TArgs args) => eventActions(source, args);

    }
}