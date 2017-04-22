using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Rigidbody RigidBody;

    public GameObject Dude;
    public float MovementSpeed = 1;

    [SerializeField]
    private float radius = 10.3f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody.MoveRotation(Quaternion.AngleAxis(Time.realtimeSinceStartup, Vector3.forward));
        //rigidbody.MovePosition(new Vector3(0, 0, 0));

        var dir = new Vector2();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            dir += new Vector2(0, 1);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir += new Vector2(0, -1);


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir += new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            dir += new Vector2(1, 0);

        dir = dir.normalized;

        var f = Vector3.one; //Camera.main.transform.forward;
        var right = Vector3.Cross(f, RigidBody.position.normalized).normalized;
        var forward = Vector3.Cross(RigidBody.position.normalized, right).normalized;

        Debug.DrawLine(RigidBody.position, RigidBody.position + right, Color.red);
        Debug.DrawLine(RigidBody.position, RigidBody.position + forward, Color.blue);

        RigidBody.position = RigidBody.position.normalized * radius;

        RigidBody.velocity = (right * dir.x + forward * dir.y) * Time.deltaTime * MovementSpeed;
        RigidBody.angularVelocity = new Vector3();

        RigidBody.transform.LookAt(RigidBody.position + forward, RigidBody.position.normalized);
        //Dude.transform.Rotate(new Vector3(1, 0, 0), dir.y * Time.deltaTime* MovementSpeed,Space.Self);
        //Dude.transform.Rotate(new Vector3(0, 0, 1), dir.x * Time.deltaTime* MovementSpeed, Space.Self);
    }
}
