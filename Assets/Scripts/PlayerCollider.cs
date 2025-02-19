using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject currentOpenDoor;  // 현재 열린 문 오브젝트
    private GameObject currentClosedDoor; // 현재 닫힌 문 오브젝트
    private bool isNearDoor = false; // 문 근처 여부
    private bool isOpenDoor = false; // 문 상태 (열림/닫힘)

    Rigidbody2D rigid;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dungeon_1_DoorCollision")
        {
            transform.position = gameManager.Dungeon1_Point.position;
            gameManager.Dungeon1_Object.SetActive(true);
            gameManager.isCameraTransitioning1 = true;
        }

        if (collision.gameObject.name == "Dungeon_2_DoorCollision")
        {
            transform.position = gameManager.Dungeon2_Point.position;
            gameManager.Dungeon2_Object.SetActive(true);
            gameManager.isCameraTransitioning2 = true;
            gameManager.isDungeon2 = true;
            rigid.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = true;

            // 감지한 "DoorTrigger"에서 열린 문과 닫힌 문을 찾아야 함
            DoorTrigger triggerScript = collision.GetComponent<DoorTrigger>();
            if (triggerScript != null)
            {
                currentOpenDoor = triggerScript.connectedOpenDoor;  // 열린 문 가져오기
                currentClosedDoor = triggerScript.connectedClosedDoor; // 닫힌 문 가져오기
                Debug.Log($"문 감지: {currentOpenDoor.name}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = false;
            currentOpenDoor = null; // 열린 문 정보 초기화
            currentClosedDoor = null; // 닫힌 문 정보 초기화
        }
    }

    private void Update()
    {

        if (isNearDoor && Input.GetButtonDown("Jump")) // 스페이스바 입력
        {
            if (currentOpenDoor != null && currentClosedDoor != null) // 현재 감지된 문이 있을 때만 실행
            {
                // 문 상태에 따라 열린 문과 닫힌 문을 전환
                isOpenDoor = !isOpenDoor; // 문 상태 변경

                currentOpenDoor.SetActive(isOpenDoor);  // 열린 문 활성화
                currentClosedDoor.SetActive(!isOpenDoor); // 닫힌 문 비활성화

                Debug.Log(isOpenDoor ? "문이 열렸습니다!" : "문이 닫혔습니다!");
            }
            else
            {
                Debug.LogWarning("OpenDoor 또는 ClosedDoor 오브젝트를 찾을 수 없음!");
            }

        }
    }
}
