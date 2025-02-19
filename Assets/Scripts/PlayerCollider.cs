using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject OpenDoor;
    public GameObject CloseDoor;
    private bool isNearDoor = false;
    private bool isOpenDoor = false;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dungeon_1_DoorCollision")
        {
            transform.position = GameManager.Instance.Dungeon1_Point.position;
            GameManager.Instance.Dungeon1_Object.SetActive(true);
            GameManager.Instance.isCameraTransitioning = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = true;
            Debug.Log(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = false; // 문에서 멀어짐
        }
    }

    private void Update()
    {

        if(isNearDoor && Input.GetButtonDown("Jump") && isOpenDoor)
        {
            OpenDoor.SetActive(true);
            CloseDoor.SetActive(false);
            isOpenDoor = false;
            Debug.Log("문이 닫혔습니다!");
        }
        else if (isNearDoor && Input.GetButtonDown("Jump")) // 점프 키를 눌렀을 때
        {
            OpenDoor.SetActive(false); // 문 비활성화 (열림)
            CloseDoor.SetActive(true);
            isOpenDoor = true;
            Debug.Log("문이 열렸습니다!");
        }
    }
}
