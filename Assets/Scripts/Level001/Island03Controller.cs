using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Island03Controller : MonoBehaviour
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
            foreach (var g in t.Get<Gate>("global.Gate03")) g.OpenGate();

            while (!t.playerInTrigger("Trigger")) yield return null;
            foreach (var g in t.Get<Gate>("global.Gate03")) g.CloseGate();
            t.Spawn("Heavy", t.Heavy);
            t.Spawn("Fast", t.Fast);
            t.Spawn("Bomber", t.Bomber);
            yield return null;

            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(f => f.HasEnemies)) yield return null;

            foreach (var g in t.Get<Gate>("global.Gate03")) g.OpenGate();
            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.OpenGate();
            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.OpenGate();

            foreach (var c in t.Get<TimelineCheckpoint>("global.Checkpoint02-top")) c.CheckpointActive = true;


            yield return null;
        }
    }
}