using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private GameManager gameManager;
    public Rigidbody2D rigid;

    [SerializeField] public SpriteRenderer KnightRenderer;
    [SerializeField] public SpriteRenderer ElfRenderer;
    [SerializeField] private Transform weaponPivot;

    [SerializeField] public int health = 100;
    [SerializeField] private float moveSpeed = 3f;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;

    bool isFlap = false;
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        rigid = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (isDead)
        {
            rigid.velocity = Vector2.zero;
            return;
        }
            

        if (gameManager.isDungeon2 )
        {
            if (gameManager.isKnight)
            {
                KnightRenderer.flipX = false;
            }
            else if (gameManager.isElf)
            {
                ElfRenderer.flipX = false;
            }

            if (gameManager.isGameStart2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isFlap = true;
                }
                return;
            }
            return;
        }

        Rotate(lookDirection);
        HandleAction();
    }
    protected virtual void FixedUpdate()
    {
        if (isDead)
            return;

        if (gameManager.isDungeon2)
        {
            if (gameManager.isGameStart2)
            {
                Jump();
                return;
            }
            return;
        }
        MoveMent(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    private void MoveMent(Vector2 direction)
    {
        direction = direction * moveSpeed;

        rigid.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rot) > 90f;

        if (gameManager.isKnight)
        {
            KnightRenderer.flipX = isLeft;
        }
        if (gameManager.isElf)
        {
            ElfRenderer.flipX = isLeft;
        }

    }

    private void Jump()
    {
        Vector3 velocity = rigid.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        rigid.velocity = velocity;

        float angle = Mathf.Clamp((rigid.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
