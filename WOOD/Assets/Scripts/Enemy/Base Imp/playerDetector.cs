using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    // Le bool qui dit si le joueur est en range ou pas 
    public bool playerInRange = false;
    // Si le joueur est dans le carré, ça autorise le raycast
    private bool playerinSquare = false;

    // Declaration du personage
    private GameObject player;

    private GameObject pointer;
    

    void Start()
    {
        // On déclare le joueur
        player = GameObject.FindGameObjectWithTag("Player");

        // Declaration du raycast
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        pointer = this.transform.Find("ray").gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform);

        pointer.transform.LookAt(player.transform);
        
        // Création du layermask
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        // La valeur hit du raycast
        RaycastHit hit;

        if (playerinSquare == true)
        {
            if (Physics.Raycast(transform.position, player.transform.position, out hit, Mathf.Infinity, layerMask)) //Range infinie
            {
                Debug.DrawRay(transform.position, player.transform.position, Color.cyan);
                if(hit.collider.tag == "Player")
                {
                    playerInRange = true;
                }
            }
            else
            {
                playerInRange = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinSquare = true;
            // playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinSquare = false;
            playerInRange = false;
        }
    }
}
