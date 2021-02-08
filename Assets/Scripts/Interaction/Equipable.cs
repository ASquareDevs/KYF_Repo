using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour, IEquipable
{
    public GameObject player;
    

    public void OnEquip()
    {
        this.GetComponentInChildren<Rigidbody>().isKinematic = true;
        player.gameObject.GetComponent<InventoryManager>().PickUpEquipable();
        Debug.Log("Interacted with an IEquipable.  An Item was equipped");
    } // END OnEquip

    
    public void OnInteracted()
    {
        OnEquip();
    } // END OnInteracted
    
} // END Equipable
