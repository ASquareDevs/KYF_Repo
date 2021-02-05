using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Variables

    [Header("Player Statistics")]
    public float staminaMax =100;
    public float staminaDrainOverTime;
    public float staminaCooldownValue =1;
  
    public float staminaCurrent;

    [Header("Camera Toggle Components")]
    public KeyCode cameraToggleKey;
    public string cameraToggleControllerInput;
    private bool isFirstPerson = true;

    [Header("GameObject hookups")]
    public GameObject thirdPersonCamera;
    #endregion


    #region Monobehaviors

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ToggleCammera();
    }

    #endregion


    #region Methods

    void Init()
    {
        if (staminaMax == 0)
        {
            staminaMax = 100;
        }
        staminaCurrent = staminaMax;
        if (staminaDrainOverTime == 0)
        {
            staminaDrainOverTime = 5;
        }
        if (staminaCooldownValue == 0)
        {
            staminaCooldownValue = 1;
        }


    }

    /// <summary>
    /// Intakes a keypress/buttonpress to switch perspectives
    /// </summary>
    public void ToggleCammera()
    {
        //Set up the first submit in the imputmanager to desired button on gampad
        if (Input.GetKeyUp(cameraToggleKey) || Input.GetButtonUp(cameraToggleControllerInput))
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
    }

    /*public GameObject GetObjectToPickUp()
    {
        inventoryManager.objectToPickUp = interactionController.objectToPickUp;
        GameObject obj = inventoryManager.objectToPickUp;
        return obj;
    } // END GetObjectToPickUp*/

    #endregion
}
