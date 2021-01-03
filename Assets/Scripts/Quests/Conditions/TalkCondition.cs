using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakening.Messages;


namespace Wakening
{
    public class TalkCondition : Condition
    {

        public string npcName;
        public override void Listen()
        {
            Singleton<EventManager>.Instance.Subscribe<Interactable, InteractedEvent>((i, e) => { Value = true; }, (i, e) => i.tagName == npcName);
        }
    }
}
