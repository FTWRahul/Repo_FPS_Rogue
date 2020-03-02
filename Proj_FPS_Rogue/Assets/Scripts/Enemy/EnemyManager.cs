using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Dictionary<GameObject, int> enemiesToSpawn;
    /*public Dictionary<GameObject, int> enemySpawned;*/
    public float minDelay;
    public float maxDelay;
    
    public int minRange;
    public int maxRange;
    
    private Transform _target;

    public Events.OnEnemyEnd enemyEnd;

    private void Awake()
    {
        StartCoroutine(DelayCounter());
    }

    private void SpawnEnemy()
    {
        Instantiate(GetEnemyToSpawn(), GetPositionToSpawn(), Quaternion.identity);
    }

    IEnumerator DelayCounter()
    {
        while (enemiesToSpawn.Count > 0)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            SpawnEnemy();
        }
        
        enemyEnd?.Invoke();
    }

    private GameObject GetEnemyToSpawn()
    {
        int index = Random.Range(0, enemiesToSpawn.Count);
        KeyValuePair<GameObject, int> newEnemy = enemiesToSpawn.ElementAt(index);
        
        if (newEnemy.Value == 1)
        {
            enemiesToSpawn.Remove(newEnemy.Key);
        }
        else
        {
            enemiesToSpawn[newEnemy.Key]--;
        }
        
        return newEnemy.Key;
    }

    private Vector3 GetPositionToSpawn()
    {
        //TODO around the player but need to know if this is flying or ground one
        
        
        return new Vector3();
    }
}
