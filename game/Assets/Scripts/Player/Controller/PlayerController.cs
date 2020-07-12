
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator inventoryAnim, equipmentInventoryAnim, playerAnim;

    public InventoryObject inventory, equipment;

    public Attribute[] attributes;

    private Vector2 move;

    [Range(0f, 20f)]
    public float speedForce, accelerateForce;

    private float forceNormilized;

    void OnEnable()
    {
        forceNormilized = speedForce;

        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }

    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
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

        playerAnim.SetFloat("Horizontal", move.x);
        playerAnim.SetFloat("Vertical", move.y);
        playerAnim.SetFloat("speed", move.sqrMagnitude);
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
}
