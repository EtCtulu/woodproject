using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    // Le bool qui dit si le joueur est en range ou pas 
    public bool playerInRange = false;
    // Si le joueur est dans le carré, ça autorise le raycast
    public bool playerinSquare = false;

    // Declaration du personage
    private GameObject player;

    private GameObject pointer;
    

    void Start()
    {
        // On déclare le joueur
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Si le joueur est dans le cube, tiyt est notifié
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinSquare = true;
        }
    }

    // Si le joueur n'y est pas, ça annule tout.
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinSquare = false;
            playerInRange = false;
        }
    }
}
