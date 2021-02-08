using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionController : MonoBehaviour
{
    [Header("Interaction")]
    public IInteractable targetInteractable; //Reference to currently detected interactable
    public IEquipable targetEquipable;
    
    [Header("Inventory Reference")]
    public InventoryManager inventory;

    [Header("Object to Equip")]
    public GameObject objectToPickUp;


    private void Update()
    {
        ReadForInteractable();
    } // END Update

    
    /// <summary>
    /// This will check if object we are casting to is of IInteractable
    /// If not, we have not casted to a valid IInteractable object
    /// </summary>
    public void ReadForInteractable()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            //Check if Interactable
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f))
            {
                if (hit.transform.gameObject.GetComponent<Equipable>() != null)
                {
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.green, 20f);

                    targetInteractable = hit.transform.root.GetComponent<IInteractable>();

                    //If not Equipable
                    if (hit.transform.root.GetComponent<IEquipable>() == null)
                    {
                        targetInteractable.OnInteracted();
                    }
                    //If Equipable
                    else
                    {
                        targetEquipable = hit.transform.root.gameObject.GetComponent<IEquipable>();
                        objectToPickUp = hit.transform.gameObject;
                        targetEquipable.OnInteracted();
                    }
                }
            }
            //Did not hit anything Interactable
            else
            {
                if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f))
                {
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000, Color.white, 20f);
                    return;
                }
            }
        }
    } // END ReadForInteractable

} //END InteractionController
