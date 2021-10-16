using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillController : MonoBehaviour
{
    float Speed = 50f;
    //-----------------------------------------------------------------------------

    void Update()
    {
        MoveDown();
    }
    void MoveDown()
    {
        transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Racket"))
            Destroy(this.gameObject);
    }
}
