using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Island02Controller : MonoBehaviour
    {

        private bool done = false;
        private Coroutine coroutine;
        private TimelineArea t;

        public void Start()
        {
            t = GetComponent<TimelineArea>();
            t.RunWithCheckpoint("Checkpoint", begin);
            //coroutine = StartCoroutine(begin().GetEnumerator());
            //GetComponent<BendAroundPlanet>().GetTarget().gameObject.SetActive(false);
        }

        public IEnumerable<YieldInstruction> begin()
        {
            yield return null;
            foreach (var g in t.Get<Gate>("Gate")) g.OpenGate();

            while (!t.playerInTrigger("Trigger")) yield return null;
            foreach (var g in t.Get<Gate>("Gate")) g.CloseGate();
            //t.Spawn("Heavy", t.Heavy);
            t.Spawn("Fast", t.Fast);
            //t.Spawn("Bomber", t.Bomber);
            yield return null;

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            foreach (var g in t.Get<Gate>("Gate")) g.OpenGate();

            yield return null;
        }
    }
}