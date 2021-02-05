using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    [Header("Singleton")]
    public static SpawnManager main = null;

    /*
    [Header("Prefabs")]
    public Enemy enemyPrefab;
    */
    [Header("Spawns")]
    public Transform enemyTrapSpawn;


    private void Start()
    {
        InitSingleton();
        
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

    /*
    /// <summary>
    /// Rand ID 0 = Enemy.prefab
    /// Rand ID 1 = other.prefabs
    /// </summary>
    /// <param name="_data"></param>
    /// <param name="_randSetID"></param>
    public void SpawnPrefab(IData _data = null, int _randSetID = -1)
    {
        //Spawn Explicit Data
        if (_data != null)
        {
            if (_data.EnemyData() != null)
            {
                EnemyHandler _enemyToSpawn = Instantiate(enemyPrefab, enemyTrapSpawn.transform.position, enemyPrefab.transform.rotation).GetComponent<EnemyHandler>();
                _enemyToSpawn.Init(_data);
            }
        }
        //Spawn Random Data via Set ID
        else
        {
            if (_randSetID == 0)
            {
                EnemyHandler _enemyToSpawn = Instantiate(enemyPrefab, enemyTrapSpawn.transform.position, enemyPrefab.transform.rotation).GetComponent<EnemyHandler>();
                _enemyToSpawn.Init();
            }
        }
    }
    */

} // END SpawnManager
