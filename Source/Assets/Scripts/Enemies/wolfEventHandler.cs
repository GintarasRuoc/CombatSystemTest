using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfEventHandler : MonoBehaviour
{
    public GameObject wolfObj;
    private enemyWolf wolf;
    
    void Start()
    {
        wolf = wolfObj.GetComponent<enemyWolf>();
    }

    private void attack1()
    {
        wolf.dealDmg(1);
    }

    private void attack2()
    {
        wolf.dealDmg(2);
    }

    private void attack3()
    {
        wolf.dealDmg(3);
    }

    private void death()
    {
        wolf.onDeath();
    }
}
