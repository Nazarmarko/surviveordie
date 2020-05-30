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
    void OnEnable()
    {
        forceNormilized = speedForce;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
  void Update()
    {
        print(health);
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift)) { speedForce = accelerateForce; }else { speedForce = forceNormilized; }

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
        rb.MovePosition((rb.position + move) * speedForce * Time.fixedDeltaTime);
    }
   public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
