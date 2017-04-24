using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Timeline;
using UnityEngine;

public class Island04_04Controller : MonoBehaviour
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

        foreach (var spawner in t.Get<Spawner>("Heavy"))
        {
            spawner.Spawn(t.Heavy);
            yield return new WaitForSeconds(0.2f);
        }
        foreach (var spawner in t.Get<Spawner>("Fast1"))
        {
            spawner.Spawn(t.Fast);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(6f);

        foreach (var spawner in t.Get<Spawner>("Fast1"))
        {
            spawner.Spawn(t.Fast);
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;

        while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

        PlayerHealthScript.Instance.RestoreHealth();

        //foreach (var g in t.Get<Gate>("Gate")) g.OpenGate();

        yield return null;
    }
}
