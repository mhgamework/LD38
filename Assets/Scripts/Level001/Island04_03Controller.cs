using Assets.Scripts;
using Assets.Scripts.Timeline;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Island04_03Controller : MonoBehaviour
{

    private bool done = false;
    private Coroutine coroutine;
    private TimelineArea t;

    public void Start()
    {
        t = GetComponent<TimelineArea>();
        t.RunWithCheckpoint("Checkpoint", begin);
    }

    public IEnumerable<YieldInstruction> begin()
    {

        yield return null;

        while (!t.playerInTrigger("Checkpoint")) yield return null;

        PlayerHealthScript.Instance.RestoreHealth();

        foreach (var spawner in t.Get<Spawner>("Bomber"))
        {
            spawner.Spawn(t.Bomber);
        }
        foreach (var spawner in t.Get<Spawner>("Heavy"))
        {
            spawner.Spawn(t.Heavy);
        }

        yield return null;
        while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

        PlayerHealthScript.Instance.RestoreHealth();

        //foreach (var g in t.Get<Gate>("Gate")) g.OpenGate();

        yield return null;
    }
}
