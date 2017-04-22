using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class BendAroundPlanet : MonoBehaviour
{

    public float CircleCircumference
    {
        get { return CircleRadius * 2 * Mathf.PI; }
    }

    public float CircleRadius = 10;// { get { return CircleCircumference / 2 / Mathf.PI; } }
    public GameObject Source;
    [SerializeField]
    private GameObject LastSource;
    [SerializeField]
    private GameObject Target;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float scale = 1f;
    [SerializeField]
    [Range(0f, 360f)]
    private float rotation = 0f;

    private float lastRotation = 1f;
    private float lastScale = 1f;
    //public bool reset = false;
    // Use this for initialization
    void Start()
    {
        //transform.position = new Vector3(0, 1, 0) * CircleRadius;
    }

    //void OnValidate()
    //{
    //    if (reset)
    //    {
    //        Destroy(Target);
    //        Target = null;
    //        reset = false;
    //    }
    //}

    private void bendAllMeshes(Transform obj)
    {
        foreach (var filter in obj.GetComponentsInChildren<MeshFilter>())
        {
            var opt = filter.GetComponent<BendOptions>();
            if (opt && !opt.BendMesh)
                continue;

            var mesh = new Mesh
            {
                vertices = filter.sharedMesh.vertices,
                triangles = filter.sharedMesh.triangles,
                uv = filter.sharedMesh.uv
            };
            var verts = mesh.vertices
                .Select(v =>
                    {
                        var vWorld = filter.transform.TransformPoint(v);
                        var vBendWorld = bendVertexWorldSpace(vWorld);
                        return vBendWorld;
                    }
                ).ToList();

            mesh.SetVertices(verts);
            filter.sharedMesh = mesh;
        }

        foreach (var b in obj.GetComponentsInChildren<BendTransform>())
        {
            bendTransform(b.transform);
        }


        foreach (var filter in obj.GetComponentsInChildren<MeshFilter>())
        {
            var opt = filter.GetComponent<BendOptions>();
            if (opt && !opt.BendMesh)
                continue;


            //bendTransform(filter.transform);
            var mesh = filter.sharedMesh;

            var verts = mesh.vertices
                .Select(v => { return filter.transform.InverseTransformPoint(v); }
                ).ToList();


            //verts = verts.Select()


            mesh.SetVertices(verts);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();


            var collider = filter.GetComponent<MeshCollider>();
            if (collider) collider.sharedMesh = mesh;
        }
    }

    public void Update()
    {
        if (Target == null && Source == null) return;
        if (Target != null && Source != null && Source == LastSource && scale == lastScale && lastRotation == rotation) return;
        LastSource = Source;
        lastScale = scale;
        lastRotation = rotation;
        if (Target != null) DestroyImmediate(Target);
        if (Source == null) return;
        Target = Instantiate(Source);
        Target.transform.localPosition += new Vector3(0, CircleRadius, 0); //* CircleRadius;
        Target.transform.localScale = Vector3.Scale(new Vector3(scale, 1f, scale), Target.transform.localScale);
        Target.transform.localEulerAngles += new Vector3(0f, rotation, 0f);


        bendAllMeshes(Target.transform);
        Target.transform.SetParent(transform, false);
        Target.name += " - Bended";
        Target.SetActive(true);



    }

    private void bendTransform(Transform t)
    {
        t.Rotate(Vector3.up, -Mathf.Rad2Deg * CalculateAngle(t.position));

        var originTransformedW = bendVertexWorldSpace(t.position);

        t.position = originTransformedW;
    }

    /*private Vector3 bendVertex(Vector3 v, Transform trans)
    {
        var vWorld = trans.TransformPoint(v);
        var transformedWorld = bendVertexWorldSpace(vWorld);

        var transformedOrigin = trans.TransformPoint(new Vector3());


        var radius = worldRadius;//v.x;
        var angle = v.z / 36 * Mathf.PI*2;

        return new Vector3(radius*Mathf.Cos(angle), v.y, radius*Mathf.Sin(angle));

    }*/

    private Vector3 bendVertexWorldSpace(Vector3 vWorld)
    {
        return vWorld.normalized * (vWorld.y);

        ////var r = CircleRadius;
        //var r = vWorld.y;

        //var v = new Vector2(vWorld.z, vWorld.x).normalized;
        //var longitude = Mathf.Atan2(v.y, v.x);
        //var latitude = Mathf.PI * 0.5f - new Vector2(vWorld.z, vWorld.x).magnitude / CircleCircumference * Mathf.PI * 2;

        ////var R = vWorld.magnitude;
        ////var longitude = vWorld.x;
        ////var latitude = vWorld.z;

        //////Mercator projection (x, y) of a given latitude and longitude is:
        //////var x = R * longitude;
        //////var y = R * Mathf.Log(Mathf.Tan((latitude + Mathf.PI / 2) / 2));

        ////////and the inverse mapping of a given map location(x, y) is:
        ////longitude = vWorld.x / R;
        ////latitude = 2 * Mathf.Atan(Mathf.Exp(vWorld.z / R)) - Mathf.PI / 2;

        ////Given longitude and latitude on a sphere of radius S,
        ////the 3D coordinates P = (P.x, P.y, P.z) are:
        //var P = new Vector3();
        //P.x = vWorld.x;
        //P.z = vWorld.z;
        ////P.x = r * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        ////P.z = r * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        //P.y = r * Mathf.Sin(latitude);

        //return P;
        //////var radius = vWorld.x;//+ CircleRadius;
        ////var angleZ = vWorld.z / CircleCircumference * Mathf.PI * 2;
        ////var anglex = vWorld.x / CircleCircumference * Mathf.PI * 2;

        ////return new Vector3(radius * Mathf.Cos(angle), vWorld.y, radius * Mathf.Sin(angle));

    }

    private float CalculateAngle(Vector3 vWorld)
    {
        var angle = vWorld.z / CircleCircumference * Mathf.PI * 2;
        return angle;
    }

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

        DestroyImmediate(o);
    }

    public GameObject GetTarget() { return Target; }
}
