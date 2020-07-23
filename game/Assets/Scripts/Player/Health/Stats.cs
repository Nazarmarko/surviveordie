using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    [Range(0.1f, 30)]
    [SerializeField]
    float currentHealth, experience, gold, maxHealth;

    public TMP_Text expText, goldText;

    public Image[] armorLevelImages;
    public SpriteRenderer[] armorOnPlayerSprite;

    public int MinValue, MaxValue, currentValue, headInt, chestInt, bootsInt;

    void Start()
    {
        ArmorVisualise(headInt, chestInt, bootsInt);
    }
    void Update()
    {
        if (expText != null || goldText != null)
        {
            expText.text = experience.ToString();
            goldText.text = gold.ToString();
        }
        if (currentHealth <= 0)
        {
            //Die
        }
    }
    public void ArmorOnPlayerVisualise(Sprite armorSprite, int armorType)
    {
        armorOnPlayerSprite[armorType].sprite = armorSprite;
    }
    public void ArmorVisualise(int headModifier, int chestModifier, int bootsModifier)
    {
        headInt = headModifier;
        chestInt = chestModifier;
        bootsInt = bootsModifier;

        int armorInt = headInt + chestInt + bootsInt;

        for (int i = 0; i < armorLevelImages.Length; i++)
        {
            if (armorInt <= i)
                armorLevelImages[i].enabled = false;
            else
                armorLevelImages[i].enabled = true;
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}

