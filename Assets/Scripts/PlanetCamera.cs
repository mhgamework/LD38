﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Rigidbody RigidBody;

    public GameObject Dude;
    public float MovementSpeed = 1;

    [SerializeField]
    private float radius = 10.3f;

    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private string walkBlendParamName = "WalkBlend";
    [SerializeField]
    private PlanetCameraDirectorManager directorManager = null;

    private Vector3 currentCameraDirection;

    // Use this for initialization
    void Start()
    {
        currentCameraDirection = Camera.main.transform.forward;
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

        Vector3 new_dir;
        if (directorManager.GetDirection(RigidBody.transform.position, out new_dir))
        {
            currentCameraDirection = Vector3.Lerp(currentCameraDirection, new_dir, 0.01f);
        }

        var f = currentCameraDirection; //Camera.main.transform.forward;
        var right = Vector3.Cross(f, RigidBody.position.normalized).normalized;
        var forward = Vector3.Cross(RigidBody.position.normalized, right).normalized;

        Debug.DrawLine(RigidBody.position, RigidBody.position + right, Color.red);
        Debug.DrawLine(RigidBody.position, RigidBody.position + forward, Color.blue);

        RigidBody.position = RigidBody.position.normalized * radius;

        RigidBody.velocity = (right * dir.x + forward * dir.y) * MovementSpeed;
        RigidBody.angularVelocity = new Vector3();

        animator.SetFloat(walkBlendParamName, Mathf.Clamp01(RigidBody.velocity.magnitude / MovementSpeed));

        RigidBody.transform.LookAt(RigidBody.position + forward, RigidBody.position.normalized);
        //Dude.transform.Rotate(new Vector3(1, 0, 0), dir.y * Time.deltaTime* MovementSpeed,Space.Self);
        //Dude.transform.Rotate(new Vector3(0, 0, 1), dir.x * Time.deltaTime* MovementSpeed, Space.Self);
    }
}
