using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class openMenu : MonoBehaviour
{
    private CorePlayer core;
    private upgradeShop upgShop;
    private int enemyCount = 0;
    public GameObject statSkills;
    public GameObject shop;
    public GameObject exit;

    private bool check;
    [Header("Stat&Skill")]
    [Header("Current Stats")]
    public Text maxHp;
    public Text regenHp;
    public Text maxMp;
    public Text regenMp;
    public Text maxSt;
    public Text regenSt;

    [Header("After Stats")]
    public Text maxHpAfter;
    public Text regenHpAfter;
    public Text maxMpAfter;
    public Text regenMpAfter;
    public Text maxStAfter;
    public Text regenStAfter;

    [Header("Current Skills")]
    public Text multipleAttackCDR;
    public Text multipleAttackMultiplier;
    public Text strongAttackCDR;
    public Text strongAttackMultiplier;
    public Text stunAttackCDR;
    public Text stunAttackMultiplier;

    [Header("After Skills")]
    public Text multipleAttackCDRAfter;
    public Text multipleAttackMultiplierAfter;
    public Text strongAttackCDRAfter;
    public Text strongAttackMultiplierAfter;
    public Text stunAttackCDRAfter;
    public Text stunAttackMultiplierAfter;

    [Header("Unused points")]
    public Text unusedStat;
    public Text unusedSkill;

    [Header("Shop")]
    [Header("Weapon")]
    public Text currentDmg;
    public Text afterDmg;
    public Text costDmg;

    [Header("Armor")]
    public Text currentArmor;
    public Text afterArmor;
    public Text costArmor;

    [Header("Currency")]
    public Text currentMoney;

    private void Start()
    {
        core = PlayerManager.Instance.player.GetComponent<CorePlayer>();
        upgShop = PlayerManager.Instance.player.GetComponent<upgradeShop>();
    }

    private void Update()
    {
        if(enemyCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                check = statSkills.activeSelf;
                if (check == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    statSkills.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    askForInfoStatSkill();
                    statSkills.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                check = shop.activeSelf;
                if (check == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    shop.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    askForInfoShop();
                    shop.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            check = exit.activeSelf;
            if (check == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log(Cursor.lockState.ToString());
                exit.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                exit.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void turnOff()
    {
        statSkills.SetActive(false);
        shop.SetActive(false);
        exit.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void leave()
    {
        SceneManager.LoadScene(0);
    }

    public void getInfo(int count)
    {
        enemyCount = count;
    }

    public void askForInfoShop()
    {
        upgShop.getInfo();
    }

    public void askForInfoStatSkill()
    {
        core.getInfoStatSkill();
    }

    public void updateStatSkills(int _maxHp, int _regenHp, int _maxMp, int _regenMp, int _maxSt, int _regenSt,
        float _multipleCDR, float _multipleMultiplier, float _strongCdr, float _strongMultiplier,
        float _stunCdr, float _stunMultiplier, int _unusedStat, int _unusedSkill)
    {
        maxHp.text = _maxHp.ToString();
        maxHpAfter.text = (_maxHp + 10).ToString();
        regenHp.text = _regenHp.ToString();
        regenHpAfter.text = (_regenHp + 1).ToString();
        maxMp.text = _maxMp.ToString();
        maxMpAfter.text = (_maxMp + 10).ToString();
        regenMp.text = _regenMp.ToString();
        regenMpAfter.text = (_regenMp + 1).ToString();
        maxSt.text = _maxSt.ToString();
        maxStAfter.text = (_maxSt + 5).ToString();
        regenSt.text = _regenSt.ToString();
        regenStAfter.text = (_regenSt + 1).ToString();
        unusedStat.text = _unusedStat.ToString();

        multipleAttackCDR.text = _multipleCDR.ToString();
        multipleAttackCDRAfter.text = (_multipleCDR - 0.2f).ToString();
        multipleAttackMultiplier.text = _multipleMultiplier.ToString();
        multipleAttackMultiplierAfter.text = (_multipleMultiplier + 0.1f).ToString();
        strongAttackCDR.text = _strongCdr.ToString();
        strongAttackCDRAfter.text = (_strongCdr - 0.2f).ToString();
        strongAttackMultiplier.text = _strongMultiplier.ToString();
        strongAttackMultiplierAfter.text = (_strongMultiplier + 0.1f).ToString();
        stunAttackCDR.text = _stunCdr.ToString();
        stunAttackCDR.text = (_stunCdr - 0.2f).ToString();
        stunAttackMultiplier.text = _stunMultiplier.ToString();
        stunAttackMultiplierAfter.text = (_stunMultiplier + 0.1f).ToString();
        unusedSkill.text = _unusedSkill.ToString();
    }

    public void updateShop(int _dmg, int _costWeapon, int _armor, int _costArmor, int _money)
    {
        currentDmg.text = _dmg.ToString();
        afterDmg.text = (_dmg + 2).ToString();
        costDmg.text = _costWeapon.ToString();
        currentArmor.text = _armor.ToString();
        afterArmor.text = (_armor + 2).ToString();
        costArmor.text = _costArmor.ToString();
        currentMoney.text = _money.ToString();
    }
}
