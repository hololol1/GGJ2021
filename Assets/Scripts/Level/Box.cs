using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Box : InteractableObject
{
    private bool m_scanned = false;

    public bool Scanned { get { return m_scanned; } }

    void Update()
    {
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        m_scanned = true;
    }

    public override void StopInteract(GameObject interactor)
    {
        base.StopInteract(interactor);
        m_scanned = false;
    }
}
