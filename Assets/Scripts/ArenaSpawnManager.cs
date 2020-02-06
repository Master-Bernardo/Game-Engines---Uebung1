using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSpawnManager : MonoBehaviour
{
    public bool active = true;
    Spawner[] spawners;
    delegate void SpawnRandomEnemies();
    SpawnRandomEnemies spawnRandomEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<Spawner>();
        foreach(Spawner spawner in spawners)
        {
            spawnRandomEnemies += spawner.SpawnRandomEnemy;
        }

        spawnRandomEnemies += ArenaGameController.Instance.IncrementWave;

        if(spawnRandomEnemies != null)
        {
            spawnRandomEnemies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(active && IsNoEnemy())
        {
            spawnRandomEnemies();
        }
    }

    bool IsNoEnemy()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if(enemy)
            {
                return false;
            }
        }

        return true;
    }

    public void Deactivate()
    {
        active = false;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }

    public void Activate()
    {
        active = true;
    }
}
