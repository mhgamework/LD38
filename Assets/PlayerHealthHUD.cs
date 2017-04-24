using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHUD : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponentsInChildren<Text>().First(f => f.name == "HealthText").text =
            "HP: " + PlayerHealthScript.Instance.Health + " of " + PlayerHealthScript.Instance.MaxHealth;

    }
}
