using System.Collections;
using UnityEngine;

public class Player : PlayerBaseClass
{
    public bool hasStartedGame = false;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        hasStartedGame = false;
    }

    protected override void Update()
    {
        base.Update();
        HandleStartGame();
    }

    protected override void MovePlayer()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        Vector2 vertical = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            vertical = Vector2.up;
        if (Input.GetKey(KeyCode.S))
            vertical = Vector2.down;

        rb.linearVelocity = vertical * moveSpeed;
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); 
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.x = transform.position.x; 
            touchPosition.z = 0f; 

            rb.MovePosition(Vector2.Lerp(transform.position, touchPosition, moveSpeed * Time.deltaTime));
        }
#endif
    }

    private void HandleStartGame()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!hasStartedGame && Input.anyKeyDown)
        {
            StartGameOnce();
        }
#endif

#if UNITY_ANDROID
        if (!hasStartedGame && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartGameOnce();
        }
#endif
    }

    private void StartGameOnce()
    {
        hasStartedGame = true;
        gameManager.isgameStarted = true;
        gameManager.StartGameAction?.Invoke();
    }
}
