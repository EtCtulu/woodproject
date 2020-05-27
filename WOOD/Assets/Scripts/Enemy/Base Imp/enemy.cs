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
        if(hp <= 0f)
        {
            Destroy(gameObject);
        }
        // Si le player est dans la range, la destination sera le joueur.
        if(GetComponentInChildren<playerDetector>().playerInRange == true)
        {
            // La destination est set a Player, on recherche donc son transform
            destination = player.transform.position;
            // On déclare que la destination est set a destination
            agent.destination = destination;
        }
        // A la sortie de la zone, la destination sera lui même, il ne bougera donc pas.
        if(GetComponentInChildren<playerDetector>().playerInRange == false)
        {
            destination = this.transform.position;
            agent.destination = destination;
        }
    }
}
