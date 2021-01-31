using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : InteractableObject
{
    private Rigidbody rb;
    private AudioSource[] audioPlayer;
    public AudioClip[] objectHitSounds;
    public float audioInitTime = 0.5f;
    private float audioInitTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponents<AudioSource>();
        audioInitTimer = audioInitTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        bool readytoPlayAudio = audioInitTimer <= 0.0f;
        if (readytoPlayAudio)
        {
            audioPlayer[0].clip = objectHitSounds[Random.Range(0, objectHitSounds.Length - 1)];
            audioPlayer[0].Play();
        }
    }
    private void Update()
    {
        audioInitTimer -= Time.deltaTime;
        bool readytoPlayAudio = audioInitTimer <= 0.0f;
        if (readytoPlayAudio)
        {
            if (rb.velocity.x > 0.01f || rb.velocity.x < -0.01f)
            {
                if (!audioPlayer[1].isPlaying)
                {
                    audioPlayer[1].Play();
                    audioPlayer[1].time = Random.Range(0, audioPlayer[1].clip.length);
                }
            }
            else
            {
                audioPlayer[1].Stop();
            }
        }
    }
}
