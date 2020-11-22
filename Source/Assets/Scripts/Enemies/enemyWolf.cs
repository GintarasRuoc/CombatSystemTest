using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyWolf : MonoBehaviour
{
    public int hpMax = 100;
    public int hp;
    public float speed = 3.5f;
    public int armor = 10;
    public int moneyDropAmount = 15;
    public int dmg1 = 15;
    public int dmg2 = 25;
    public int dmg3 = 40;
    public int xpWorth = 8;
    public LayerMask mask;

    public float attackRadius = 5f;
    public float time;
    private float stunnedTime = 0f;

    private string nextAttack = "attack1";
    private int nextDmg;

    Transform target;
    NavMeshAgent agent;
    Animator anim;
    CorePlayer core;
    enemyHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.Instance.player.transform;
        core = target.GetComponent<CorePlayer>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = speed;
        hp = hpMax;
        nextDmg = dmg1;
        healthBar = GetComponentInChildren<enemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (core == null)
            return;
        if (stunnedTime <= 0f)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
            {
                if (distance <= attackRadius)
                {
                    agent.SetDestination(transform.position);
                    anim.Play(nextAttack);
                }
                else
                {
                    agent.SetDestination(target.position);
                    anim.Play("run");
                }
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            anim.Play("idle");
        }
        if (stunnedTime > 0f)
            stunnedTime -= Time.deltaTime;
        if (time > 0f)
            time -= Time.deltaTime;
    }

    public void dealDmg(int type)
    {
        // type: 1 - attack1 , 2 - attack2, 3 - attack3
        Collider[] player = Physics.OverlapSphere(transform.position, attackRadius, mask);
        if (player.Length > 0)
        {
            core.takeDmg(nextDmg, 0f);
            if(type == 1)
            {
                nextAttack = "attack2";
                nextDmg = dmg2;
            }
            if(type == 2)
            {
                nextAttack = "attack3";
                nextDmg = dmg3;
            }
            else
            {
                nextAttack = "attack1";
                nextDmg = dmg1;
            }
        }
    }

    public void takeDmg(int _dmg, float stun)
    {
        hp -= (_dmg - armor);
        float currentHealthPct = (float)hp / (float)hpMax;
        healthBar.changeFill(currentHealthPct);
        if (hp <= 0)
        {
            anim.Play("death");
        }
        else if (stun > 0f)
        {
            stunnedTime = stun;
        }

    }

    public void onDeath()
    {
        core.gainLoseMoney(moneyDropAmount);
        core.gainXp(xpWorth);
        Destroy(gameObject);
    }

    public void clearCore()
    {
        core = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
