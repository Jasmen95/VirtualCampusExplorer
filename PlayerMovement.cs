
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    //[SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    //[SerializeField] private float _moveSpeed;
    //[SerializeField] private float _rotateSpeed;

    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] public float playerSpeed = 2.0f;

    public bool walking = false;
    public bool running = false;
    public bool idle = true;

    staminaBar stam;
    private float runTakSt = 0.2f;
    private float regen = 0.15f;
    private PlayerControls playerInput;

    private void Awake()
    {
        playerInput = new PlayerControls();
        controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    // Start is called before the first frame update


    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 3f;

    private Transform cameraMain;
    private Transform child;

    private void Start()
    {
        stam = GetComponent<staminaBar>();

        cameraMain = Camera.main.transform;

        child = transform.GetChild(1).transform;
    }



    private void FixedUpdate()
    {
        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        bool isRunning = playerInput.Player.Run.IsPressed();
        bool isJumping = playerInput.Player.Jump.IsPressed();

         // Check for left shift key press to set isRunning to true
        if (Keyboard.current.spaceKey.isPressed && stam.currentStamina > 1f)
        {
            isRunning = true;
        }

        if (isRunning)
        {
            if (movementInput != Vector2.zero) 
            { 

            Quaternion targetRotation = Quaternion.LookRotation(move);
            _rigidbody.MoveRotation(targetRotation);

            _animator.SetTrigger("Run");
            _animator.ResetTrigger("Walk");
            _animator.ResetTrigger("Idle");
            walking = false;
            running = true;
            idle = false;

            //deplete stamina
            stam.TakeStamina(runTakSt);

                if(movementInput == Vector2.zero)
                {
                    _animator.ResetTrigger("Run");
                    _animator.SetTrigger("Idle");
                    running = false;
                    idle = true;
                }
            }
        }
        else if (movementInput != Vector2.zero)
        {
            

            _animator.SetTrigger("Walk");
            _animator.ResetTrigger("Idle");
            _animator.ResetTrigger("Run");
            walking = true;
            running = false;
            idle = false;

             Quaternion targetRotation = Quaternion.LookRotation(move);
            _rigidbody.MoveRotation(targetRotation);


                if (isJumping)
            {
                _animator.SetTrigger("Jump");
                _animator.ResetTrigger("Walk");
                _animator.ResetTrigger("Run");
            }

        }
        else
        {
            _animator.SetTrigger("Idle");
            _animator.ResetTrigger("Walk");
            _animator.ResetTrigger("Run");
            _animator.ResetTrigger("Jump");
            walking = false;
            running = false;
            idle = true;

            stam.RegenStamina(regen);
        }



    }
}
