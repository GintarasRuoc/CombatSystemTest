using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveStats : MonoBehaviour
{
    public Text waveNumber;
    private int wave = 1;
    public Text enemiesCount;
    public GameObject noEnemies;

    public void getInfo(int count)
    {
        enemiesCount.text = count.ToString();
        if (count == 0)
        {
            wave++;
            waveNumber.text = wave.ToString();
            noEnemies.SetActive(true);
        }
        else if (count > 0)
            noEnemies.SetActive(false);

    }
}
