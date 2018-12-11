using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSpawner : MonoBehaviour
{
    // spawns a squad every x seconds
    public float intervallofSpawning;
    public GameObject squad;
    float nextSpawningTime;

    void Start()
    {
        nextSpawningTime = Time.time + Random.Range(0, intervallofSpawning);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawningTime)
        {
            Instantiate(squad,transform.position,transform.rotation);
            nextSpawningTime = Time.time + Random.Range(0, intervallofSpawning);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,4);
    }
}
