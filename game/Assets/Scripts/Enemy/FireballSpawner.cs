using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour {

    private Transform playerTransform;

    [SerializeField]
    private Transform barrelTip;

    [SerializeField]
    private GameObject bullet;

    private Vector2 lookDirection;
    private float lookAngle;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        lookDirection = playerTransform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        GameObject firedBullet = Instantiate(bullet, barrelTip.position, Quaternion.Euler(0f, 0f, lookAngle));
        firedBullet.GetComponent<Rigidbody2D>().velocity = barrelTip.up * 10f;
    }
}
