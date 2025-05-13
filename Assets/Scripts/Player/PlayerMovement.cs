using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float transperency = 0.3f;

    public Vector2 MoveDirection => moveDirection;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private PlayerActions actions;
    
    private Vector2 moveDirection;
    private float currentSpeed;
    private bool usingDash;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        actions = new PlayerActions();
    }

    private void Start()
    {
        currentSpeed = speed;
        actions.Movement.Dash.performed += context => Dash();
    }

    private void Update()
    {
        CaptureInput();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb2D.MovePosition(rb2D.position + moveDirection * (currentSpeed * Time.fixedDeltaTime));
    }
    
    private void Dash()
    {
        if (usingDash)
        {
            return;
        }

        usingDash = true;
        StartCoroutine(IEDash());
    }
    
    private IEnumerator IEDash()
    {
        currentSpeed = dashSpeed;
        ModifySpriteRenderer(transperency);
        yield return new WaitForSeconds(dashTime);
        currentSpeed = speed;
        ModifySpriteRenderer(1f);
        usingDash = false;
    }

    private void RotatePlayer()
    {
        if (moveDirection.x >= 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
    }
    
    private void ModifySpriteRenderer(float alpha)
    {
        Color color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, alpha);
        spriteRenderer.color = color;
    }

    public void FaceRightDirection()
    {
        spriteRenderer.flipX = false;
    }
    
    private void CaptureInput()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
    }
    
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
