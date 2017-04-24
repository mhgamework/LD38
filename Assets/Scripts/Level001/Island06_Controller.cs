using Assets.Scripts.Timeline;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Island06_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject[] endObjects = new GameObject[0];

    [SerializeField]
    private TextMesh waveText = null;

    private bool done = false;
    private Coroutine coroutine;
    private TimelineArea t;
    private IEnumerator routine;

    public void Start()
    {
        t = GetComponent<TimelineArea>();
        routine = begin().GetEnumerator();
        StartCoroutine(routine);
    }

    public IEnumerable<YieldInstruction> begin()
    {
        yield return null;

        waveText.gameObject.SetActive(false);

        TimelineService.Instance.ActivateCheckpoint(t.Get<TimelineTrigger>("Checkpoint").First(), () =>
        {
            Debug.Log("ENDED");
            StopCoroutine(routine);

            foreach (var g in t.Get<Gate>("global.Gate4")) g.OpenGate();
            waveText.gameObject.SetActive(false);
            foreach (var o in endObjects)
            {
                o.SetActive(true);
            }
            PlayerHealthScript.Instance.RestoreHealth();
        });


        while (!t.playerInTrigger("Trigger")) yield return null;

        TimelineService.Instance.enableRestore = false;

        foreach (var g in t.Get<Gate>("global.Gate4")) g.CloseGate();
        waveText.gameObject.SetActive(true);

        PlayerHealthScript.Instance.RestoreHealth();

        var i = 1;
        while (true)
        {
            waveText.text = "Wave: " + i;
            PlayerHealthScript.Instance.RestoreHealth();

            for (int j = 0; j < i; j++)
            {
                foreach (var spawner in t.Get<Spawner>("Heavy1"))
                {
                    spawner.Spawn(t.Heavy);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.5f);
                foreach (var spawner in t.Get<Spawner>("Fast1"))
                {
                    spawner.Spawn(t.Fast);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.5f);
                foreach (var spawner in t.Get<Spawner>("Bomber1"))
                {
                    spawner.Spawn(t.Bomber);
                    yield return new WaitForSeconds(0.2f);
                }
            }

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;
            i++;
        }
    }
}
