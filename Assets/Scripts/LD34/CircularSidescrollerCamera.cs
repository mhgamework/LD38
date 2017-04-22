using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CircularSidescrollerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    public Vector3 PlayerTransformOffset;


    private Vector3 Center;

    public float DistanceFromPlayer = 3;
    public float CameraYOffset = 0;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var lookTarget = playerTransform.position + PlayerTransformOffset;
        Center.y = lookTarget.y;

        

        var awayFromCenterDir = (lookTarget - Center).normalized;
	    transform.position = lookTarget + awayFromCenterDir*DistanceFromPlayer + Vector3.up*CameraYOffset;
        transform.LookAt(lookTarget);

    }
}
