using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class TimelineEnemyDetector : MonoBehaviour, ITimelineEntity
{
    public bool HasEnemies = false;
    public LayerMask EnemiesMask;
    // Use this for initialization
    void Start()
    {
        GetComponent<BendAroundPlanet>().GetTarget().GetComponent<Renderer>().enabled = false;
        StartCoroutine(begin().GetEnumerator());

    }

    private IEnumerable<YieldInstruction> begin()
    {
        yield return new WaitForSeconds(Random.Range(150,250)); // Randomize because multiple detectors active
        var enemies = Physics.OverlapSphere(getSpawnPosition(), GetComponent<BendAroundPlanet>().Scale, EnemiesMask)
            .Select(f => EnemiesHelper.GetEnemyForCollider(f)).Where(f => f != null);
        HasEnemies = enemies.Any();
    }

    private Vector3 getSpawnPosition()
    {
        return GetComponent<BendAroundPlanet>().GetTarget().transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmo()
    {

    }
}
