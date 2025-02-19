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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnimationHandler animationHandler = collision.gameObject.GetComponent<AnimationHandler>();
            animationHandler.Damage();
            Debug.Log("게임 오버");
            Destroy(collision.gameObject, 2f);
        }

        if(collision.gameObject.CompareTag("RemoveCollision"))
        {
            Destroy(collision.gameObject);
        }
    }
}
