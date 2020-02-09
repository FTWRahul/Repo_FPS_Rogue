using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class OnHealthChangeEvent: UnityEvent <int> {}
    [System.Serializable] public class OnDamageEvent: UnityEvent <int> {}
    [System.Serializable] public class OnShootEvent: UnityEvent {}
}
