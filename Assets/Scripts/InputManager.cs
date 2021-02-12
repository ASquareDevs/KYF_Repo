using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variables

    [Header("Movement Control Options")]
    public float movementSpeed = 20f;
    public float sprintSpeed = 5f;
    public float lookSpeed;
    private bool isSprinting = false;
    private bool isMovingForward = false;

    [Header("Sprint Config and Components")]
    public KeyCode SprintKey;
    public string sprintControllerInput;

    [Header("Camera Toggle Config and Components")]
    public KeyCode CameraToggleKey;
    public string cameraToggleControllerInput;
    private bool isFirstPerson = true;

    [Header("References")]
    public GameObject playerHead;
    public GameObject thirdPersonCamera;
    public PlayerStats playerStats;
    public CharacterController characterController;

    [Header("X rotation Clamps")]
    public float maxVertacleRotation;
    public float minVertacleRotation;

    [Header("Gravity and Grounding")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    private Vector3 playerVelocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;
    

    private float currentAngleX = 0; //Current X angle of Camera
    private float currentAngleY = 0; //Current Y angle of whole player

    #endregion


    #region Monobehaviors 

    private void Start()
    {
        Init();
    } // END Start


    private void Update()
    {
        Sprint();
        PlayerMovement();
        PlayerLooking();
        ToggleCamera();
        ApplyGravity();
        CheckIfGrounded();
    } // END Update


    private void FixedUpdate()
    {
        
    } // END FixedUpdate

    #endregion


    #region Methods

    /// <summary>
    /// Initilization of the InputManager
    /// </summary>
    public void Init()
    {
        GetComponents();

        Cursor.lockState = CursorLockMode.Locked; //lock the mouse for easier testing
        currentAngleX = 0;

        if (movementSpeed == 0)
        {
            movementSpeed = 20;
        }
        if (lookSpeed == 0)
        {
            lookSpeed = 2;
        }
    } // END Init


    private void GetComponents()
    {
        playerStats = this.GetComponent<PlayerStats>();
        characterController = this.GetComponent<CharacterController>();
    } // END GetComponents


    /// <summary>
    /// Player Movement & Rotation Fuctionality
    /// Takes our right transform and multiplies by 1...-1
    /// Same for forward transform
    /// </summary>
    private void PlayerMovement()
    {
        // Are we moving positive or negative on the Axis?
        float _movementX = Input.GetAxis("Horizontal");
        float _movementZ = Input.GetAxis("Vertical");

        // Takes our X transform and multiplies by _movementX to get our Left/Right strafe 
        // Then adds our forward transform multiplied by our depth(Z)
        Vector3 _playerMovement = transform.right * _movementX + transform.forward * _movementZ;
        characterController.Move(_playerMovement * (movementSpeed + 10) * Time.deltaTime);

        if (isSprinting && _movementZ > 0 || isSprinting && _movementZ < 0)
        {
            characterController.Move(_playerMovement * (movementSpeed + sprintSpeed) * Time.deltaTime);
            isMovingForward = true;
        }
        else
        {
            isMovingForward = false;
        }

        //Vector3 _playerMovment = new Vector3(_movementX, 0, _movementZ);
        //rigiBody.AddRelativeForce(_playerMovment);

    } // END PlayerMovement
    private void ApplyGravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    } // END ApplyGravity

    /// <summary>
    /// Checks to see if we are colliding on a Walkable Layer
    /// </summary>
    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    } // END CheckIfGrounded
    private void PlayerLooking()
    {
        //Mouse rotation
        if (Input.GetAxis("Mouse Y") != 0|| Input.GetAxis("Mouse X") != 0)
        {
            currentAngleX -= Input.GetAxis("Mouse Y") * lookSpeed;
            currentAngleX = Mathf.Clamp(currentAngleX, -90, 90);
            currentAngleY += Input.GetAxis("Mouse X") * lookSpeed;
        }
        //Gamepad rotation
        //Uses custom inputs Pitch and Roll which will need to be set in inputmanager
        else if (Input.GetAxis("Pitch") != 0 || Input.GetAxis("Roll") != 0)
        {
            currentAngleX -= Input.GetAxis("Pitch") * lookSpeed;
            currentAngleX = Mathf.Clamp(currentAngleX, -90, 90);
            currentAngleY += Input.GetAxis("Roll") * lookSpeed;
        }
        //playerHead.transform.localRotation = Quaternion.Euler(currentAngleX, this.transform.rotation.y, 0);
        //this.transform.rotation = Quaternion.Euler(0, currentAngleY, 0);
        transform.eulerAngles = new Vector3(currentAngleX, currentAngleY, 0);

    } // END PlayerLooking

    
    private void Sprint()
    {
        if (Input.GetKey(SprintKey) && playerStats.staminaCurrent > 0 || Input.GetButton(sprintControllerInput) && playerStats.staminaCurrent > 0)
        {
            isSprinting = true;
            if (isMovingForward)
            {
                playerStats.staminaCurrent -= playerStats.staminaDrainOverTime * Time.deltaTime;
            }
        }
        else
        {
            isSprinting = false;
            Invoke("RegainStamina", playerStats.staminaCooldownValue);
        }
    } // END Sprint

    
    private void RegainStamina()
    {
        if (!isSprinting)
        {
            if (playerStats.staminaCurrent < playerStats.staminaMax)
            {
                playerStats.staminaCurrent += (playerStats.staminaDrainOverTime * 2) * Time.deltaTime;
            }
            else if (playerStats.staminaCurrent > playerStats.staminaMax)
            {
                playerStats.staminaCurrent = playerStats.staminaMax;
            }
        }
    } // END RegainStamina
    

    /// <summary>
    /// Intakes a keypress/buttonpress to switch perspectives
    /// </summary>
    private void ToggleCamera()
    {
        //Set up the first submit in the imputmanager to desired button on gampad
        if (Input.GetKeyDown(CameraToggleKey) || Input.GetButtonDown(cameraToggleControllerInput))
        {
            if (isFirstPerson)
            {
                thirdPersonCamera.SetActive(true);
                isFirstPerson = false;
            }
            else
            {
                thirdPersonCamera.SetActive(false);
                isFirstPerson = true;
            }
        }
    } // END ToggleCamera

    #endregion

} // END InputManager
