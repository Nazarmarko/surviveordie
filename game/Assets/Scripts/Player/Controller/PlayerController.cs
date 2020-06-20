
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public GameObject inventoryGameObject;
    public InventoryObject inventory;

    private Vector2 move;

    [Range(0f, 20f)]
    public float speedForce, accelerateForce;

    private float forceNormilized;
    void OnEnable()
    {

        forceNormilized = speedForce;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedForce = accelerateForce;
        }
        else
        {
            speedForce = forceNormilized;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryGameObject.activeSelf == false)
            {
                inventoryGameObject.SetActive(true);
            }
            else
            {
                inventoryGameObject.SetActive(false);

            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.Save();
        }
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);
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
