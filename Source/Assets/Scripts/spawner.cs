using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private int wave = 1;
    private int enemies = 0;

    [Header("Prefabs")]
    public GameObject ogre;
    public GameObject skeleton;
    public GameObject wolf;

    public void Update()
    {
        if (enemies == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            spawn();
            enemies = 0;
        }
    }

    public void spawn()
    {
        Vector3 spawnPoint = new Vector3(-10f, 0.2f, 10);
        int type;
        for(int i = 0; i < wave + 2; i++)
        {
            type = Random.Range(1, 4);
            if(type == 1)
            {
                Instantiate(ogre, spawnPoint + new Vector3(3 * i, 0, 0), Quaternion.identity);
            }
            else if(type == 2)
            {
                Instantiate(skeleton, spawnPoint + new Vector3(3 * i, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(wolf, spawnPoint + new Vector3(3 * i, 0, 0), Quaternion.identity);
            }
        }
    }

    public void getInfo(int count)
    {
        enemies = count;
        if (count == 0)
        {
            wave++;
        }
    }
}
