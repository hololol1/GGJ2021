using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractableObject
{
    private Animator anim;
    public bool open = false;

    public Box[] boxes;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < boxes.Length; ++i)
        {
            if (!boxes[i].Scanned)
            {
                Close();
                return;
            }
        }

        Open();
    }

    private void Open()
    {
        open = true;
        anim.SetBool("open", open);
    }

    private void Close()
    {
        open = false;
        anim.SetBool("open", open);
    }

    public override void Interact(GameObject interactor)
    {
        if (interactable)
        {
            base.Interact(interactor);
            Open();
        }
    }
}
