using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRandomEnemy()
    {
        if(enemyPool != null)
        {
            Instantiate(enemyPool[Random.Range(0, enemyPool.Length)], transform.position, transform.rotation);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.8f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
