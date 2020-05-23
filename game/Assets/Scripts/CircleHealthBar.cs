using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CircleHealthBar : MonoBehaviour
{
    public Image healthBar;
    public Text Healthtext;

    public float ShowHealth;
    float maxHealth = 100f;
    

    public static float health;
    void Start() {      
        //ShowHealth = float.Parse(Healthtext.text); ShowHealth / maxHealth
    }

// Update is called once per frame
void Update()
    {
        healthBar.fillAmount = Health.Instance.curHealth / maxHealth;
    }
}
