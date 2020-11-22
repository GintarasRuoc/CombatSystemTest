using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    CorePlayer core;

    string mFrontBack = "Vertical";
    string mLeftRight = "Horizontal";
    string mRunning = "Shift";
    string jump = "Jump";
    string attack = "Attack";
    string block = "Block";
    string attackStrong = "AttackStrong";
    string attackMultiple = "AttackMultiple";
    string attackStun = "AttackStun";

    bool isLastAnimBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        core = GetComponent<CorePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(core.checkIfDead())
        {
            anim.Play("death");
        }
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("run") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("block")) && core.checkIfStunned() == false)
        {
            if (Input.GetButton(block))
            {
                anim.Play("block");
                checkBlocking(true);
            }
            else
            {
                checkBlocking(false);
                if ((Input.GetButton(mFrontBack) || Input.GetButton(mLeftRight)) && Input.GetButtonDown(jump) && core.canDodge())
                {
                    if (Input.GetAxisRaw(mFrontBack) > 0)
                    {
                        anim.Play("dodgeF");
                    }
                    else if (Input.GetAxisRaw(mFrontBack) < 0)
                    {
                        anim.Play("dodgeB");
                    }
                    else if (Input.GetAxisRaw(mLeftRight) < 0)
                    {
                        anim.Play("dodgeL");
                    }
                    else if (Input.GetAxisRaw(mLeftRight) > 0)
                    {
                        anim.Play("dodgeR");
                    }
                }
                else if ((Input.GetButton(mFrontBack) || Input.GetButton(mLeftRight)) && Input.GetButton(mRunning))
                {
                    anim.Play("run");
                    core.PerformMovement();
                }
                else if (Input.GetButton(mFrontBack) || Input.GetButton(mLeftRight))
                {
                    anim.Play("walk");
                    core.PerformMovement();
                }
                else if (Input.GetButtonDown(attack))
                {
                    anim.Play("attack");
                }
                else if (Input.GetButtonDown(attackStrong) && core.canStrongAttack())
                {
                    anim.Play("attackStrong");
                }
                else if (Input.GetButtonDown(attackMultiple) && core.canMultipleAttack())
                {
                    anim.Play("attackMultiple");
                }
                else if (Input.GetButtonDown(attackStun) && core.canStunAttack())
                {
                    anim.Play("attackStun");
                }
                else
                {
                    anim.Play("idle");
                }
            }
        }
        if(core.checkIfStunned() == true)
        {
            checkBlocking(false);
            anim.Play("idle");
        }
        
    }

    private void checkBlocking(bool block)
    {
        if(block == false && isLastAnimBlock == true)
        {
            isLastAnimBlock = false;
            core.changeBlocking(false);
        }
        if(block == true && isLastAnimBlock == false)
        {
            isLastAnimBlock = true;
            core.changeBlocking(true);
        }
    }
}
