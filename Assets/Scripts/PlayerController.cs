using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

namespace PlayerStuff
{
    public class PlayerController : MonoBehaviour
    {
        Vector2 playerInputVector;
        Rigidbody2D rb2d;

        public GameObject drill;

        [SerializeField] Animator animation;

        public float moveSpeed = 2f;
        private CustomInput input;

        //Storing move direction and checking if its flipped
        MoveDirection previousMoveDirection;
        MoveDirection inputMoveDirection;
        bool isFlippedRight = true;
        bool isFlippedUp = false;

        private void Awake()
        {
            inputMoveDirection = MoveDirection.None;
            input = new CustomInput();
            drill = transform.GetChild(0).gameObject;
        }


        public void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CalculateMoveDirection();
            Move();
        }

        //Enabling the controls
        private void OnEnable()
        {
            input.Enable();
            input.Player.Move.performed += OnMovementPreformed;
            input.Player.Move.canceled += OnMovementCancelled;
        }

        //Disabling the controls
        private void OnDisable()
        {
            input.Player.Move.performed -= OnMovementPreformed;
            input.Player.Move.canceled -= OnMovementCancelled;
            input.Disable();
        }

        //Getting current player input vector (NOT velocity, just input)
        private void OnMovementPreformed(InputAction.CallbackContext context)
        {
            playerInputVector = context.ReadValue<Vector2>();
        }

        //Reset player input vector to zero (0,0)
        private void OnMovementCancelled(InputAction.CallbackContext context)
        {
            playerInputVector = Vector2.zero;
        }

        //Calculating the move direction based on player input (4 directions and none)
        private void CalculateMoveDirection()
        {
            previousMoveDirection = inputMoveDirection;
            float threshold = 0.1f;
            if (playerInputVector.x >= 1 - threshold)
            {
                inputMoveDirection = MoveDirection.Right;
            }
            else if (playerInputVector.x <= -1 + threshold)
            {
                inputMoveDirection = MoveDirection.Left;
            }
            else if (playerInputVector.y >= 1 - threshold)
            {
                inputMoveDirection = MoveDirection.Up;
            }
            else if (playerInputVector.y <= -1 + threshold)
            {
                inputMoveDirection = MoveDirection.Down;
            }
            else
            {
                inputMoveDirection = MoveDirection.None;
            }

        }


        //Move player according to inputMoveDirection 
        private void Move()
        {
            Vector2 playerSourceVelocity = Vector2.zero;

            switch (inputMoveDirection)
            {
                case MoveDirection.Left:
                    playerSourceVelocity = Vector2.left;
                    animation.SetBool("MakeSpriteLeftRight", true);
                    animation.SetBool("MakeSpriteUpDown", false);
                    break;
                case MoveDirection.Right:
                    playerSourceVelocity = Vector2.right;
                    animation.SetBool("MakeSpriteLeftRight", true);
                    animation.SetBool("MakeSpriteUpDown", false);
                    break;
                case MoveDirection.Up:
                    animation.SetBool("MakeSpriteLeftRight", false);
                    animation.SetBool("MakeSpriteUpDown", true);
                    playerSourceVelocity = Vector2.up;
                    break;
                case MoveDirection.Down:
                    animation.SetBool("MakeSpriteLeftRight", false);
                    animation.SetBool("MakeSpriteUpDown", true);
                    playerSourceVelocity = Vector2.down;
                    break;
                default:
                    break;
            }
            flipSprite();

            //Rounding Formula
            Vector2 half = new Vector2(0.5f, 0.5f);
            Vector2 deltaToGridCenter = RoundVector(rb2d.position - half) + half - rb2d.position;
            rb2d.velocity = (deltaToGridCenter * 4 + playerSourceVelocity * 4) * moveSpeed;
        }

        //Flipping Sprite according to move direction
        public void flipSprite()
        {
            Vector3 currentScale = transform.localScale;

            if (inputMoveDirection == MoveDirection.Right && (!isFlippedRight || isFlippedUp))
            {
                isFlippedRight = true;
                isFlippedUp = false;
            }
            else if (inputMoveDirection == MoveDirection.Left && (isFlippedRight || isFlippedUp))
            {
                isFlippedRight = false;
                isFlippedUp = false;
            }
            else if (inputMoveDirection == MoveDirection.Up && !isFlippedUp)
            {
                isFlippedUp = true;
                isFlippedRight = true;
            }
            else if (inputMoveDirection == MoveDirection.Down && isFlippedUp)
            {
                isFlippedUp = false;
                isFlippedRight = true;
            }

            currentScale = new Vector3(isFlippedRight ? 1 : -1, isFlippedUp ? -1 : 1, 1);
            //drillRotation = new Quaternion(1, 1, isFlippedUp ? -90 : 0, 1);
            gameObject.transform.localScale = currentScale;
            drill.gameObject.transform.eulerAngles = new Vector3(0, 0, calculateRotation());

        }
        //calculate rotation to rotate drill game object collider
        float calculateRotation()
        {
            if (inputMoveDirection == MoveDirection.Up)
            {
                return 90;
            }
            else if (inputMoveDirection == MoveDirection.Down)
            {
                return -90;
            }
            else if (inputMoveDirection == MoveDirection.Left && !isFlippedUp)
            {
                return 0;
            }
            else if (inputMoveDirection == MoveDirection.Right &&!isFlippedUp)
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }

        //Round the vector
        static Vector2 RoundVector(Vector2 vec)
        {
            return new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
        }
    }

}
