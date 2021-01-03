using Wakening.Messages;
using UnityEngine;

namespace Wakening
{
    abstract public class Response : MonoBehaviour
    {
        public virtual void Execute(TriggerComponent t) => Singleton<EventManager>.Instance.Publish(t, this);

        public bool singleShot = true;
        public float reloadTime = 0;
    }

    
}
