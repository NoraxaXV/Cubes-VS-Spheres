using System;


namespace Wakening
{
    public class SubscribeEvent
    {
        Action action;
        public Action Event { get => action; }

        public Subscription Subscribe(Action action)
        {
            this.action += action;
            return new Subscription(() => { this.action -= action; });
        }
    }

    public class SubscribeEvent<T>
    {
        Action<T> action;
        public Action<T> Event { get => action; }

        public Subscription Subscribe(Action<T> action)
        {
            this.action += action;
            return new Subscription(() => { this.action -= action; });
        }
    }
}

