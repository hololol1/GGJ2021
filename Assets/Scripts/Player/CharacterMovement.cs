using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public LayerMask groundLayer;
    public Transform groundPosition;

    public ParticleSystem jumpingParticles;

    public float moveSpeed = 5.0f;
    public float jumpForce = 200.0f;
    private bool grounded = false;

    private AudioSource audioPlayer;
    public AudioClip[] scannerJump;
    public AudioClip scannerGrunt;
    public AudioClip scannerBeamPrepare;
    public AudioClip[] scannerPush;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        jumpingParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
   

        //grounded = Physics.OverlapSphere(groundPosition.position, 0.25f, groundLayer).Length > 0;
        grounded = Physics.OverlapBox(groundPosition.position, new Vector3(1.0f, 0.15f, 1.0f), Quaternion.identity, groundLayer).Length > 0;
        //Debug.DrawLine(transform.position, transform.position + new Vector3(0.0f, -0.25f, 0.0f), Color.red);
        Vector3 direction = Vector3.zero;
        float d = Input.GetAxisRaw("Horizontal");
  

        if (d < 0.0f)
        {
            direction = Vector3.left;
        }

        if (d > 0.0f)
        {
            direction = Vector3.right;
        }

        RaycastHit hit;
        if (rb.SweepTest(direction, out hit, 0.2f))
        {
			if (hit.collider.gameObject.GetComponent<MovableObject>() != null || hit.collider.gameObject.GetComponent<LevelTrigger>() != null || hit.transform.position.y < groundPosition.position.y)
			{
				rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, 0);
				//transform.position = transform.position + (direction * moveSpeed * Time.deltaTime);
			}
		}
        else
        {
			rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, 0);
			//transform.position = transform.position + (direction * moveSpeed * Time.deltaTime);
		}

		if (Input.GetButtonDown("Jump") && grounded)
        {
            audioPlayer.clip = scannerJump[Random.Range(0, scannerJump.Length)];
            audioPlayer.Play();
            rb.AddForce(Vector3.up * jumpForce);
            jumpingParticles.Play();
        }

    }
}
