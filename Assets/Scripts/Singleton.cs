using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wakening
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Multiple instances of " + name + " detected!");
            }
            Instance = (T)this;
        }
    }
}