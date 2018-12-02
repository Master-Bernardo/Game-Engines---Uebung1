using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    bool cutting; //is the sword currently moving and able to cut something?
    public float cuttingTime = 2; //how long does it cut? - depends on the animation


    private void OnTriggerEnter(Collider collider)
    {
        if (cutting)
        {
            if(collider.gameObject.tag != "Player")
            {
                Debug.Log(collider.gameObject);
            }
            if(collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    public void StartCutting()
    {
        cutting = true;
        StartCoroutine("StopCutting");
    }

    IEnumerator StopCutting()
    {
        yield return new WaitForSeconds(cuttingTime);
        cutting = false;
    }
}
