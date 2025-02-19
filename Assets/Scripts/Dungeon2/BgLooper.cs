using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 3;

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGrund"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }

        UpDownObstacle obstacle = collision.GetComponent<UpDownObstacle>();

        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void ResetPositions()
    {
        foreach (var kvp in originalPositions)
        {
            kvp.Key.transform.position = kvp.Value;
        }
    }

    public void StartPositions()
    {
        UpDownObstacle[] obstacles = GameObject.FindObjectsOfType<UpDownObstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            originalPositions[obstacles[i].gameObject] = obstacles[i].transform.position;
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }

        GameObject[] background = GameObject.FindGameObjectsWithTag("BackGrund");
        foreach (GameObject bg in background)
        {
            originalPositions[bg] = bg.transform.position;
        }
    }
}