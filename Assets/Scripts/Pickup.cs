using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    [SerializeField]
    private int worth = 5;
    [SerializeField]
    private AudioSource audioSource = null;
    private bool isPickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp)
            return;
        isPickedUp = true;

        Debug.Log(string.Format("You earned {0} points!!", worth));
        StartCoroutine(OnPickup());
    }

    IEnumerator OnPickup()
    {
        audioSource.enabled = true;
        GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        Destroy(this);
    }
}
