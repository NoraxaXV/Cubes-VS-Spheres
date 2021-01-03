using System;

namespace Wakening
{
    public class Subscription : IDisposable
    {
        private Action _unsubscribeAction;

        public Subscription(Action unsubscribeAction)
        {
            _unsubscribeAction = unsubscribeAction;
        }

        public void Dispose()
        {
            _unsubscribeAction.Invoke();
        }
    }
}
