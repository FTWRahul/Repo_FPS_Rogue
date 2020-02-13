using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class OnHealthChangeEvent: UnityEvent <int> {}
    [System.Serializable] public class OnDamageEvent: UnityEvent <int> {}
    [System.Serializable] public class OnShootEvent: UnityEvent {}
    [System.Serializable] public class OnLastDamageChangedEvent: UnityEvent {}
    [System.Serializable] public class OnEnemyStateUpdateEvent: UnityEvent <EnemyState>{}
}
