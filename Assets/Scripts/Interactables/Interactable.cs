using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float cost; //kosten für diese Aktion in Score Punkten
    public GameObject interactionText;

    private void Start()
    {
        ArenaGameController.Instance.AddInteractable(this);
        HideUI();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (interactionText.activeSelf)
        {
            interactionText.transform.forward = (Camera.main.transform.position - transform.position);
        }
    }


    public void ShowUI()
    {
        interactionText.SetActive(true);
    }

    public void HideUI()
    {
        interactionText.SetActive(false);
    }

     public void Interact()
    {
        if(ArenaGameController.Instance.score >= cost)
        {
            ArenaGameController.Instance.score -= cost;
            ExecuteInteractAction();
        }
        
    }

    protected virtual void ExecuteInteractAction()
    {
        Debug.Log("you interacted");
    }
}
