using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Declares the forxard vector of the camera (when player is withing radius)
/// </summary>
public class PlanetCameraDirector : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 25f)]
    private float radius = 10f;

    public float GetDistance(Vector3 world_pos)
    {
        var dist = Vector3.Distance(world_pos, transform.position);
        if (dist > radius)
            return float.MaxValue;
        return dist;
    }

    public Vector3 GetDirection()
    {
        return transform.forward;
    }

    void OnDrawGizmos()
    {
        var prev_color = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, transform.position + GetDirection() * radius);

        Gizmos.color = prev_color;
    }
}
