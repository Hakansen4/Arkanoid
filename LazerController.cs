using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public float laserSpeed = 200;
    Rigidbody2D PhysicLaser;
    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        PhysicLaser = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        PhysicLaser.velocity = Vector2.up * laserSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Ball")
            Destroy(gameObject);
    }
}
