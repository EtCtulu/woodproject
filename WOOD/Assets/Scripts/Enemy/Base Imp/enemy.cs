using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    [Header("Variables")]
    public float hp = 100f;

    // Déclaration en privé du joueur et de l'agent
    private GameObject player;
    private NavMeshAgent agent;

    // La destination
    Vector3 destination;


    void Start()
    {
        // On cherche le joueur puis l'agent
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            Destroy(gameObject);
        }
        // Si le player est dans la range, la destination sera le joueur.
        if (GetComponentInChildren<playerDetector>().playerInRange == true)
        {
            // La destination est set a Player, on recherche donc son transform
            destination = player.transform.position;
            // On déclare que la destination est set a destination
            agent.destination = destination;
        }
        // A la sortie de la zone, la destination sera lui même, il ne bougera donc pas.
        if (GetComponentInChildren<playerDetector>().playerInRange == false)
        {
            destination = this.transform.position;
            agent.destination = destination;
        }

        // Création du layermask
        int layerMask = 1 << 8;

        layerMask = ~layerMask;


        // La valeur hit du raycast
        RaycastHit hit;

        if (GetComponentInChildren<playerDetector>().playerinSquare == true)
        {

            // Permets de définir une localisation celle-ci est le player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Le raycast part de l'enemy et la direction est le player.
            if (Physics.Raycast(this.transform.position, direction, out hit, Mathf.Infinity, layerMask)) //Range infinie
            {
                // Si le raycast entre en collision avec le player, ça le poursuit.
                Debug.DrawRay(this.transform.position, direction, Color.cyan);
                if (hit.collider.tag == "Player")
                {
                    GetComponentInChildren<playerDetector>().playerInRange = true;
                }
                // Si le raycast entre en contact avec tout sauf le player, il s'arrète
                else
                {
                    GetComponentInChildren<playerDetector>().playerInRange = false;
                }
            }
        }
    }
}
    

