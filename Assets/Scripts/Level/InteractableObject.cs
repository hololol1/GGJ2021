using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool interactable = true;

    private GameObject interactor;

    public virtual void Interact(GameObject interactor)
    {
        if (interactable)
        {
            this.interactor = interactor;
        }
        else
        {
            interactor = null;
            return;
        }
    }
}
