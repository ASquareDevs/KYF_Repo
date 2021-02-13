using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Equipable))]
[RequireComponent(typeof(Spawner))]

public class Spawner : MonoBehaviour
{
    public GameObject equipable;
    public Transform spawnPoint;


    void Start()
    {
        Instantiate(equipable, spawnPoint);

    } // END Start

} // END Spawner
