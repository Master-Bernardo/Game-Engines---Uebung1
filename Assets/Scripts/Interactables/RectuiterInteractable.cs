using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectuiterInteractable : Interactable
{
    public GameObject friendlyFollowerPrefab;

    protected override void ExecuteInteractAction()
    {
        Instantiate(friendlyFollowerPrefab, transform.position, transform.rotation);
    }
}
