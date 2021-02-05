using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float verticalSpeed = 2f;
    
    private float pitch = 0f;
    private float yaw = 0f;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    } // END Start


    private void LookLeftOrRight()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    } // END LookLeftOrRight


    void Update()
    {
        LookLeftOrRight();
    } // END Update

} // END LookWithMouse
