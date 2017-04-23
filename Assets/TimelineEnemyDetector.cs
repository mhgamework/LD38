using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class TimelineEnemyDetector : MonoBehaviour, ITimelineEntity
{
    public bool HasEnemies = false;
    // Use this for initialization
    void Start()
    {
        GetComponent<BendAroundPlanet>().GetTarget().GetComponent<Renderer>().enabled = false;

    }
    private Vector3 getSpawnPosition()
    {
        return GetComponent<BendAroundPlanet>().GetTarget().transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        var enemies = Physics.OverlapSphere(getSpawnPosition(), GetComponent<BendAroundPlanet>().Scale)
            .Select(f => EnemiesHelper.GetEnemyForCollider(f)).Where(f => f != null);
        HasEnemies = enemies.Any();
    }

    public void OnDrawGizmo()
    {

    }
}
