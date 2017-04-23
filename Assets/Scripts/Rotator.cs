using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationDir = Vector3.zero;
    [SerializeField]
    [Range(0f, 1000f)]
    private float speed = 100f;

    void Update()
    {
        transform.localEulerAngles += rotationDir * speed * Time.deltaTime;
    }
}
