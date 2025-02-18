using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Vector3 wallTransform;

    [SerializeField]
    private Vector3 Dungeon1Transform;

    private void Awake()
    {
        gameManager = GameManager.Instance;

    }

    public void CameraMoving()
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

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isCameraTransitioning)
        {
            CameraMoving();
        }
        else
        {
            transform.position = Dungeon1Transform; 
        }
    }
}
