using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stamina Variables")]
    public float staminaMax = 100;
    public float staminaDrainOverTime;
    public float staminaCooldownValue = 1;
    public float staminaCurrent;

    private void Start()
    {
        Init();

    } // END Start


    //Sets Player stamina
    private void Init()
    {
        if (staminaMax == 0)
        {
            staminaMax = 100;
        }

        staminaCurrent = staminaMax;

        if (staminaDrainOverTime == 0)
        {
            staminaDrainOverTime = 5;
        }
        if (staminaCooldownValue == 0)
        {
            staminaCooldownValue = 1;
        }
    } // END Init

} // END PlayerStats
