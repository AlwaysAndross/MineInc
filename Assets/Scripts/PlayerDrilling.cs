using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStuff;
using Unity.VisualScripting;

public class PlayerDrilling : MonoBehaviour
{
    [SerializeField] BoxCollider2D bc2dLR;
    [SerializeField] BoxCollider2D bc2dUD;
    [SerializeField] PlayerController playerSpeed;
    public Vector2 moveVectorA;
    string direction = "Up";
    GameObject Player;
    GameObject blockBeingDug;

    private bool blockDug = false;
    float countdownTimer = 1.0f;
    bool ifDug = false;
    bool Updown = true;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        PlayerController playerSpeed = Player.GetComponent<PlayerController>();
        Vector2 moveVectorA = playerSpeed.moveVector;
    }

    void Update()
    {

        moveVectorA = playerSpeed.moveVector;

        if (moveVectorA.y > 0 || moveVectorA.y < 0)
        {
            Updown = true;
        }

        else if (moveVectorA.x > 0 || moveVectorA.x < 0)
        {
            Updown = false;
        }

        if (direction == "Up" && moveVectorA.y > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                Debug.Log(blockBeingDug);
                ifDug = true;
                Destroy(blockBeingDug);
                countdownTimer = 1f;
            }

        }
        else if (direction == "Up")
        {
            blockDug = false;
            countdownTimer = 1f;
        }

        if (direction == "Down" && moveVectorA.y < 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                Debug.Log(blockBeingDug);
                ifDug = true;
                Destroy(blockBeingDug);
                countdownTimer = 1f;
            }

        }
        else if (direction == "Down")
        {
            blockDug = false;
            countdownTimer = 1f;
        }

        if (direction == "Right" && moveVectorA.x > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                Debug.Log(blockBeingDug);
                ifDug = true;
                Destroy(blockBeingDug);
                countdownTimer = 1f;
            }

        }
        else if (direction == "Right")
        {
            blockDug = false;
            countdownTimer = 1f;
        }

        if (direction == "Left" && moveVectorA.x < 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                Debug.Log(blockBeingDug);
                ifDug = true;
                Destroy(blockBeingDug);
                countdownTimer = 1f;
            }

        }
        else if (direction == "Left")
        {
            blockDug = false;
            countdownTimer = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (Updown && moveVectorA.y > 0)
        {
            direction = "Up";
            blockBeingDug = other.GameObject();
        }

        if (Updown && moveVectorA.y < 0)
        {
            direction = "Down";
            blockBeingDug = other.GameObject();
        }

        if (!Updown && moveVectorA.x > 0)
        {
            direction = "Right";
            blockBeingDug = other.GameObject();
        }

        if (!Updown && moveVectorA.x < 0)
        {
            direction = "Left";
            blockBeingDug = other.GameObject();
        }


    }


}
