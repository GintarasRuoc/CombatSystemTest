using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeShop : MonoBehaviour
{
    private CorePlayer core;

    [Header("Weapon info")]
    public int wpnUpgCost = 100;
    public int wpnDmgUpgrade = 2;
    public float wpnUpgCostMultiplier = 1.2f;

    [Header("Armor info")]
    public int armorUpgCost = 70;
    public int armorUpgrade = 2;
    public float armorUpgCostMultiplier = 1.2f;
    void Start()
    {
        core = PlayerManager.Instance.player.GetComponent<CorePlayer>();
    }

    public void upgradeWeapon()
    {
        if(core.checkIfEnoughMoney(wpnUpgCost))
        {
            core.upgradeWeapon(wpnUpgCost, wpnDmgUpgrade);
            wpnUpgCost = Mathf.RoundToInt(wpnUpgCostMultiplier * wpnUpgCost);
        }
    }

    public void upgradeArmor()
    {
        if (core.checkIfEnoughMoney(wpnUpgCost))
        {
            core.upgradeArmor(armorUpgCost, armorUpgrade);
            armorUpgCost = Mathf.RoundToInt(armorUpgCostMultiplier * armorUpgCost);
        }
    }

    public void getInfo()
    {
        
        core.getInfoShop(wpnUpgCost, armorUpgCost);
    }
}
