using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [Header("Variables")]
    // Vitesse du joueur
    public float speed = 10f;

    // Force du Jump
    public float jumpForce = 7;

    // La layer pour le groundcheck
    public LayerMask groundLayer;

    // Raccourcis
    public CapsuleCollider col;
    private Rigidbody rb;

    // Nombre de jumps
    [Header("DoubleJump")]
    // public int maxDoubleJump = 1;
    // public int doubleJump = 1;
    private bool doubleJump = false;



    void Start()
    {
        // Curseur bloqué au centre du joueur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Déclaration des raccourcis
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.Clamp(doubleJump, 0, maxDoubleJump);

        // L'axis vertical
        float translation = Input.GetAxis("Vertical") * speed;

        // L'axis horizontal
        float straffe = Input.GetAxis("Horizontal") * speed;

        // Permets de ne pas avoir un vitesse diff suivant les frames
        translation *= Time.deltaTime;

        // Pareil pour le straffe
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);


        // Débloquage de la souris
        if (Input.GetKeyDown("escape"))
        {

            // Enlève le bloquage du curseur
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        // Le saut
        if (doubleJump == true && Input.GetButtonDown("Jump"))
        {
            // La force du saut
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
        }
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            // La force du saut
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = true;


        }        
        
    }

    // Le check du ground
    private bool IsGrounded()
    {
        // doubleJump = maxDoubleJump;
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y
            , col.bounds.center.z), col.radius * .9f, groundLayer);
    }

    
}
