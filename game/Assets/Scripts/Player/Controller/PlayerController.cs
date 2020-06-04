using TMPro;
using UnityEngine;   


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Vector2 move;

    [Range(0f, 20f)]
    public float speedForce, accelerateForce, health, experience, gold;
    
    private float forceNormilized;

    public TMP_Text healthText, expText, goldText;

    #region Inventory
    public static PlayerController Instance { get; private set; }

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

   // [SerializeField] private MaterialTintColor materialTintColor;

    private State state;

    private enum State
    {
        Normal,
    }
    #endregion
    void OnEnable()
    {
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        inventory = new Inventory(UseItem);

        forceNormilized = speedForce;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
  void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift)) { 
            speedForce = accelerateForce; 
        }
        else { 
            speedForce = forceNormilized; 
        }

        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);

        if (healthText != null || expText != null || goldText != null)
        {
            healthText.text = health.ToString();
            expText.text = experience.ToString();
            goldText.text = gold.ToString();
        }
        if(health <= 0)
        {
            //Die
        }

    }
    void FixedUpdate()
    {     
        rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
    }
   public void TakeDamage(float damage)
    {
        health -= damage;
    }

    #region InventoryMethods
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
              //  FlashGreen();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
               // FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
        }
    }
    //private void DamageFlash()
    //{
    //    materialTintColor.SetTintColor(new Color(1, 0, 0, 1f));
    //}

    public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance)
    {
        transform.position += knockbackDir * knockbackDistance;
      //  DamageFlash();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //public void FlashGreen()
    //{
    //    materialTintColor.SetTintColor(new Color(0, 1, 0, 1));
    //}

    //public void FlashRed()
    //{
    //    materialTintColor.SetTintColor(new Color(1, 0, 0, 1));
    //}

    //public void FlashBlue()
    //{
    //    materialTintColor.SetTintColor(new Color(0, 0, 1, 1));
    //}
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
    //    if (itemWorld != null)
    //    {
    //        inventory.AddItem(itemWorld.GetItem());
    //        itemWorld.DestroySelf();
    //    }
    //}
    #endregion
}
