using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wakening
{
    public class DebugResponse : Response
    {
        public string message;
        public override void Execute(TriggerComponent t)
        {
            base.Execute(t);
            Debug.Log(message);
        }
    }
}
