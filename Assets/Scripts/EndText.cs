using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class EndText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMesh>().text = string.Format("FINAL SCORE: {0}\nThanks for playing!!", PlayerPoints.Points);
    }
}
