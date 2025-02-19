using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera mainCamera;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.position = Vector3.zero;
        mainCamera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical);
        
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        lookDirection = (worldPosition - (Vector2) transform.position);

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
