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
    public GameObject objectToPickUp = null;
    private bool isInLeftHand = false;
    private bool isInRightHand = false;

    [Header("Inventory Size")]
    [SerializeField] private int _inventorySize = 1;

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
        GetEquipableObject();
    } // END Update

    #endregion


    #region Methods

    private void GetEquipableObject()
    {
        objectToPickUp = PlayerController1.main.GetObjectToPickUp();
    } // END GetEquipableObject


    //Puts the object in the appropriate hand
    public void PickUpEquipable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //If left hand is null (has no children)
            if (leftHand.transform.childCount == 0 || isInLeftHand == false)
            {
                objectToPickUp = PlayerController1.main.GetObjectToPickUp();
                objectToPickUp.transform.SetParent(leftHand.transform);
                objectToPickUp.transform.localPosition = leftHand.transform.localPosition;
                objectToPickUp.SetActive(true);
                isInLeftHand = true;
                Debug.Log("Picked up an Equipable with Left Hand.");
            }
            else
            {
                //- Check if list is full -
                if(leftHandInventory.Count < _inventorySize)
                {
                    //Place current equipped gameObject into inventory - Add to List -
                    GameObject _equippedObject = objectToPickUp;
                    leftHandInventory.Add(_equippedObject);
                    _equippedObject.SetActive(false);
                    //Destroy(objectToPickUp);
                    //null the object so we can pick up another item
                    //_equippedObject = null;
                    
                    //objectToPickUp = null;

                    Debug.Log(leftHandInventory);
                    //Equip newly selected gameObject
                    isInLeftHand = false;
                }
                else
                {
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
                objectToPickUp = PlayerController1.main.GetObjectToPickUp();
                objectToPickUp.transform.SetParent(rightHand.transform);
                objectToPickUp.transform.localPosition = rightHand.transform.localPosition;
                objectToPickUp.SetActive(true);
                isInRightHand = true;
                Debug.Log("Picked up an Equipable with Right Hand.");
            }
            else
            {
                //- Check if list is full -
                if (rightHandInventory.Count < _inventorySize)
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
