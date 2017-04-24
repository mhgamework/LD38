using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGate : MonoBehaviour
{
    [SerializeField]
    private GameObject gateContainer = null;
    [SerializeField]
    private TextMesh text = null;
    [SerializeField]
    private int requiredPoints = 100;

    private Gate gate;

    void Start()
    {
        gate = gateContainer.transform.GetComponentInChildren<Gate>();
        text.text = "Required points:\n" + requiredPoints;
    }

    void Update()
    {
        if (requiredPoints <= PlayerPoints.Points)
        {
            gate.OpenGate();
            text.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
