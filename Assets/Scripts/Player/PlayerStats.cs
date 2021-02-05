using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int speed;
    public int attack;
    public int defense;
    public int incomingDamage;
    public int outgoingDamage; // For modification later.  If we want to scale attack by 1.2 we can cast and multiply

    private void Start()
    {
        Init();
    }


    //Sets Players stats
    private void Init()
    { 
        health = 10;
        attack = 15;
        defense = 10;
        speed = 15;
    } // END Init

} // END PlayerStats
