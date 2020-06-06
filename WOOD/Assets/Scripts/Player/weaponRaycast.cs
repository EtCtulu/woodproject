using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponRaycast : MonoBehaviour
{
    // Les dégats des armes
    [Header("Damages")]
    public float pistolDmg = 10;
    public float smgDmg = 3;

    // Les munitions des armes
    [Header("Ammo")]
    public int pistolAmmo = 100;
    public int smgAmmo = 100;

    // Ui pour les munitions
    GameObject canvas;
    public Text ammoText;

    // Les variables qui se modifient au changement de l'arme
    int equipedWpnAmmo;
    string equipedWpn;

    // Les bools des armes
    bool pistol = true;
    bool smg = false;

    // Déclaration de l'animator des armes.
    public Animator pistolAnim;

    [Header("Cadence de tir")]
    public float timerBetweenShots = 0;
    public float timerBetweenShotsPistol = 0.5f;
    public float timerBetweenShotsSMG = 0.1f;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        ammoText = canvas.transform.Find("ammo").gameObject.GetComponent<Text>();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        pistolAnim = this.transform.Find("Pistol").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timerBetweenShots = timerBetweenShots - Time.deltaTime;

        ammoText.text = equipedWpn + equipedWpnAmmo;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.

        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        layerMask = ~layerMask;

        RaycastHit hit;

        // Permets de voir le raycast du pistolet et sa range, il est en rouge
        Debug.DrawRay(transform.position, transform.forward * 30, Color.red);

        // L'equipement du pistolet et ce qui suffit
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pistol = true;
            smg = false;
            equipedWpn = "Pistol : ";
            timerBetweenShots = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            smg = true;
            pistol = false;
            equipedWpn = "Smg : ";
            timerBetweenShots = 0f;
        }
        if (pistol == true)
        {
            equipedWpnAmmo = pistolAmmo;
            equipedWpn = "Pistol : ";
        }
        if (smg == true)
        {
            equipedWpnAmmo = smgAmmo;
            equipedWpn = "Smg : ";
        }

        if (Input.GetButton("Fire1"))
        {
            
            
            // Les munitions qui décrémentent ici, il faut bien la mettre après le raycast pour que les munitions partent après le tir.
            if (pistol == true && pistolAmmo != 0 && timerBetweenShots <= 0f)
            {
                // Raycast du pistolet, il as une range de 30, et on peut tirer que si on as des balles
                if (Physics.Raycast(transform.position, transform.forward, out hit, 30, layerMask))
                {


                    // Permet de savoir si on as touché un Enemy classique
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.transform.GetComponent<enemy>().hp -= pistolDmg;
                        Debug.Log("Touché");
                    }
                }
                FindObjectOfType<AudioManager>().Play("FirePistol");
                // Le trigger du shoot de l'anim.
                pistolAnim.SetTrigger("Trigger");
                // Les munitions qui décrémentes
                pistolAmmo--;
                Debug.Log(pistolAmmo);
                timerBetweenShots = timerBetweenShotsPistol;
            }

            // Les munitions qui décrémentent ici, il faut bien la mettre après le raycast pour que les munitions partent après le tir.
            if (smg == true && smgAmmo != 0 && timerBetweenShots <= 0f)
            {
                // Raycast du pistolet, il as une range de 30, et on peut tirer que si on as des balles
                if (Physics.Raycast(transform.position, transform.forward, out hit, 30, layerMask))
                {


                    // Permet de savoir si on as touché un Enemy classique
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.transform.GetComponent<enemy>().hp -= smgDmg;
                        Debug.Log("Touché");
                    }
                }
                FindObjectOfType<AudioManager>().Play("FirePistol");
                // Le trigger du shoot de l'anim.
                pistolAnim.SetTrigger("Trigger");
                // Les munitions qui décrémentes
                smgAmmo--;
                Debug.Log(smgAmmo);
                timerBetweenShots = timerBetweenShotsSMG;
            }
        }
    }
}
