using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyOgre : MonoBehaviour
{
    public int hpMax = 150;
    public int hp;
    public float speed = 3f;
    public int armor = 20;
    public int moneyDropAmount = 20;
    public int dmg = 30;
    public int xpWorth = 20;
    public float stunDuration = 1f;
    public float timeBetweenSkills = 10f;
    public LayerMask mask;

    public float attackRadius = 5f;
    public float time;
    private float stunnedTime = 0f;

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
        time = timeBetweenSkills;
        agent.speed = speed;
        hp = hpMax;
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
                    if (time <= 0f)
                    {
                        anim.Play("attackStun");
                        time = timeBetweenSkills;
                    }
                    else
                    {
                        anim.Play("attackBasic");
                    }
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
        // type: 1 - basic attack, 2 - stun attack
        Collider[] player = Physics.OverlapSphere(transform.position, attackRadius, mask);
        if (player.Length > 0)
        {
            if(type == 2)
                core.takeDmg(dmg, stunDuration);
            else {
                core.takeDmg(dmg, 0f);
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
