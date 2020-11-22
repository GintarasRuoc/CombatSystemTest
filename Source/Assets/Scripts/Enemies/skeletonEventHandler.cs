using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonEventHandler : MonoBehaviour
{
    public GameObject skeletonObj;
    private enemySkeleton skeleton;

    private void Start()
    {
        skeleton = skeletonObj.GetComponent<enemySkeleton>();
    }

    private void attack()
    {
        skeleton.dealDmg();
    }

    private void death()
    {
        skeleton.onDeath();
    }
}
