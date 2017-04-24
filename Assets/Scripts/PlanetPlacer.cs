using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class PlanetPlacer : MonoBehaviour
{
    public float CircleRadius = 100f;

    public void PlaceObject(Ray ray)
    {
        var o = new GameObject("Sphere");
        var sphere = o.AddComponent<SphereCollider>();
        sphere.radius = CircleRadius;

        RaycastHit hit;
        if (!sphere.Raycast(ray, out hit, 1000))
        {
            DestroyImmediate(o);
            return;
        }

        var dir = hit.point.normalized;
        transform.up = dir;
        transform.position = hit.point;

        DestroyImmediate(o);
    }
}
#endif
