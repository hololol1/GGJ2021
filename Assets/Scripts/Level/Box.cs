using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Box : InteractableObject
{
    private bool m_scanned = false;
    private float unscanTime = 0.25f;
    private float unscanTimer = 0.0f;

    public bool Scanned { get { return m_scanned; } }

    void Update()
    {
        unscanTimer -= Time.deltaTime;

        if (unscanTimer <= 0.0f)
        {
            m_scanned = false;
        }
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        unscanTimer = unscanTime;
        m_scanned = true;
    }

    public override void StopInteract(GameObject interactor)
    {
        base.StopInteract(interactor);
        m_scanned = false;
    }
}
