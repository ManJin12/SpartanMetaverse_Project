using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownObstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPositon, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize, 0);
        bottomObject.localPosition = new Vector3(0, - halfHoleSize, 0);

        Vector3 placePosition = lastPositon + new Vector3(widthPadding, 0, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.localPosition = placePosition;
        return placePosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
