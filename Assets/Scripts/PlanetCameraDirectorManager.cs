using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Keeps track of all directors, provides funcrtionality to get the closest one.
/// </summary>
public class PlanetCameraDirectorManager : MonoBehaviour
{
    private List<PlanetCameraDirector> directors;

    void Start()
    {
        directors = FindObjectsOfType<PlanetCameraDirector>().ToList();
    }

    public bool GetDirection(Vector3 position, out Vector3 direction)
    {
        var succeeded = false;
        var min_dist = float.MaxValue;
        direction = Vector3.zero;
        foreach (var director in directors)
        {
            if (director.GetDistance(position) < min_dist)
            {
                succeeded = true;
                direction = director.GetDirection();
            }
        }

        return succeeded;
    }


}
