using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ogreEventHandler : MonoBehaviour
{
    public GameObject ogreObj;
    private enemyOgre ogre;

    private void Start()
    {
        ogre = ogreObj.GetComponent<enemyOgre>();
    }

    private void attackBasic()
    {
        ogre.dealDmg(1);
    }

    private void attackStun()
    {
        ogre.dealDmg(2);
    }

    private void death()
    {
        ogre.onDeath();
    }
}
