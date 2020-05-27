using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponRaycast : MonoBehaviour
{
    // Les dégats des armes
    [Header("Damages")]
    public float pistolDmg = 10;

    void Start()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.

        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        layerMask = ~layerMask;

        RaycastHit hit;

        // Permets de voir le raycast du pistolet et sa range, il est en rouge
        Debug.DrawRay(transform.position, transform.forward * 30, Color.red);

        if (Input.GetButtonDown("Fire1"))
        {
            // Raycast du pistolet, il as une range de 30
            if (Physics.Raycast(transform.position, transform.forward, out hit, 30, layerMask)) 
            {
                

                // Permet de savoir si on as touché un Enemy classique
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    hit.transform.GetComponent<enemy>().hp -= pistolDmg;
                    Debug.Log("Touché");
                }
            }
        }
    }
}
