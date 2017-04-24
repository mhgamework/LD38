using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Island02_02Controller : MonoBehaviour
    {

        private bool done = false;
        private Coroutine coroutine;
        private TimelineArea t;

        public void Start()
        {
            t = GetComponent<TimelineArea>();

            //t.Disable(name);
            foreach (var c in t.Get<TimelineCheckpoint>("Checkpoint02-top")) c.CheckpointActive = false;
            t.RunWithCheckpoint("Checkpoint02-top", begin);
            //coroutine = StartCoroutine(begin().GetEnumerator());
            //GetComponent<BendAroundPlanet>().GetTarget().gameObject.SetActive(false);
        }

        public IEnumerable<YieldInstruction> begin()
        {
            yield return null;


            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.OpenGate();
            foreach (var g in t.Get<Gate>("global.Gate02-2")) g.CloseGate();

            while (!t.playerInTrigger("Trigger")) yield return null;

            foreach (var i in t.SpawnAsync("Fast3", t.Fast, 0.3f)) yield return i;


            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;


            foreach (var i in t.SpawnAsync("Fast1", t.Fast, 0.5f)) yield return i;


            foreach (var i in t.SpawnAsync("Fast2", t.Fast, 0.5f)) yield return i;

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            for (int i = 0; i < 3; i++)
            {
                foreach (var k in t.SpawnAsync("Fast1", t.Fast, 0.5f)) yield return k;

                foreach (var k in t.SpawnAsync("Fast2", t.Fast, 0.5f)) yield return k;

            }



            yield return null;

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            //foreach (var g in t.Get<Gate>("Gate")) g.OpenGate();


            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.OpenGate();
            foreach (var g in t.Get<Gate>("global.Gate02-2")) g.OpenGate();

            yield return null;
        }
    }
}