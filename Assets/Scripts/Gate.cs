using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour, ITimelineEntity
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
