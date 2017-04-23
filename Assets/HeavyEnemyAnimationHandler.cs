using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAnimationHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAirbourne()
    {
        transform.GetComponentInParent<HeavyEnemy>().MarkAirbourne();
    }
}
