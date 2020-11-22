using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventHandler : MonoBehaviour
{
    CorePlayer core;

    private void Start()
    {
        core = PlayerManager.Instance.player.GetComponent<CorePlayer>();
    }

    private void basicAttackEvent()
    {
        core.PerformAttack(1);
    }

    private void attackMultipleEvent()
    {
        core.PerformAttack(2);
    }

    private void attackStrongEvent()
    {
        core.PerformAttack(3);
    }

    private void attackStunEvent()
    {
        core.PerformAttack(4);
    }

    private void dodgeF()
    {
        core.handleDodge(1);
    }

    private void dodgeB()
    {
        core.handleDodge(2);
    }

    private void dodgeL()
    {
        core.handleDodge(3);
    }

    private void dodgeR()
    {
        core.handleDodge(4);
    }

    private void death()
    {
        core.onDeath();
    }
}
