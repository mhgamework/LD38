using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private GameObject gateContainer = null;
    [SerializeField]
    private DummyEnemy[] dummyEnemies = new DummyEnemy[0];
    [SerializeField]
    private GameObject walkTutorialText = null;
    [SerializeField]
    private GameObject gateTutorialText = null;

    [SerializeField]
    private GameObject[] toDeactivate = new GameObject[0];

    private bool completed = false;

    void Start()
    {
        walkTutorialText.SetActive(false);
        PlanetCamera.EnableMovement = false;
    }

    void Update()
    {
        if (completed || dummyEnemies.Any(e => e != null))
            return;

        completed = true;
        StartCoroutine(OnTutorialCompleted());
    }

    IEnumerator OnTutorialCompleted()
    {
        gateContainer.transform.GetComponentInChildren<Gate>().OpenGate();
        PlanetCamera.EnableMovement = true;
        walkTutorialText.SetActive(true);
        gateTutorialText.SetActive(false);

        yield return new WaitForSeconds(3f);

        walkTutorialText.SetActive(false);

        foreach (var o in toDeactivate)
        {
            o.SetActive(false);
        }

        this.enabled = false;
    }

}
