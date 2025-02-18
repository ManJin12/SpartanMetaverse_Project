using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnInterval = 1.5f; // ó�� �������µ� �ɸ��� �ð�
    public float spawTimer = 0.5f; // ������ �ð�
    private float spawnTime = 0f; // �帣�� �ð� ���� ����

    public Transform minRange;
    public Transform maxRange;

    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= spawnInterval)
        {
            if (spawnTime >= spawTimer)
            {
                SpawnObstacle();
                spawnTime = 0f;
            }
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(minRange.transform.position.x, maxRange.transform.position.x);
        float randomY = Random.Range(minRange.transform.position.y, maxRange.transform.position.y);
        Vector2 spawnPosition = new Vector2 (randomX, randomY);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
