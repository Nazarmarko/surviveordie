
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rb;

    public Animator inventoryAnim;
    public InventoryObject inventory;

    private Vector2 move;

    [Range(0f, 20f)]
    public float speedForce, accelerateForce;

    private float forceNormilized;
    void OnEnable()
    {
        forceNormilized = speedForce;

        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
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
        #endregion

        #region IventoryLoadInput
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.Save();
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
            inventory.AddItem(item.item, 1);
            Destroy(collision.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }
}
