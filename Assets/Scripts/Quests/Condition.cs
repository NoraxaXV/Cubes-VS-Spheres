using System;
using System.Collections.Generic;
using Wakening.Messages;
using UnityEngine;

namespace Wakening
{
    public class Condition : MonoBehaviour, IDisposable
    {

        [SerializeField] bool _value = false;
        List<Subscription> _subscriptions;

        public bool Value
        {
            get => _value;
            protected set
            {
                if (value != _value)
                    Singleton<EventManager>.Instance.Publish(this, new ValueChanged<bool>(_value, value));
                _value = value;
            }
        }
        public virtual void Listen() { }
        public virtual void CheckEvents() { }

        private void Start()
        {
            _subscriptions = new List<Subscription>();
            Listen();
        }
        private void Update()
        {
            CheckEvents();
        }

        protected void OnApplicationQuit()
        {
            // Dispose();
        }


        protected Subscription Subscribe<TSource, TArgs>(Action<TSource, TArgs> action, Func<TSource, TArgs, bool> filter = null, params TSource[] sources)
        {
            var sub = Singleton<EventManager>.Instance.Subscribe(action, filter, sources);
            _subscriptions.Add(sub);
            return sub;
        }
        public void Dispose() {
            foreach (Subscription s in _subscriptions) s.Dispose();
            _subscriptions.Clear();
        }

    }
}
