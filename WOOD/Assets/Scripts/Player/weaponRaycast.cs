using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponRaycast : MonoBehaviour
{
    // Start is called before the first frame update
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
        Debug.DrawRay(transform.position, transform.forward * 20, Color.red);

        if (Input.GetButtonDown("Fire1"))
        {
            // Raycast du pistolet
            if (Physics.Raycast(transform.position, transform.forward, out hit, 20, layerMask)) 
            {
                

                // Permet de savoir si on as touché un Enemy classique
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Touché");
                }
            }
        }
    }
}
