using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpHeight = 5f;
    public float gravity = -50f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public CharacterController controller;
    public AudioSource footsteps;
    public AudioSource jumpSound;

    bool gunEquipped;
    public GameObject gun;
    public GameObject text;

    Vector3 velocity;
    // Update is called once per frame
    void Start() {
        gunEquipped = true;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Checks if grounded, if yes: stops gravity
        if (isGrounded && velocity.y < 0) {
            if (jump == 0) {
                velocity.y = -2f;
            }
            else {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                jumpSound.Play();

            }
            
        } 

        //Moves up, down, left, right by move speed
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Applies gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if ((x != 0 || z!=0) && isGrounded){
            footsteps.enabled = true;
        }
        else {
            footsteps.enabled = false;
        }

        if (transform.position.y <= -100) {
            transform.position = new Vector3(0f,5f,0f);
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            if (gunEquipped) {
                gunEquipped = false;
                gun.SetActive(false);
                text.SetActive(false);
            }
            else {
                gunEquipped = true;
                gun.SetActive(true);
                text.SetActive(true);
            }
        }
    }
}
