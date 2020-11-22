using System;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public static event Action<int> OnPointOfInterestEntered;

    private void Start()
    {
        OnPointOfInterestEntered?.Invoke(1);
    }

    private void OnDestroy()
    {
        OnPointOfInterestEntered?.Invoke(-1);
    }

}
