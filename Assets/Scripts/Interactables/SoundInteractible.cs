using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInteractible : Interactable
{
    public AudioSource audioSource;
    public Animator animator;

    protected override void ExecuteInteractAction()
    {
        audioSource.Play();
        animator.SetTrigger("play");
    }
}
