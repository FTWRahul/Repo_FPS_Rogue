using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable] public class OnHealthChangeEvent: UnityEvent <int> {}
    [System.Serializable] public class OnDamageEvent: UnityEvent <int,int> {}
    [System.Serializable] public class OnDamageUpdateEvent: UnityEvent <float> {}
    [System.Serializable] public class OnShootEvent: UnityEvent {}
    
    [System.Serializable] public class OnReloadStartEvent: UnityEvent {}
    
    [System.Serializable] public class OnDeathEvent: UnityEvent {}
    [System.Serializable] public class OnLastDamageChangedEvent: UnityEvent <Damage>{}
    [System.Serializable] public class OnEnemyStateUpdateEvent: UnityEvent <EnemyState>{}
    
    [System.Serializable] public class OnEnemyEnd: UnityEvent {}
    
}
