using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Stats : MonoBehaviour
{
    [Range(0.1f, 30)]
    [SerializeField]
    int currentHealth, experience, gold, maxHealth;

    public TMP_Text expText, goldText;

    public Image[] armorLevelImages, healthLevelImages;
    public SpriteRenderer[] armorOnPlayerSprite;

    public int MinValue, MaxValue, currentValue, headInt, chestInt, bootsInt, weaponInt, shealdInt;

    void Start()
    {
        ArmorVisualise(headInt, chestInt, bootsInt);

        HealthVisualise(maxHealth);
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
    #region ArmorVisualise
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
    #endregion

    public void HealthVisualise(int healthInt)
    {
        currentHealth -= healthInt;
        for (int i = 0; i < healthLevelImages.Length; i++)
        {
            if (healthInt <= i)
                healthLevelImages[i].enabled = false;
            else
                healthLevelImages[i].enabled = true;
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
        }
    }
}

