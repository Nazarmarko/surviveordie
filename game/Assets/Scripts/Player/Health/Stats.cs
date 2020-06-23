using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    [Range(0.1f,30)]
    [SerializeField]float currentHealth, experience, gold, maxHealth;

    public TMP_Text healthText, expText, goldText;

    public Slider ImgHealthBar;

    public int MinValue, MaxValue, currentValue;

    void Update()
    {
        if (healthText != null || expText != null || goldText != null)
        {
            healthText.text = currentHealth.ToString();
            expText.text = experience.ToString();
            goldText.text = gold.ToString();
        }
        if (currentHealth <= 0)
        {
            //Die
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
        }

    //    healthBar.value = currentHealth / maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}

