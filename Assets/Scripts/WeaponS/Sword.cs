using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    bool cutting; //is the sword currently moving and able to cut something?
    public float cuttingTime = 2; //how long does it cut? - depends on the animation

    public bool rightCut = true; // if this is rue, we cut from the right, if false we cut from the left, we always alternate, the first being right
    public float cutAlternatingMaxIntervall = 2; // max time before we cut from the right again 


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
        StopCoroutine("StopAlternating");
        cutting = true;
        StartCoroutine("StopCutting");
        StartCoroutine("StopAlternating");
        rightCut = !rightCut;
    }

    IEnumerator StopCutting()
    {
        yield return new WaitForSeconds(cuttingTime);
        cutting = false;
    }

    IEnumerator StopAlternating()
    {
        yield return new WaitForSeconds(cutAlternatingMaxIntervall);
        rightCut = true;
    }
}
