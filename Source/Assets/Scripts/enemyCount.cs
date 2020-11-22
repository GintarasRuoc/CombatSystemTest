using UnityEngine;

public class enemyCount : MonoBehaviour
{
    [SerializeField]
    private int enemysLeft = 0;

    private spawner manage;
    public GameObject wave;
    private waveStats ui;
    public GameObject menuObj;
    private openMenu menu;

    private void Start()
    {
        manage = GetComponent<spawner>();
        menu = menuObj.GetComponent<openMenu>();
        ui = wave.GetComponent<waveStats>();
        PointOfInterest.OnPointOfInterestEntered +=
            PointOfInterest_OnPointOfInterestEntered;
    }

    private void PointOfInterest_OnPointOfInterestEntered(int poi)
    {
        enemysLeft += poi;

        ui.getInfo(enemysLeft);
        manage.getInfo(enemysLeft);
        menu.getInfo(enemysLeft);
    }
}
