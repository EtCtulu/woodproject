using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterHP : MonoBehaviour
{
    // Déclaration des scripts de Vie et d'armure
    [Header("Health And Armor")]
    public float maxHp = 100;
    public float hp = 100;
    public float armor = 0;

    [Header("Barre de vie")]
    public Image hpBar;
    private float ratio;

    // Le float qui changera en fonction des dégats
    private float dmgToTake;

    void Start()
    {
        Debug.Log("Vie : " + hp);
    }

    // Update is called once per frame
    void Update()
    {
        ratio = hp / maxHp;
        hpBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Le script pour les dégats de Melee
       if(other.gameObject.tag == "Melee")
        {
            dmgToTake = other.GetComponent<dmg>().damageMelee;

            hp = hp - dmgToTake;

            Debug.Log("Vie : " + hp);

        } 
    }
}
