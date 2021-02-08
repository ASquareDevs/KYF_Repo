using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour, IEquipable
{
    #region Variables

    public GameObject player;
    [HideInInspector]
    public Transform OriginalParent;

    #endregion


    #region Monobehaviors

    void Start()
    {
        Init();
    }

    void Update()
    {
        CheckIfDropped();
    }

    #endregion


    #region Methods

    /// <summary>
    /// Initilization of Class
    /// </summary>
    void Init()
    {
        OriginalParent = this.GetComponentInParent<Transform>();
    }

    public void OnEquip()
    {
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
            Debug.Log("Object dropped reassigning to original Parent!");
            this.transform.SetParent(OriginalParent);
        }
    }

    #endregion

} // END Equipable
