using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    Rigidbody2D rigd;
    Vector3 directionVec;
    float h;
    float v;
    GameObject scanObject;
    private void Awake()
    {
        rigd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");

        if (vDown && v == 1)
            directionVec = Vector3.up;
        else if (vDown && v == -1)
            directionVec = Vector3.down;
        else if (hDown && h == -1)
            directionVec = Vector3.left;
        else if (hDown && h == 1)
            directionVec = Vector3.right;

        if (Input.GetButtonDown("Interaction") && scanObject != null)  // g키를 누르면
        {
            Debug.Log(scanObject.name);
        }
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(rigd.position, directionVec * 1.5f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigd.position, directionVec, 1.5f, LayerMask.GetMask("Door"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
