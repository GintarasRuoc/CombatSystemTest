using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStatsInterface : MonoBehaviour
{
    // hp
    [Header("Health")]
    public Image fHp;
    public Text hpLeft;
    public Text hpMax;

    // mp
    [Header("Mana")]
    public Image fMp;
    public Text mpLeft;
    public Text mpMax;

    // st
    [Header("Stamina")]
    public Image fSt;
    public Text stLeft;
    public Text stMax;

    // xp
    [Header("Expierience")]
    public Image fXp;

    [Header("Skills")]
    public Image skill1;
    public Image skill2;
    public Image skill3;

    public void changeHp(float pct, int left)
    {
        fHp.fillAmount = pct;
        hpLeft.text = left.ToString();
    }

    public void changeHpMax(int max)
    {
        hpMax.text = max.ToString();
    }

    public void changeMp(float pct, int left)
    {
        fMp.fillAmount = pct;
        mpLeft.text = left.ToString();
    }

    public void changeMpMax(int max)
    {
        mpMax.text = max.ToString();
    }

    public void changeSt(float pct, int left)
    {
        fSt.fillAmount = pct;
        stLeft.text = left.ToString();
    }

    public void changeStMax(int max)
    {
        stMax.text = max.ToString();
    }

    public void changeXp(float pct)
    {
        fXp.fillAmount = pct;
    }

    public void changeSkillOne(float pct)
    {
        skill1.fillAmount = pct;
    }

    public void changeSkillTwo(float pct)
    {
        skill2.fillAmount = pct;
    }

    public void changeSkillThree(float pct)
    {
        skill3.fillAmount = pct;
    }
}
