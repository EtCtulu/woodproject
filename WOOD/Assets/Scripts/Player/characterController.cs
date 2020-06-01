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
        // Le double saut est mis avant le saut normal, si le bool du double saut est en true, on peut sauter, très très très autorisé.
        if (doubleJump == true && Input.GetButtonDown("Jump"))
        {
            // La force du saut
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
        }
        // Si on est détecté au sol, on saute et basse le bool de double saut en true
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            // La force du saut
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump = true;


        }


        // Ceci est le crouch, quand on appuyes sur le CTRL de gauche, la scale du joueur se baisse, et quand on relache, ça remonte.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = transform.localScale + new Vector3(0f, -0.7f, 0f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = transform.localScale + new Vector3(0f, +0.7f, 0f);
        }
        
    }

    // Le check du ground
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y
            , col.bounds.center.z), col.radius * .9f, groundLayer);
    }

    
}
