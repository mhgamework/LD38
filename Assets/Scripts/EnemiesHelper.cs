using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemiesHelper
    {

        public static IEnemy GetEnemyForCollider(Collider c)
        {
            return c.transform.GetComponentInParent<IEnemy>();
        }
    }
}
