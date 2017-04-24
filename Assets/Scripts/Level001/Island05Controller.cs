using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Island05Controller : MonoBehaviour
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
            foreach (var g in t.Get<Gate>("global.Gate05")) g.CloseGate();
            t.Disable("PreciousPickupCenter");
            while (!t.playerInTrigger("Trigger")) yield return null;

            for (int i = 0; i < 3; i++)
            {
                t.Spawn("Spawn1", t.Fast);
                t.Spawn("Spawn2", t.Fast);
                t.Spawn("Spawn3", t.Fast);
                t.Spawn("Spawn4", t.Fast);

                yield return new WaitForSeconds(1f);
            }

            yield return null;
            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;
            while (!t.playerInTrigger("Checkpoint")) yield return null;


            yield return new WaitForSeconds(2);

            t.Spawn("Spawn5Bomber", t.Bomber);
            for (int i = 0; i < 3; i++)
            {
                t.Spawn("Spawn1", t.Fast);
                t.Spawn("Spawn2", t.Fast);
                t.Spawn("Spawn3", t.Fast);
                t.Spawn("Spawn4", t.Fast);

                yield return new WaitForSeconds(1f);
            }

            yield return null;
            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            yield return new WaitForSeconds(0.5f);

            t.Enable("PreciousPickupCenter");

            while (!t.playerInTrigger("TriggerCenter")) yield return null;
            yield return new WaitForSeconds(0.5f);

            foreach (var i in t.SpawnAsync("SpawnSurrounded", t.Fast, 0.2f)) yield return i;

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            foreach (var g in t.Get<Gate>("global.Gate05")) g.OpenGate();

            yield return null;
        }
    }
}