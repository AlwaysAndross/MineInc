using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector2 playerInput;
        playerInput.x = 0f;
        playerInput.y = 0f;
        transform.localPosition = new Vector2(playerInput.x, playerInput.y);

        
    }
}
