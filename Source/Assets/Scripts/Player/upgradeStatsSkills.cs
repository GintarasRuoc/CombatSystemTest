using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeStatsSkills : MonoBehaviour
{
    private CorePlayer core;
    [Header("Hp/Mp/St")]
    public int hpMaxUpg = 10;
    public int hpRegenUpg = 1;

    public int mpMaxUpg = 10;
    public int mpRegenUpg = 1;

    public int stMaxUpg = 5;
    public int stRegenUpg = 1;

    [Header("Skills")]
    public float multiplierUpg = 0.1f;
    public float cooldownUpg = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        core = PlayerManager.Instance.player.GetComponent<CorePlayer>();
    }

    public void upgradeHp()
    {
        if(core.checkIfHaveUnusedStatPoint())
        {
            core.upgradeHp(hpMaxUpg, hpRegenUpg);
        }
    }

    public void upgradeMp()
    {
        if(core.checkIfHaveUnusedStatPoint())
        {
            core.upgradeMp(mpMaxUpg, mpRegenUpg);
        }
    }
    
    public void upgradeSt()
    {
        if(core.checkIfHaveUnusedStatPoint())
        {
            core.upgradeSt(stMaxUpg, stRegenUpg);
        }
    }

    public void upgradeMultiple()
    {
        if(core.checkIfHaveUnusedSkillPoint())
        {
            core.upgradeMultiple(multiplierUpg, cooldownUpg);
        }
    }

    public void upgradeStrong()
    {
        if (core.checkIfHaveUnusedSkillPoint())
        {
            core.upgradeStrong(multiplierUpg, cooldownUpg);
        }
    }

    public void upgradeStun()
    {
        if (core.checkIfHaveUnusedSkillPoint())
        {
            core.upgradeStun(multiplierUpg, cooldownUpg);
        }
    }
}
