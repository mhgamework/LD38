using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TimelineTrigger : MonoBehaviour, ITimelineEntity
{
    private TimelineTrigger parent = null;

    public bool PlayerInTrigger = false;
    // Use this for initialization
    void Start()
    {
        //if (transform.parent != null)
        //    parent = transform.parent.GetComponentInParent<TimelineTrigger>();
        //if (!parent)
        GetComponent<BendAroundPlanet>().GetTarget().GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Vector3 getSpawnPosition()
    {
        return GetComponent<BendAroundPlanet>().GetTarget().transform.position;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = PlayerInTrigger ? Color.red : Color.green;
        Gizmos.DrawSphere(getSpawnPosition(), 2);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<PlanetCamera>() != null)
            PlayerInTrigger = true;
    }

    public void OnTriggerStay(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponentInParent<PlanetCamera>() != null)
            PlayerInTrigger = false;
    }
}
