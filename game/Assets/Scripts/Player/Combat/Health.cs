using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public float curHealth = 100;
    public Text HealthText;
    [SerializeField]
    private Animator anim;
    private bool Dead;
    void Update()
    {
        HealthText.text = "u:" + curHealth.ToString();

        if (curHealth >= 100) {
            curHealth = 100;
        }

        if (curHealth <= 0 && !Dead)
        {
            Dead = true;
            Death();
        }
    }
    private void Awake()
    {
        Dead = false;
        anim.SetBool("DieBool", false);

        if (Instance != null)
        {
            DestroyImmediate(this);
        }
        else Instance = this;
    }
     
    void Death() {
            

        anim.SetTrigger("Die");
        anim.SetBool("DieBool", true);

    }

    public void TakeDamage(float damage) {
        curHealth -= damage;
        anim.SetTrigger("Hurt");
    }

    public void ReloadLevel() 
    {
        Application.LoadLevel(Application.loadedLevel);

    }
}
