using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakening.Messages;

namespace Wakening
{
    public class TriggerComponent : MonoBehaviour
    {
        public string title;
        public TriggerSystem[] systems;
        private void Start()
        {
            foreach(TriggerSystem sys in systems)
            {
                sys.trigger = this;
                sys.Listen();
            }
            Debug.Log("Starting Trigger!");
        }
    }

    [Serializable]
    public class TriggerSystem {

        [HideInInspector] public TriggerComponent trigger;
        public Condition condition;
        public Response[] responses;

        public void Listen()
        {
            Singleton<EventManager>.Instance.Subscribe<Condition, ValueChanged<bool>>((c, value) => {
                foreach (Response response in responses) response.Execute(trigger);
            }, null, condition);
        }
    }

    
}
