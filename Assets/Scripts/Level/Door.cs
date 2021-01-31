using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractableObject
{
    private Animator anim;
    public bool open = false;
    public Box[] boxes;
    private AudioSource[] audioPlayers;
    public AudioClip[] openCloseSounds;
    private bool playOnce;

   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioPlayers = GetComponents<AudioSource>();
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
        bool prevValue = open;
        open = true;
        anim.SetBool("open", open);
        if (prevValue != open)
        {
            audioPlayers[0].Stop();
            audioPlayers[0].PlayOneShot(openCloseSounds[0]);
            audioPlayers[1].Play();
        }
    }

    private void Close()
    {
        bool prevValue = open;
        open = false;
        anim.SetBool("open", open);
        if (prevValue != open) 
        {
            audioPlayers[0].Stop();
            audioPlayers[0].PlayOneShot(openCloseSounds[1]); 
            
        }
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
