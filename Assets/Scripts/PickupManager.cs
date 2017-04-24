using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static Pickup[] allPickups = new Pickup[0];
    private static List<Pickup> restorePoint = new List<Pickup>();


    void Start()
    {
        allPickups = FindObjectsOfType<Pickup>();
    }

    public static void CreateRestorePoint()
    {
        restorePoint.Clear();
        foreach (var pickup in allPickups)
        {
            if (!pickup.IsPickedUp) //keep track of the ones that arent picked up yet
                restorePoint.Add(pickup);
        }
    }

    public static void Restore()
    {
        foreach (var pickup in restorePoint)
        {
            pickup.ResetPickedUp(); //reset the ones that werent picked up
        }
    }

}
