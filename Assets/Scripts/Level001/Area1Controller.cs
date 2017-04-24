using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Area1Controller : MonoBehaviour
    {
        private bool done = false;
        private Coroutine coroutine;
        private TimelineArea t;

        public void Start()
        {
            t = GetComponent<TimelineArea>();
            t.RunWithCheckpoint("Checkpoint", () => begin());
        }

      


        public IEnumerable<YieldInstruction> begin()
        {
            PlayerHealthScript.Instance.RestoreHealth();
            yield return null;

            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.CloseGate();
            foreach (var g in t.Get<Gate>("global.Gate02-2")) g.CloseGate();

            while (!t.playerInTrigger("Trigger1")) yield return new WaitForSeconds(0);
            t.Spawn("Fast1", t.Fast);

            while (!t.playerInTrigger("Trigger2")) yield return new WaitForSeconds(0);


            t.Spawn("Bomber2", t.Bomber);
            yield return new WaitForSeconds(1f);
            t.Spawn("Fast2", t.Fast);


            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(e => e.HasEnemies)) yield return null;

            yield return new WaitForSeconds(1f);


            foreach (var g in t.Get<Gate>("global.Gate02-1")) g.OpenGate();
            PlayerHealthScript.Instance.RestoreHealth();
        }




    }
}