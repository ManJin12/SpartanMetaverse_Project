using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dungeon_1_DoorCollision")
        {
            transform.position = GameManager.Instance.Dungeon1_Point.position;
            GameManager.Instance.isCameraTransitioning = true;
        }
    }
}
