using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;
    private AnimationHandler animationHandler;
    private GameObject currentOpenDoor;  // 현재 열린 문 오브젝트
    private GameObject currentClosedDoor; // 현재 닫힌 문 오브젝트
    private bool isNearDoor = false; // 문 근처 여부
    private bool isOpenDoor = false; // 문 상태 (열림/닫힘)

    public ObstacleSpawner ObstacleSpawner;
    Rigidbody2D rigid;

    public GameObject bgLooper;

    public PlayerController player;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        rigid = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        player = GetComponent<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.name == "Dungeon_1_DoorCollision")
        {
            currentOpenDoor.SetActive(false); 
            currentClosedDoor.SetActive(true);
            transform.position = gameManager.Dungeon1_Point.position;
            gameManager.Dungeon1_Object.SetActive(true);
            gameManager.isCameraTransitioning1 = true;
            gameManager.isDungeon1 = true;
            uiManager.dungeon1Btn.SetActive(true);
            uiManager.timeText.gameObject.SetActive(true);
            uiManager.gameChat.SetActive(true);
            uiManager.gameChatText.text = "폭탄 피하기 게임입니다. \nStart를 누르면 시작하고, Exit를 누르면 돌아갑니다.";
        }

        if (collision.gameObject.name == "Dungeon_2_DoorCollision")
        {
            currentOpenDoor.SetActive(false);
            currentClosedDoor.SetActive(true);
            transform.position = gameManager.Dungeon2_Point.position;
            gameManager.Dungeon2_Object.SetActive(true);
            gameManager.isCameraTransitioning2 = true;
            gameManager.isDungeon2 = true;
            rigid.gravityScale = 1;
            bgLooper.SetActive(true);
            uiManager.dungeon2Btn.SetActive(true);
            uiManager.timeText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = true;

            uiManager.mainChat.SetActive(true);
            uiManager.mainChatText.text = "폭탄 피하기 게임입니다. \nSpace Bar를 눌러 문을 열고 닫습니다.";

            // 감지한 "DoorTrigger"에서 열린 문과 닫힌 문을 찾아야 함
            DoorTrigger triggerScript = collision.GetComponent<DoorTrigger>();
            if (triggerScript != null)
            {
                currentOpenDoor = triggerScript.connectedOpenDoor;  // 열린 문 가져오기
                currentClosedDoor = triggerScript.connectedClosedDoor; // 닫힌 문 가져오기
                Debug.Log($"문 감지: {currentOpenDoor.name}");
            }
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            animationHandler.Damage();
            player.isDead = true;
            gameManager.isGameStart1 = false;
            uiManager.dungeon1Btn.SetActive(true);
            uiManager.gameChat.SetActive(true);
            uiManager.gameChatText.text = uiManager.maxGame1TimeTxt + ", 기록 달성!";
            ObstacleSpawner.RemoveAllChildren();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = false;
            currentOpenDoor = null; // 열린 문 정보 초기화
            currentClosedDoor = null; // 닫힌 문 정보 초기화
            uiManager.mainChat.SetActive(false);
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
