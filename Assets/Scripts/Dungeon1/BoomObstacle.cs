using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomObstacle : MonoBehaviour
{
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed *  Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RemoveCollision"))
        {
            Destroy(gameObject);
        }
    }
}
