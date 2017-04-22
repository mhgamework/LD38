using UnityEngine;
using System.Collections;

public class DisableOnPlay : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

	    if (Application.isPlaying)
	        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
