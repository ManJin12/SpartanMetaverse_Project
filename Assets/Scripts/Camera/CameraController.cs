using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Vector3 wallTransform;

    [SerializeField]
    private Vector3 Dungeon1Transform;

    [SerializeField]
    private Vector3 Dungeon2Transform;

    


    private void Awake()
    {
        gameManager = GameManager.Instance;

    }

    public void MainCameraMoving()
    {
        Vector3 camPosition = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        Vector3 camPositionX = transform.position;
        Vector3 camPositionY = transform.position;

        if (!(wallTransform.x < Mathf.Abs(camPosition.x)))
        {
            camPositionX = new Vector3(camPosition.x, 0, 0);
        }

        if (!(wallTransform.y < Mathf.Abs(camPosition.y)))
        {
            camPositionY = new Vector3(0, camPosition.y, 0);
        }

        transform.position = new Vector3(camPositionX.x, camPositionY.y, transform.position.z);
    }

    public void Dungeon1Move()
    {
        transform.position = Dungeon1Transform;
    }

    public void Dungeon2Move()
    {
        if (!gameManager.isDungeon2PositionSet)
        {
            transform.position = Dungeon2Transform;
            gameManager.offsetX = transform.position.x - Target.position.x;
            gameManager.isDungeon2PositionSet = true;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x = Target.position.x + gameManager.offsetX;
            transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isCameraTransitioning1 && !gameManager.isCameraTransitioning2)
        {
            MainCameraMoving();
        }
        else if(gameManager.isCameraTransitioning1)
        {
            Dungeon1Move();
        }
        else if(gameManager.isCameraTransitioning2)
        {
            Dungeon2Move();
        }
    }
}
