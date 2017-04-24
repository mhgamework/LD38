using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointsHud : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = "Points: " + PlayerPoints.Points;
    }

}
