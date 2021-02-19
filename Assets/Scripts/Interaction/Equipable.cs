using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour, IEquipable
{
    #region Variables

    public GameObject player;
    public InventoryManager inventory;
    public bool isInteracted;
    [HideInInspector] public Transform OriginalParent;


    #endregion


    #region Monobehaviors

    void Start()
    {
        //Init();
        inventory = player.GetComponent<InventoryManager>();
        isInteracted = false;
        //SetObjectToPickUp(this.gameObject);
    } // END Start

    void Update()
    {
        if(isInteracted)
        {
            inventory.objectToPickUp = this.gameObject;
        }
    } // END Update


    #endregion


    #region Methods

    /// <summary>
    /// Initilization of Class
    /// </summary>
    void Init()
    {
        
    } // END Init


    public GameObject SetObjectToPickUp(GameObject _objectToPickUp)
    {
        inventory.objectToPickUp = _objectToPickUp;

        GameObject _obj = _objectToPickUp;
        return _obj;
    } // END GetObjectToPickUp


    public void OnEquip()
    {
        isInteracted = true;
        //player.gameObject.GetComponent<InventoryManager>().objectToPickUp = SetObjectToPickUp(this.gameObject);
        player.gameObject.GetComponent<InventoryManager>().PickUpEquipable();
        Debug.Log("Interacted with an IEquipable.  An Item was equipped");

    } // END OnEquip


    public void OnInteracted()
    {
        OnEquip();

    } // END OnInteracted


    void CheckIfDropped()
    {
        //CHECK dosn't work need to find a way to check if not parented,and assign to parent
        if (this.transform.parent == null)
        {
            //Debug.Log("Object dropped reassigning to original Parent!");
            this.transform.SetParent(OriginalParent);
        }
    } // END CheckIfDropped

    #endregion

} // END Equipable
