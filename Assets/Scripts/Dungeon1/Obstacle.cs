using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed *  Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("게임 오버");
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("RemoveCollision"))
        {
            Destroy(collision.gameObject);
        }
    }
}
