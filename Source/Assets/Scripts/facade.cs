using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facade : MonoBehaviour
{
    private upgradeStatsSkills statSkill;
    private upgradeShop shop;
    void Start()
    {
        statSkill = PlayerManager.Instance.player.GetComponent<upgradeStatsSkills>();
        shop = PlayerManager.Instance.player.GetComponent<upgradeShop>();
    }

    public void upgradeHealth()
    {
        statSkill.upgradeHp();
    }

    public void upgradeMana()
    {
        statSkill.upgradeMp();
    }
    
    public void upgradeStamina()
    {
        statSkill.upgradeSt();
    }

    public void upgradeMultipleSkill()
    {
        statSkill.upgradeMultiple();
    }

    public void upgradeStrongSkill()
    {
        statSkill.upgradeStrong();
    }

    public void upgradeStunSkill()
    {
        statSkill.upgradeStun();
    }

    public void upgradeWeaponItem()
    {
        shop.upgradeWeapon();
    }

    public void upgradeArmorItem()
    {
        shop.upgradeArmor();
    }
}
