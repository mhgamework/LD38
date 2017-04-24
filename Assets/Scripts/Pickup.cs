using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    [SerializeField]
    private int worth = 5;
    [SerializeField]
    private AudioSource audioSource = null;
    public bool IsPickedUp { get; private set; }

    public void ResetPickedUp()
    {
        IsPickedUp = false;
        gameObject.SetActive(true);
        GetComponent<Renderer>().enabled = true;
        audioSource.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPickedUp)
            return;
        IsPickedUp = true;

        PlayerPoints.Points += worth;

        //Debug.Log(string.Format("You earned {0} points!!", worth));
        StartCoroutine(OnPickup());
    }

    IEnumerator OnPickup()
    {
        audioSource.enabled = true;
        GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}
