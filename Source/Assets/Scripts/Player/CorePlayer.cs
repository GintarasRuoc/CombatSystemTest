using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlayer : MonoBehaviour
{
    // player stats
    [Header("Health Settings")]
    public int hpMax = 100;
    public int hp = 100;
    public int regenHp = 1;
    public float regenHpCooldown = 5f;
    private float regenHpTimer;

    [Header("Mana Settings")]
    public int manaMax = 100;
    public int mana = 100;
    public int regenMana = 2;
    public float regenManaCooldown = 2f;
    private float regenManaTimer;

    [Header("Stamina Settings")]
    public int staminaMax = 50;
    public int stamina = 50;
    public int regenStamina = 5;
    public float regenStaminaCooldown = 2f;
    private float regenStaminaTimer;

    [Header("Dodge Settings")]
    public float dodgeDistance = 5f;
    public int dodgeCost = 25;

    [Header("Block Settings")]
    private bool blocking = false;
    public int blockCost = 5;

    [Header("Leveling Settings")]
    public int level = 1;
    public int xpForNextLvl = 15;
    public int xp = 0;

    [Header("Armor Settings")]
    public int armor = 5;

    [Header("Damage Settings")]
    public int dmg = 40;

    [Header("Usable points Settings")]
    public int money = 0;
    public int unusedStatPoints = 0;
    public int unusedSkillPoints = 0;

    [Header("Running Settings")]
    public int runningStaminaCost = 10;
    public float runningtimeBetweenCosts = 1f;
    private float runningTimer;
    

    // judėjimas
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 tilt = Vector3.zero;
    
    [SerializeField]
    private Camera cam;

    [Header("Common attack Setting")]
    public Transform attackPoint; // multiple atakos taskas yra zmogaus centras
    public float attackRange = 3f;
    public LayerMask enemyLayers;

    [Header("Multiple attack Settings")]
    public float multipleAttackMultiplier = 1f;
    public int multipleAttackManaCost = 40;
    public float multipleAttackCooldown = 10f;
    private float multipleAttackTimer = 0f;
    public float attackMultipleRange = 5.3f;

    [Header("Strong attack Settings")]
    public float strongAttackMultiplier = 1.4f;
    public int strongAttackManaCost = 20;
    public float strongAttackCooldown = 7f;
    private float strongAttackTimer = 0f;

    [Header("Stun attack Settings")]
    public float stunAttackMultiplier = 1.1f;
    public int stunAttackManaCost = 50;
    public float stunAttackCooldown = 5f;
    private float stunAttackTimer = 0f;
    public float stunDuration = 2f;

    private playerStatsInterface stats;

    public GameObject menu;
    private openMenu men;
    

    // ar uzstunintas ir ar blokuoja
    private float isStunned = 0f;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        regenHpTimer = regenHpCooldown;
        regenManaTimer = regenManaCooldown;
        regenStaminaTimer = regenStaminaCooldown;
        stats = GetComponentInChildren<playerStatsInterface>();
        men = menu.GetComponent<openMenu>();
    }

    private void Update()
    {
        if (checkIfStunned() == false)
        {
            PerformRotation();
        }
        //PerformTilt();
        if (hp < hpMax)
        {
            regenHpTimer -= Time.deltaTime;
            if (regenHpTimer <= 0f)
                GetHp();
        }
        if (mana < manaMax)
        {
            regenManaTimer -= Time.deltaTime;
            if(regenManaTimer <= 0f)
                GetMana();
        }
        if(stamina < staminaMax)
        {
            regenStaminaTimer -= Time.deltaTime;
            if(regenStaminaTimer <= 0f)
                GetStamina();
        }
        if(multipleAttackTimer > 0f)
        {
            multipleAttackTimer -= Time.deltaTime;
            float pct = (float)multipleAttackTimer / (float)multipleAttackCooldown;
            stats.changeSkillTwo(pct);
        }
        if(strongAttackTimer > 0f);
        {
            strongAttackTimer -= Time.deltaTime;
            float pct = (float)strongAttackTimer / (float)strongAttackCooldown;
            stats.changeSkillOne(pct);
        }
        if(stunAttackTimer > 0f)
        {
            stunAttackTimer -= Time.deltaTime;
            float pct = (float)stunAttackTimer / (float)stunAttackCooldown;
            stats.changeSkillThree(pct);
        }
        if(isStunned > 0f)
        {
            isStunned -= Time.deltaTime;
        }
    }
    // atpildo gyvybe
    private void GetHp()
    {
        hp += regenHp;
        if (hp > hpMax)
            hp = hpMax;
        float pct = (float)hp / (float)hpMax;
        stats.changeHp(pct, hp);
        regenHpTimer = 1f;
    }

    // atpildo mana
    private void GetMana()
    {
        mana += regenMana;
        if (mana > manaMax)
            mana = manaMax;
        float pct = (float)mana / (float)manaMax;
        stats.changeMp(pct, mana);
        regenManaTimer = 1f;
    }

    // atpildo stamina
    private void GetStamina()
    {
        stamina += regenStamina;
        if (stamina > staminaMax)
            stamina = staminaMax;
        float pct = (float)stamina / (float)staminaMax;
        stats.changeSt(pct, stamina);
        regenStaminaTimer = 1f;
    }

    // pinigu atimimat ir gavimas
    public void gainLoseMoney(int _money)
    {
        money += _money;
    }

    public void gainXp(int _xp)
    {
        xp += _xp;
        if(xp >= xpForNextLvl)
        {
            level++;
            unusedSkillPoints++;
            unusedStatPoints++;
            xp -= xpForNextLvl;
            xpForNextLvl = Mathf.RoundToInt((float)xpForNextLvl * 1.1f);
            gainXp(0);
        }
        float pct = (float)xp / (float)xpForNextLvl;
        stats.changeXp(pct);
    }

    public bool checkIfDead()
    {
        return hp <= 0f;
    }

    // zalos gavimas ir patikrinimas ar reika stunint
    public void takeDmg(int _dmg, float stun)
    {
        if (blocking == true && stamina >= blockCost && stun == 0f)
        {
            stamina -= blockCost;
            float pct = (float)stamina / (float)staminaMax;
            stats.changeSt(pct, stamina);
            Debug.Log("Blokavo smugi");
        }
        else
        {
            hp -= (_dmg - armor);
            float pct = (float)hp / (float)hpMax;
            stats.changeHp(pct, hp);
            if (stun > 0f)
            {
                Debug.Log("Stunino");
                isStunned = stun;
            }
        }
    }

    public void onDeath()
    {
        
    }

    public bool checkIfStunned()
    {
        return isStunned > 0f;
    }

    // Gauna judėjimo info
    public void Movement(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Gauna horizontalaus pasukimo info
    public void Rotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gauna vertikalaus pavertimo info
    public void Tilt(Vector3 _tilt)
    {
        tilt = _tilt;
    }

    // Judėjimas
    public void PerformMovement()
    {
        // type: 1 - walking, 2 - running
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
    // Horizantalus pasukimas
    private void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
    }
    // Vertikalus pavertimas
    private void PerformTilt()
    {
        if (tilt != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (cam != null)
            {
                cam.transform.Rotate(-tilt);
            }
        }
    }

    // Patikrina ar gali naudoti multipleAttack
    public bool canMultipleAttack()
    {
        return (multipleAttackTimer <= 0f && mana >= multipleAttackManaCost);
    }

    // Patikrina ar gali naudoti strongAttack
    public bool canStrongAttack()
    {
        return (strongAttackTimer <= 0f && mana >= strongAttackManaCost);
    }

    // Patikrina ar gali naudoti stunAttack
    public bool canStunAttack()
    {
        return (stunAttackTimer <= 0f && mana >= stunAttackManaCost);
    }

    // Atakavimas
    public void PerformAttack(int type)
    {
        Collider[] hitEnemies;
        if(type == 2)
        {
            hitEnemies = Physics.OverlapSphere(attackPoint.position, attackMultipleRange, enemyLayers);
        }
        else
        {
            hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        }

        if (type == 1)
        {
            Debug.Log("Basic attack");
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.GetComponent<enemyOgre>() != null)
                    enemy.GetComponent<enemyOgre>().takeDmg(Mathf.RoundToInt(dmg), 0f);
                else if (enemy.GetComponent<enemySkeleton>() != null)
                    enemy.GetComponent<enemySkeleton>().takeDmg(Mathf.RoundToInt(dmg), 0f);
                else enemy.GetComponent<enemyWolf>().takeDmg(Mathf.RoundToInt(dmg), 0f);
            }

        }
        else if (type == 2)
        {
            Debug.Log("Multiple attack");
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.GetComponent<enemyOgre>() != null)
                    enemy.GetComponent<enemyOgre>().takeDmg(Mathf.RoundToInt(dmg * multipleAttackMultiplier), 0f);
                else if (enemy.GetComponent<enemySkeleton>() != null)
                    enemy.GetComponent<enemySkeleton>().takeDmg(Mathf.RoundToInt(dmg * multipleAttackMultiplier), 0f);
                else enemy.GetComponent<enemyWolf>().takeDmg(Mathf.RoundToInt(dmg * multipleAttackMultiplier), 0f);
            }
            mana -= multipleAttackManaCost;
            multipleAttackTimer = multipleAttackCooldown;
            float pct = (float)mana / (float)manaMax;
            stats.changeMp(pct, mana);
        }
        else if (type == 3)
        {
            Debug.Log("Strong attack");
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.GetComponent<enemyOgre>() != null)
                    enemy.GetComponent<enemyOgre>().takeDmg(Mathf.RoundToInt(dmg * strongAttackMultiplier), 0f);
                else if (enemy.GetComponent<enemySkeleton>() != null)
                    enemy.GetComponent<enemySkeleton>().takeDmg(Mathf.RoundToInt(dmg * strongAttackMultiplier), 0f);
                else enemy.GetComponent<enemyWolf>().takeDmg(Mathf.RoundToInt(dmg * strongAttackMultiplier), 0f);
            }
            mana -= strongAttackManaCost;
            strongAttackTimer = strongAttackCooldown;
            float pct = (float)mana / (float)manaMax;
            stats.changeMp(pct, mana);
        }
        else
        {
            Debug.Log("Stun attack");
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.GetComponent<enemyOgre>() != null)
                    enemy.GetComponent<enemyOgre>().takeDmg(Mathf.RoundToInt(dmg * stunAttackMultiplier), stunDuration);
                else if (enemy.GetComponent<enemySkeleton>() != null)
                    enemy.GetComponent<enemySkeleton>().takeDmg(Mathf.RoundToInt(dmg * stunAttackMultiplier), stunDuration);
                else enemy.GetComponent<enemyWolf>().takeDmg(Mathf.RoundToInt(dmg * stunAttackMultiplier), stunDuration);
            }
            mana -= stunAttackManaCost;
            strongAttackTimer = strongAttackCooldown;
            float pct = (float)mana / (float)manaMax;
            stats.changeMp(pct, mana);
        }
    }
    
    // Reguliuoja ar siuo metu zaidejas blokuoja
    public void changeBlocking(bool _blocking)
    {
        blocking = _blocking;
        if (blocking == true)
            Debug.Log("Blocking");
        else
        Debug.Log("Not blocking");
    }

    // Ar gali siuo metu zaidejas atlikti dodge
    public bool canDodge()
    {
        return (stamina >= dodgeCost);
    }

    public void handleDodge(int side)
    {
        // side - 1( i prieki), 2( atgal), 3( i kaire), 4( i desine)

        RaycastHit hit;
        Vector3 destination;
        Vector3 direction;
        if (side == 1)
        {
            direction = transform.forward;
        }
        else if(side == 2)
        {
            direction = -transform.forward;
        }
        else if(side == 3)
        {
            direction = -transform.right;
        }
        else
        {
            direction = transform.right;
        }
        
        destination = rb.position + direction * dodgeDistance;
        // jei randa blokuojanti objekta
        if (Physics.Linecast(rb.position, destination, out hit))
        {
            destination = transform.position + direction * (hit.distance - 1f);
        }
        else rb.MovePosition(destination);

        stamina -= dodgeCost;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Vector3 pos = transform.position;
        pos.y += 2;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(pos, attackMultipleRange);
    }

    // Upgrade function stuff

    public bool checkIfEnoughMoney(int cost)
    {
        return cost >= money;
    }

    public bool checkIfHaveUnusedStatPoint()
    {
        return unusedStatPoints > 0;
    }

    public bool checkIfHaveUnusedSkillPoint()
    {
        return unusedSkillPoints > 0;
    }

    public void upgradeWeapon(int cost, int _dmg)
    {
        money -= cost;
        dmg += _dmg;
        GetComponent<upgradeShop>().getInfo();
    }

    public void upgradeArmor(int cost, int _armor)
    {
        money -= cost;
        armor += _armor;
        GetComponent<upgradeShop>().getInfo();
    }

    public void upgradeHp(int _hpMax, int _regenHp)
    {
        hpMax += _hpMax;
        regenHp += _regenHp;
        unusedStatPoints--;
        stats.changeHpMax(hpMax);
        float pct = (float)hp / (float)hpMax;
        stats.changeMp(pct, hp);
        getInfoStatSkill();
    }

    public void upgradeMp(int _mpMax, int _regenMp)
    {
        manaMax += _mpMax;
        regenMana += _regenMp;
        unusedStatPoints--;
        stats.changeMpMax(manaMax);
        float pct = (float)mana / (float)manaMax;
        stats.changeMp(pct, mana);
        getInfoStatSkill();
    }

    public void upgradeSt(int _stMax, int _regenSt)
    {
        staminaMax += _stMax;
        regenStamina += _regenSt;
        unusedStatPoints--;
        stats.changeStMax(staminaMax);
        float pct = (float)stamina / (float)staminaMax;
        stats.changeMp(pct, stamina);
        getInfoStatSkill();
    }

    public void upgradeMultiple(float _multiplier, float _coolDown)
    {
        multipleAttackCooldown -= _coolDown;
        multipleAttackMultiplier += _multiplier;
        unusedSkillPoints--;
        getInfoStatSkill();
    }

    public void upgradeStrong(float _multiplier, float _coolDown)
    {
        strongAttackCooldown -= _coolDown;
        strongAttackMultiplier += _multiplier;
        unusedSkillPoints--;
        getInfoStatSkill();
    }

    public void upgradeStun(float _multiplier, float _coolDown)
    {
        stunAttackCooldown -= _coolDown;
        stunAttackMultiplier += _multiplier;
        unusedSkillPoints--;
        getInfoStatSkill();
    }

    public void getInfoStatSkill()
    {
        men.updateStatSkills(hpMax, regenHp, manaMax, regenMana, staminaMax, regenStamina,
            multipleAttackCooldown, multipleAttackMultiplier, strongAttackCooldown, strongAttackMultiplier,
            stunAttackCooldown, stunAttackMultiplier, unusedStatPoints, unusedSkillPoints);
    }

    public void getInfoShop(int _costWeapon, int _costArmor)
    {
        men.updateShop(dmg, _costWeapon, armor, _costArmor, money);
    }

}
