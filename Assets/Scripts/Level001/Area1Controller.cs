using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Timeline;
using UnityEngine;

namespace Assets.Scripts.Level001
{
    public class Area1Controller : Singleton<Area1Controller>
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
            yield return null;
            while (!t.playerInTrigger("Trigger1")) yield return new WaitForSeconds(0);
            t.Spawn("Fast1", t.Fast);

            while (!t.playerInTrigger("Trigger2")) yield return new WaitForSeconds(0);


            t.Spawn("Bomber2", t.Bomber);
            yield return new WaitForSeconds(1f);
            t.Spawn("Fast2", t.Fast);


            while (t.Get<TimelineEnemyDetector>("EnemyDetector").Any(e => e.HasEnemies)) yield return null;

            yield return new WaitForSeconds(1f);


            foreach (var g in t.Get<Gate>("Gate1")) g.OpenGate();
        }




    }
}