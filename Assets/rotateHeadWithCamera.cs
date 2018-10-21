using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateHeadWithCamera : MonoBehaviour
{
    public GameObject head;
    public GameObject p3Camera;
    public GameObject p1Camera;

    public GameObject oberarmL;
    public GameObject oberarmR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if (p1Camera.activeSelf)
         {
             head.transform.forward = p1Camera.transform.forward;
             oberarmL.transform.up = p1Camera.transform.forward;
             oberarmL.transform.forward = p1Camera.transform.up;
             oberarmR.transform.up = p1Camera.transform.forward;
             oberarmR.transform.forward = p1Camera.transform.up;
         }
         else
         {
             head.transform.forward = p3Camera.transform.forward;
             oberarmL.transform.up = p3Camera.transform.forward;
             oberarmL.transform.forward = p1Camera.transform.up;
             oberarmR.transform.up = p3Camera.transform.forward;
             oberarmR.transform.forward = p1Camera.transform.up;
         }*/
        if (p1Camera.activeSelf)
        {
            head.transform.forward = p1Camera.transform.forward;
            oberarmL.transform.rotation= p1Camera.transform.eulerAngles.x;
        }
        else
        {
            head.transform.forward = p3Camera.transform.forward;
        }
    }
}
