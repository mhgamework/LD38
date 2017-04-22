using UnityEngine;
using System.Collections;
using System.Linq;

public class BendAroundPoint : MonoBehaviour
{

    public float CircleCircumference = 36;

    private float CircleRadius { get { return CircleCircumference / 2 / Mathf.PI; } }
    // Use this for initialization
    void Start()
    {

        transform.position = new Vector3(1, 0, 0) * CircleRadius;

        foreach (var filter in GetComponentsInChildren<MeshFilter>())
        {
            var opt = filter.GetComponent<BendOptions>();
            if(opt && !opt.BendMesh)
                continue;

            var mesh = filter.mesh;



            var verts = mesh.vertices
                .Select(v =>
                {
                    var vWorld = filter.transform.TransformPoint(v);
                    var vBendWorld = bendVertexWorldSpace(vWorld);
                    return vBendWorld;
                }
                ).ToList();

            mesh.SetVertices(verts);
        }

        foreach (var b in GetComponentsInChildren<BendTransform>())
        {
            bendTransform(b.transform);
        }


        foreach (var filter in GetComponentsInChildren<MeshFilter>())
        {
            var opt = filter.GetComponent<BendOptions>();
            if (opt && !opt.BendMesh)
                continue;


            //bendTransform(filter.transform);
            var mesh = filter.mesh;

            var verts = mesh.vertices
            .Select(v =>
            {
                return filter.transform.InverseTransformPoint(v);
            }
            ).ToList();




            //verts = verts.Select()



            mesh.SetVertices(verts);

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();


            var collider = filter.GetComponent<MeshCollider>();
            if (collider) collider.sharedMesh = mesh;
        }



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

        var radius = vWorld.x;//+ CircleRadius;
        var angle = CalculateAngle(vWorld);

        return new Vector3(radius * Mathf.Cos(angle), vWorld.y, radius * Mathf.Sin(angle));

    }

    private float CalculateAngle(Vector3 vWorld)
    {
        var angle = vWorld.z / CircleCircumference * Mathf.PI * 2;
        return angle;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
