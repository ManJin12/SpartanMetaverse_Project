using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject currentOpenDoor;  // ���� ���� �� ������Ʈ
    private GameObject currentClosedDoor; // ���� ���� �� ������Ʈ
    private bool isNearDoor = false; // �� ��ó ����
    private bool isOpenDoor = false; // �� ���� (����/����)

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

            // ������ "DoorTrigger"���� ���� ���� ���� ���� ã�ƾ� ��
            DoorTrigger triggerScript = collision.GetComponent<DoorTrigger>();
            if (triggerScript != null)
            {
                currentOpenDoor = triggerScript.connectedOpenDoor;  // ���� �� ��������
                currentClosedDoor = triggerScript.connectedClosedDoor; // ���� �� ��������
                Debug.Log($"�� ����: {currentOpenDoor.name}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            isNearDoor = false;
            currentOpenDoor = null; // ���� �� ���� �ʱ�ȭ
            currentClosedDoor = null; // ���� �� ���� �ʱ�ȭ
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
    }
}
