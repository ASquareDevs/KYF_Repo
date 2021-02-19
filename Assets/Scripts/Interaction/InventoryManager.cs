using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  The InventoryManager.cs will be attached to the Player.prefab 
 *  and its function is to track the equipping / unequipping of items.
 */
public class InventoryManager : MonoBehaviour
{
    #region Variables

    [Header("Hands and Object to Equip")]
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject objectToPickUp;
    public GameObject equippedObject;
    private bool isInLeftHand = false;
    private bool isInRightHand = false;

    [Header("Inventory Size")]
    [SerializeField] private int inventorySize = 3;

    [Header("Inventories")]
    public List<GameObject> leftHandInventory = new List<GameObject>();
    public List<GameObject> rightHandInventory = new List<GameObject>();

    [Header("Interaction Controller")]
    public InteractionController interactController;

    #endregion

    #region Monobehaviors

    private void Start()
    {
        objectToPickUp = null;
    } // END Start


    private void Update()
    {
        PickUpEquipable();
    } // END Update

    #endregion


    #region Methods


    private void EquipLeftHand()
    {
        //objectToPickUp.transform.SetParent(leftHand.transform);
        //objectToPickUp.transform.localPosition = leftHand.transform.localPosition;
        objectToPickUp.SetActive(true);
        //Debug.Log("Picked up an Equipable with Left Hand.");
    }


    private void EquipRightHand()
    {
        objectToPickUp.transform.SetParent(rightHand.transform);
        objectToPickUp.transform.localPosition = rightHand.transform.localPosition;
        objectToPickUp.SetActive(true);
        isInLeftHand = true;
        Debug.Log("Picked up an Equipable with Left Hand.");
    }


    private void UnEquipLeftHand()
    {

    } // END UnEquipLeftHand


    //Puts the object in the appropriate hand
    public void PickUpEquipable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //If left hand is null (has no children) Equip Weapon
            if (leftHand.transform.childCount == 0)
            {
                EquipLeftHand();
            }
            else //If hand is not null, we have something equipped, add it to inventory
            {
                //- Check if list is full -
                if(leftHandInventory.Count < inventorySize)
                {
                    //Place current equipped gameObject into inventory - Add to List -
                    equippedObject = objectToPickUp;
                    leftHandInventory.Add(equippedObject);
                    equippedObject.SetActive(false);
                    
                    //Equip newly selected gameObject
                    isInLeftHand = false;
                    Instantiate(objectToPickUp, leftHand.transform);
                    equippedObject = objectToPickUp;

                    //null the object so we can pick up another item
                    //equippedObject = null;
                    //objectToPickUp = null;
                }
                else
                {
                    /*
                    if (leftHand.transform.GetChild(0).gameObject.activeSelf)
                    {
                        leftHand.transform.GetChild(0).gameObject.SetActive(false);
                        leftHand.transform.GetChild(1).gameObject.SetActive(true);
                        Instantiate(leftHand.transform.GetChild(1).gameObject, leftHand.transform);
                    }
                    if (leftHand.transform.GetChild(1).gameObject.activeSelf)
                    {
                        leftHand.transform.GetChild(0).gameObject.SetActive(true);
                        leftHand.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    /*
                    if (leftHandInventory[2].gameObject.activeSelf == true)
                    {
                        leftHandInventory[0].gameObject.SetActive(true);
                        leftHandInventory[1].gameObject.SetActive(false);
                        leftHandInventory[2].gameObject.SetActive(false);
                    }
                    */
                    Debug.Log("Invantory Full. Swaping items.");
                }
                //Else
                // - Prompt to remove an item from inventory - OR - Drop an item from the left hand
                Debug.Log("Cannot pick up.  Adding object to left pocket");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //If left hand is null (has no children)
            if (rightHand.transform.childCount == 0 || isInRightHand == false)
            {
                EquipRightHand();
            }
            else
            {
                //- Check if list is full -
                if (rightHandInventory.Count < inventorySize)
                {
                    //Place current equipped gameObject into inventory - Add to List -
                    GameObject _equippedObject = objectToPickUp;
                    rightHandInventory.Add(_equippedObject);
                    _equippedObject.SetActive(false);
                    //Destroy(objectToPickUp);
                    //null the object so we can pick up another item
                    //_equippedObject = null;

                    //objectToPickUp = null;

                    Debug.Log(rightHandInventory);
                    //Equip newly selected gameObject
                    isInRightHand = false;
                }
                else
                {

                }
                //Else
                // - Prompt to remove an item from inventory - OR - Drop an item from the left hand
                Debug.Log("Cannot pick up.  Adding object to left pocket");
            }

        }
    } // END PickUpEquipable

    #endregion

} // END InventoryManager
