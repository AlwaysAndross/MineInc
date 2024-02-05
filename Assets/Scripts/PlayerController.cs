using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PlayerStuff
{
    public class PlayerController : MonoBehaviour
    {
        private CustomInput input = null;
        public Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null;
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] Animator animator;
        bool facingRight = true;

        private void Awake()
        {
            input = new CustomInput();
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            input.Enable();
            input.Player.Move.performed += OnMovementPreformed;
            input.Player.Move.canceled += OnMovementCancelled;
        }

        private void OnDisable()
        {
            input.Disable();
            input.Player.Move.performed -= OnMovementPreformed;
            input.Player.Move.canceled -= OnMovementCancelled;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveVector * moveSpeed;
            float movingY = rb.velocity.y;
            float movingX = rb.velocity.x;

            if (movingY > 0)
            {
                Vector3 currentScaleV = transform.localScale;
                currentScaleV.y = -1;
                gameObject.transform.localScale = currentScaleV;
                animator.SetBool("IsOnY", true);

            }
            if (movingY < 0)
            {
                correctYSprite();
                animator.SetBool("IsOnY", true);
            }

            if (movingX < 0 && facingRight)
            {
                correctYSprite();
                animator.SetBool("IsOnY", false);
                flipSpriteLeftRight();
                //Debug.Log("You pressed left");
                facingRight = false;
            }
            else if (movingX < 0)
            {
                correctYSprite();
                animator.SetBool("IsOnY", false);
            }

            if (movingX > 0 && !facingRight)
            {
                correctYSprite();
                animator.SetBool("IsOnY", false);
                flipSpriteLeftRight();
                //Debug.Log("You pressed right");
            }
            else if (movingX > 0)
            {
                correctYSprite();
                animator.SetBool("IsOnY", false);
            }

        }

        private void OnMovementPreformed(InputAction.CallbackContext value)
        {
            moveVector = value.ReadValue<Vector2>();
        }

        private void OnMovementCancelled(InputAction.CallbackContext value)
        {
            moveVector = Vector2.zero;
        }

        void flipSpriteLeftRight()
        {
            facingRight = !facingRight;

            Vector3 currentScaleH = transform.localScale;
            currentScaleH.x *= -1;

            gameObject.transform.localScale = currentScaleH;
        }

        void correctYSprite()
        {
            Vector3 currentScaleV = transform.localScale;
            currentScaleV.y = 1;
            gameObject.transform.localScale = currentScaleV;
        }

    }
}