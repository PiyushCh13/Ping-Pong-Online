using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : PlayerBaseClass
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        StartGame();
    }


    protected override void MovePlayer()
    {

#if UNITY_EDITOR || UNITY_STANDALONE

        Vector2 vectical = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            vectical = Vector2.up;

        if (Input.GetKey(KeyCode.S))
            vectical = Vector2.down;

        Vector2 movement = vectical * moveSpeed;
        rb.velocity = movement;

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

    public void StartGame()
    {
        if (!gameManager.isBallMoving && Input.anyKeyDown || Input.touchCount > 0)
        {
            gameManager.isgameStarted = true;
            gameManager.StartGameAction?.Invoke();
        }
    }

}
