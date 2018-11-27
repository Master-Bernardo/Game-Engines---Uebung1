using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    public float startSpeed;

    void Start()
    {
        rb.velocity = transform.forward * startSpeed;
    }

    void Update()
    {
        
    }
}
