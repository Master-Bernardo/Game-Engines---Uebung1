using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] unitsToSpawn;

    void Start()
    {
        foreach(GameObject unit in unitsToSpawn)
        {
            unit.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (GameObject unit in unitsToSpawn)
            {
                unit.SetActive(true);
            }
        }
    }


}
