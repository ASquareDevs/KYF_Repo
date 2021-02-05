using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Variables
    //Player's Rigid Body
    private Rigidbody rigiBody;

    [Header("Movement Control Options")]
    public float movementSpeed = 20;
    public float lookSpeed;
    private bool isSprinting = false;
    private bool isMovingForward = false;

    [Header("Inputs Config")]
    public KeyCode sprintKey;
    public string sprintControllerInput;

    [Header("Component hookups")]
    public GameObject playerHead;
    public CharacterController characterController;


    [Header("X rotation Clamps")]
    public float maxVertacleRotation;
    public float minVertacleRotation;

    //Current X angle of Camera
    private float currentAngleX = 0;
    //Current Y angle of whole player
    private float currentAngleY = 0;

    #endregion


    #region Monobehaviors 

    void Start()
    {
        Init();
    }
    void Update()
    {
        Sprint();
    }
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerLooking();
    }

    #endregion


    #region Methods

    /// <summary>
    /// Initilization of Character Controller
    /// </summary>
    void Init()
    {
        rigiBody = this.GetComponent<Rigidbody>();
        //lock the mouse for easier testing
        Cursor.lockState = CursorLockMode.Locked;
        currentAngleX = 0;
        if (movementSpeed == 0)
        {
            movementSpeed = 20;
        }
        if (lookSpeed ==0)
        {
            lookSpeed = 2;
        }
        characterController = this.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Player Movement & Rotation Fuctionality
    /// </summary>
    void PlayerMovement()
    {
        //Relative player movement based on  X & Y axis input * movementSpeed
        float movementX = Input.GetAxis("Horizontal") * movementSpeed;
        float movementZ = Input.GetAxis("Vertical") * movementSpeed;
        if (isSprinting && movementZ >0)
        {
            movementZ = movementZ * 2;
            isMovingForward = true;
        }
        else
        {
            isMovingForward = false;
        }
        Vector3 playerMovment = new Vector3(movementX, 0, movementZ);
        rigiBody.AddRelativeForce(playerMovment);  
    }

    public void PlayerLooking()
    {
        //Mouse rotation
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            currentAngleX -= Input.GetAxis("Mouse Y") * lookSpeed;
            currentAngleX = Mathf.Clamp(currentAngleX, -90, 90);
            //print(currentAngleX);
            currentAngleY -= Input.GetAxis("Mouse X") * lookSpeed;
        }
        else if (Input.GetAxis("Pitch") != 0 || Input.GetAxis("Roll") != 0)
        {
            //Gamepad rotation
            //Uses custom inputs Pitch and Roll which will need to be set in inputmanager
            currentAngleX -= Input.GetAxis("Pitch") * lookSpeed;
            currentAngleX = Mathf.Clamp(currentAngleX, -90, 90);
            currentAngleY -= Input.GetAxis("Roll") * lookSpeed;
        }

        playerHead.transform.localRotation = Quaternion.Euler(currentAngleX, this.transform.rotation.y, 0);
        this.transform.rotation = Quaternion.Euler(0, currentAngleY, 0);
    }

    void Sprint()
    {
        if (Input.GetKey(sprintKey) && characterController.staminaCurrent >0 || Input.GetButton(sprintControllerInput) && characterController.staminaCurrent > 0)
        {
            isSprinting = true;
            if (isMovingForward)
            {
                characterController.staminaCurrent -= characterController.staminaDrainOverTime * Time.deltaTime;
            }
            
        }
        else
        {
            isSprinting = false;
            Invoke("RegainStamina", characterController.staminaCooldownValue);
        }
    }

    void RegainStamina()
    {
        if (!isSprinting)
        {
            if (characterController.staminaCurrent < characterController.staminaMax)
            {
                characterController.staminaCurrent += (characterController.staminaDrainOverTime * 2) * Time.deltaTime;
            }
            else if (characterController.staminaCurrent > characterController.staminaMax)
            {
                characterController.staminaCurrent = characterController.staminaMax;
            }
        }
    }

    #endregion


    #region Corutines
    #endregion
}
