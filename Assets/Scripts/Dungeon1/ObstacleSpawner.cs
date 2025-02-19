using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    GameManager gameManager;
    public GameObject obstaclePrefab;

    public float spawnInterval = 1.5f; // ó�� �������µ� �ɸ��� �ð�
    public float spawTimer = 0.5f; // ������ �ð�
    private float spawnTime = 0f; // �帣�� �ð� ���� ����

    private bool startChk = false;
    public Transform minRange;
    public Transform maxRange;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        if(!gameManager.isGameStart1)
            return;

        spawnTime += Time.deltaTime;

        if (!startChk)
        {
            if (spawnTime >= spawnInterval)
            {
                startChk = true;
            }
            return;
        }
        

        if (spawnTime >= spawTimer)
        {
            SpawnObstacle();
            spawnTime = 0f;
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(minRange.transform.position.x, maxRange.transform.position.x);
        float randomY = Random.Range(minRange.transform.position.y, maxRange.transform.position.y);
        Vector2 spawnPosition = new Vector2 (randomX, randomY);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        obstacle.transform.parent = transform;
    }

    public void RemoveAllChildren()
    {
        for (int i = transform.childCount - 1; 0 <= i; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
