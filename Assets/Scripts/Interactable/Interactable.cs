using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakening;
using Wakening.Messages;

public class InteractedEvent { }
public class Interactable : MonoBehaviour
{
    public string tagName;
    public virtual void Interact() => Singleton<EventManager>.Instance.Publish(this, new InteractedEvent());
}
