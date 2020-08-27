
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator inventoryAnim, equipmentInventoryAnim, playerAnim;

    public GameObject[] shieldsObj;

    public InventoryObject inventory, equipment;

    public Attribute[] attributes;

    private Stats playerStats;

    private Vector2 move;

    [Range(0f, 20f)]
    public float speedForce, accelerateForce;

    private float forceNormilized;

    public SpriteRenderer[] armors;
    void OnEnable()
    {
        forceNormilized = speedForce;

        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerStats = GetComponent<Stats>();

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
        }
    }

    #region SlotConected
    public void OnRemoveItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;

        switch (_slot.parent.inventory.type)
        {

            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                if (_slot.ItemObject.uiDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            playerStats.ArmorOnPlayerVisualise(null, 0);
                            playerStats.ArmorVisualise(0, playerStats.chestInt, playerStats.bootsInt);
                            break;
                        case ItemType.Weapon:
                            playerStats.weaponInt = 0;
                            break;
                        case ItemType.Shields:
                            playerStats.shealdInt = 0;
                            break;
                        case ItemType.Boots:
                            playerStats.ArmorOnPlayerVisualise(null, 2);
                            playerStats.ArmorVisualise(playerStats.headInt, playerStats.chestInt, 0);
                            break;
                        case ItemType.Chest:
                            playerStats.ArmorOnPlayerVisualise(null, 1);
                            playerStats.ArmorVisualise(playerStats.headInt, 0, playerStats.bootsInt);
                            break;
                    }
                }
                break;
        }

    }
    public void OnAddItem(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;

        int armorValue = _slot.item.buffs[0].value;
        int weaponValue = _slot.item.buffs[1].value;
        int shealdValue = _slot.item.buffs[2].value;

        Sprite armorSprite = _slot.ItemObject.uiDisplay;

        switch (_slot.parent.inventory.type)
        {

            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                if (_slot.ItemObject.uiDisplay != null)
                {
                    switch (_slot.AllowedItems[0])
                    {
                        case ItemType.Helmet:
                            playerStats.ArmorOnPlayerVisualise(armorSprite, 0);
                            playerStats.ArmorVisualise(armorValue, playerStats.chestInt, playerStats.bootsInt);
                            break;
                        case ItemType.Weapon:
                            playerStats.weaponInt = weaponValue;
                            break;
                        case ItemType.Shields:
                            switch (_slot.ItemObject.type)
                            {
                                case ItemType.Weapon:
                                    playerStats.weaponInt = weaponValue;
                                    break;
                                case ItemType.Shields:
                                    playerStats.shealdInt = shealdValue;
                                    break;
                            }
                            break;
                        case ItemType.Boots:
                            playerStats.ArmorOnPlayerVisualise(armorSprite, 2);
                            playerStats.ArmorVisualise(playerStats.headInt, playerStats.chestInt, armorValue);
                            break;
                        case ItemType.Chest:
                            playerStats.ArmorOnPlayerVisualise(armorSprite, 1);
                            playerStats.ArmorVisualise(playerStats.headInt, armorValue, playerStats.bootsInt);
                            break;
                        //case ItemType.Tool:
                        //    playerStats.ArmorOnPlayerVisualise(armorSprite, 1);
                        //    playerStats.ArmorVisualise(playerStats.headInt, armorValue, playerStats.bootsInt);
                        //    break;
                    }
                }
                break;
        }
    }
    #endregion

    void Update()
    {

        #region MovementInput
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedForce = accelerateForce;
        }
        else
        {
            speedForce = forceNormilized;
        }

        if (move.x >= 0)
        {
            this.transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-5, 5, 1);
        }
        #endregion

        #region InventoryInput
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryAnim.GetBool("IsOpen") == false)
            {
                inventoryAnim.SetBool("IsOpen", true);
            }
            else
            {
                inventoryAnim.SetBool("IsOpen", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (equipmentInventoryAnim.GetBool("IsOpened") == false)
            {
                equipmentInventoryAnim.SetBool("IsOpened", true);
            }
            else
            {
                equipmentInventoryAnim.SetBool("IsOpened", false);
            }
        }
        #endregion

        #region IventoryLoadInput
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
            equipment.Load();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.Save();
            equipment.Save();
        }
        #endregion

        #region AnimatorController
        playerAnim.SetFloat("Horizontal", move.x);
        playerAnim.SetFloat("speed", move.sqrMagnitude);
        #endregion
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    #region Attribute
    public void AttributeModified(Attribute attribute)
    {

    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        public PlayerController parent;
        public Attributes type;
        public ModifiableInt value;
        public void SetParent(PlayerController _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }
        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }
    #endregion
}
