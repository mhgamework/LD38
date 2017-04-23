using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    private GameObject forceField = null;

    public void OpenGate()
    {
        forceField.SetActive(false);
    }

    public void CloseGate()
    {
        forceField.SetActive(true);
    }
}
