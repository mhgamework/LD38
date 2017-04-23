using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BomberEnemyEventHandler : MonoBehaviour
{

    public float TestVar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrintFLoat(float f)
    {
        transform.GetComponentInParent<BomberEnemy>().Fire();
    }
}
