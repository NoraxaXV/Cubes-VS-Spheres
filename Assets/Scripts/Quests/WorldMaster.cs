using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wakening.Messages
{
    public class WorldMaster : MonoBehaviour
    {
        public GameObject EVIL_Prefab;
        // Start is called before the first frame update
        void Start()
        {
            Singleton<EventManager>.Instance.Subscribe<TriggerComponent, SpawnMonsterResponse>((t, s) => { Instantiate(EVIL_Prefab, transform); });
        }


    }
}
