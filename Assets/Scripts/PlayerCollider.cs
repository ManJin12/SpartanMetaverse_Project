using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;
    private AnimationHandler animationHandler;
    private GameObject currentOpenDoor;  // ���� ���� �� ������Ʈ
    private GameObject currentClosedDoor; // ���� ���� �� ������Ʈ
    private bool isNearDoor = false; // �� ��ó ����
    private bool isOpenDoor = false; // �� ���� (����/����)

    private bool isAngel = false;

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
            uiManager.gameChatText.text = "��ź ���ϱ� �����Դϴ� �����¿�� �����̼���. \nStart�� ������ �����ϰ�, Exit�� ������ ���ư��ϴ�.";
        }

        if (collision.gameObject.name == "Dungeon_2_DoorCollision")
        {
            currentOpenDoor.SetActive(false);
            currentClosedDoor.SetActive(true);
            transform.position = gameManager.Dungeon2_Point.position;
            gameManager.Dungeon2_Object.SetActive(true);
            gameManager.isCameraTransitioning2 = true;
            gameManager.isDungeon2 = true;
            bgLooper.SetActive(true);
            bgLooper.GetComponent<BgLooper>().StartPositions();
            uiManager.dungeon2Btn.SetActive(true);
            uiManager.timeText.gameObject.SetActive(true);
            uiManager.gameChat.SetActive(true);
            uiManager.gameChatText.text = "��ֹ� ���ϱ� �����Դϴ�. SpaceBar�� �����ϼ���. \nStart�� ������ �����ϰ�, Exit�� ������ ���ư��ϴ�.";
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            animationHandler.Damage();
            player.isDead = true;
            gameManager.isGameStart2 = false;
            uiManager.dungeon2Btn.SetActive(true);
            uiManager.gameChat.SetActive(true);
            uiManager.gameChatText.text = uiManager.maxGame2TimeTxt + ", ��� �޼�!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (collision.gameObject.name == "Dungeon_1_DoorOpenCollision")
            {
                uiManager.mainChat.SetActive(true);
                uiManager.mainChatText.text = "��ź ���ϱ� �����Դϴ�. \nSpace Bar�� ���� ���� ���� �ݽ��ϴ�.";
            }
            else if (collision.gameObject.name == "Dungeon_2_DoorOpenCollision")
            {
                uiManager.mainChat.SetActive(true);
                uiManager.mainChatText.text = "��ֹ� ���ϱ� �����Դϴ�.. \nSpace Bar�� ���� ���� ���� �ݽ��ϴ�.";
            }

            isNearDoor = true;

            // ������ "DoorTrigger"���� ���� ���� ���� ���� ã�ƾ� ��
            DoorTrigger triggerScript = collision.GetComponent<DoorTrigger>();
            if (triggerScript != null)
            {
                currentOpenDoor = triggerScript.connectedOpenDoor;  // ���� �� ��������
                currentClosedDoor = triggerScript.connectedClosedDoor; // ���� �� ��������
                Debug.Log($"�� ����: {currentOpenDoor.name}");
            }
        }


        if (collision.gameObject.CompareTag("Bomb"))
        {
            animationHandler.Damage();
            player.isDead = true;
            gameManager.isGameStart1 = false;
            uiManager.dungeon1Btn.SetActive(true);
            uiManager.gameChat.SetActive(true);
            uiManager.gameChatText.text = uiManager.maxGame1TimeTxt + ", ��� �޼�!";
            ObstacleSpawner.RemoveAllChildren();
        }

        if (collision.gameObject.CompareTag("Angel"))
        {
            isAngel = true;
            uiManager.mainChat.SetActive(true);
            uiManager.mainChatText.text = "NPC : õ�� - ĳ���� ����\n �����̽��ٸ� ���� ĳ���͸� ������ �� �־��";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = false;
            currentOpenDoor = null; // ���� �� ���� �ʱ�ȭ
            currentClosedDoor = null; // ���� �� ���� �ʱ�ȭ
            uiManager.mainChat.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Angel"))
        {
            isAngel = false;
            uiManager.mainChat.SetActive(false);
            uiManager.CharacterBtn.SetActive(false);
        }     
    }

    private void Update()
    {

        if (isNearDoor && Input.GetButtonDown("Jump")) // �����̽��� �Է�
        {
            if (currentOpenDoor != null && currentClosedDoor != null) // ���� ������ ���� ���� ���� ����
            {
                // �� ���¿� ���� ���� ���� ���� ���� ��ȯ
                isOpenDoor = !isOpenDoor; // �� ���� ����

                currentOpenDoor.SetActive(isOpenDoor);  // ���� �� Ȱ��ȭ
                currentClosedDoor.SetActive(!isOpenDoor); // ���� �� ��Ȱ��ȭ

                Debug.Log(isOpenDoor ? "���� ���Ƚ��ϴ�!" : "���� �������ϴ�!");
            }
            else
            {
                Debug.LogWarning("OpenDoor �Ǵ� ClosedDoor ������Ʈ�� ã�� �� ����!");
            }

        }

        if(isAngel && Input.GetButtonDown("Jump"))
        {
            if (uiManager.CharacterBtn.activeSelf == false)
            {
                uiManager.mainChat.SetActive(false);
                uiManager.CharacterBtn.SetActive(true);
            }
            else
            {
                uiManager.mainChat.SetActive(true);
                uiManager.CharacterBtn.SetActive(false);
            }
        }
    }
}
