using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Spawner : MonoBehaviour
{
    public GameObject equipable;
    public Transform spawnPoint;


    void Start()
    {
        Instantiate(equipable, spawnPoint.transform.position, Quaternion.identity);
        StartCoroutine(ISetInActive());
    } // END Start


    public IEnumerator ISetInActive()
    {
        yield return new WaitForSeconds(2f);
        SetInActive();
        
    }

    void SetInActive()
    {
        this.gameObject.SetActive(false);
    }

} // END Spawner
