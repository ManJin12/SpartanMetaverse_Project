using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D rigid;
    [SerializeField] private SpriteRenderer CharacterRenderer;
    [SerializeField] private Transform weaponPivot;

    [SerializeField] public int health = 100;
    [SerializeField] private float moveSpeed = 3f;
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Rotate(lookDirection);
        HandleAction();
    }
    protected virtual void FixedUpdate()
    {
        MoveMent(movementDirection);    
    }

    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * moveSpeed;

        rigid.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rot) > 90f;

        CharacterRenderer.flipX = isLeft;

    }
}
