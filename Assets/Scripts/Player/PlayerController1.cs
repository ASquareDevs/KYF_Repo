using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerController1 main = null;

    //Cache Helper components for reference through the Singleton
    [Header("Player Components")]
    public InteractionController interactionController;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public LookWithMouse lookWithMouse;
    public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        InitSingleton();
        //GetComponents();
    } // END Start


    public void InitSingleton()
    {
        if (main == null)
        {
            main = this;
            DontDestroyOnLoad(main);
        }
        else if (main != this)
        {
            Destroy(this);
        }
    } // END InitSingleton


    private void Update()
    {
        GetObjectToPickUp();
    } // END Update


    public GameObject GetObjectToPickUp()
    {
        inventoryManager.objectToPickUp = interactionController.objectToPickUp;
        GameObject obj = inventoryManager.objectToPickUp;
        return obj;
    } // END GetObjectToPickUp


    private void GetComponents()
    {
        interactionController = this.gameObject.GetComponent<InteractionController>();
        playerStats = this.gameObject.GetComponent<PlayerStats>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        lookWithMouse = this.gameObject.GetComponent<LookWithMouse>();
        inventoryManager = this.gameObject.GetComponent<InventoryManager>();
    } // END GetComponents

} // END PlayerController
