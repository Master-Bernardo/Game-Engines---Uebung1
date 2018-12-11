using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ammoAmount;
    public AmmoType ammoType;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.tag == "Player")
        {
            WeaponSystem.Instance.AddAmmo(ammoType, ammoAmount);
                
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(60);
        Destroy();
    }
}
